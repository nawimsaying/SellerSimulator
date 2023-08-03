using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Assets.Scripts.Architecture.WareHouseDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Architecture.MainDB
{
    class BuyFrameDbMock : IBuyFrameSource // переделать класс
    {
        const int originallyCountBox = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка


        ///Сдлеать сохранение листа, а так же его обновление.

        List<ModelsBuyFrame> result = new List<ModelsBuyFrame>();

        private MainDbMock mainDbMock;
        private WareHouseDbMock wareHouseDbMock;
        private PlayerData playerData;


        public BuyFrameDbMock()
        {
            mainDbMock = new MainDbMock();
            wareHouseDbMock = WareHouseDbMock.Instance;
            playerData = PlayerDataHolder.playerData;
        }

        public Result<string> UnlockItemForGold(int productId, int gold)
        {
            ModelBox itemToBuy = mainDbMock.ListBox.FirstOrDefault(item => item.idProduct.id == productId);

            if (itemToBuy == null)
            {
                return Result<string>.Error("Item with the specified ID was not found.");
            }

            if(gold >= itemToBuy.idProduct.goldenPrice)
            {
                int newGold = gold - itemToBuy.idProduct.goldenPrice;

                PlayerPrefs.SetInt("Gold", newGold);

                foreach (var modelBuyFrame in result)
                {
                    if (modelBuyFrame.idProduct == productId)
                    {
                        modelBuyFrame.lockForGold = false;
                        break; // Можно прервать цикл, т.к. мы уже нашли нужный элемент
                    }
                }

                playerData.SetGold(newGold);

                return Result<string>.Success($"Товар разблокирован успешно: {itemToBuy.idProduct.name}");

            }
            else
            {
                return Result<string>.Success($"Не достаточно средств");
            }
        }

        Result<string> IBuyFrameSource.BuyItem(int productId, int money)
        {
            ModelBox itemToBuy = mainDbMock.ListBox.FirstOrDefault(item => item.idProduct.id == productId);

            if (itemToBuy == null)
            {
                return Result<string>.Error("Item with the specified ID was not found.");
            }

            if(money >= itemToBuy.price)
            {
                int newMoney = money - itemToBuy.price;

                PlayerPrefs.SetInt("Coins", newMoney);
               
                // Добавляем купленный товар (весь объект itemToBuy) в список класса WareHouseDbMock
                wareHouseDbMock.AddPurchasedItem(itemToBuy);
                playerData.SetCoins(newMoney);

                playerData.AddExperience(100);


                return Result<string>.Success($"Товар куплен успешно: {itemToBuy.idProduct.name}");
            }
            else
            {
                return Result<string>.Success($"Не достаточно средств");
            }
        }

        Result<List<ModelsBuyFrame>> IBuyFrameSource.GetAll()
        {
            

            foreach (var modelBox in mainDbMock.ListBox)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsBuyFrame modelsBuyFrame = new ModelsBuyFrame()
                {
                    
                    idProduct = modelBox.idProduct.id,
                    productName = modelBox.idProduct.name,
                    price = modelBox.price,
                    imageName = modelBox.idProduct.imageName,
                    levelUnlock = modelBox.idProduct.lvlUnlock,
                    lockForGold = modelBox.idProduct.lockForGold,
                    goldenPrice = modelBox.idProduct.goldenPrice
                };


                // Добавляем экземпляр ModelsBuyFrame в результирующий лист
                result.Add(modelsBuyFrame);
            }

            return Result<List<ModelsBuyFrame>>.Success(result);
        }
    }
}
