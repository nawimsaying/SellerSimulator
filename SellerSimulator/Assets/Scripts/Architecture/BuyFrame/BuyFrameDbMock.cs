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

        

        private MainDbMock _listLocal;
        private WareHouseDbMock _wareHouseDbMock;
        private PlayerData _playerData;

        public BuyFrameDbMock()
        {
            _listLocal = SaveLoadManager.LoadMainDbMockList();
            _wareHouseDbMock = new WareHouseDbMock();
            _playerData = PlayerDataHolder.playerData;
        }

        public Result<bool> UnlockItemForGold(int productId, int gold)
        {
            ModelBox itemToBuy = _listLocal.ListBox.FirstOrDefault(item => item.idProduct.id == productId);


            if (itemToBuy == null)
            {
                return Result<bool>.Error("Item with the specified ID was not found.");
            }  

            if (gold >= itemToBuy.idProduct.goldenPrice)
            {
                    int newGold = gold - itemToBuy.idProduct.goldenPrice;

                    PlayerPrefs.SetInt("Gold", newGold);

                    foreach (var modelBuyFrame in _listLocal.ListBox)
                    {
                        if (modelBuyFrame.idProduct.id == productId)
                        {
                            modelBuyFrame.idProduct.lockForGold = false;
                            break; // Можно прервать цикл, т.к. мы уже нашли нужный элемент
                        }
                    }

                    SaveLoadManager.SaveMainDbMockList(_listLocal);

                    _playerData.SetGold(newGold);

                    return Result<bool>.Success(true);
            }else
                return Result<bool>.Success(false);
        }

        string GetEmptyCellCountMessage(List<Sample> sampleList)
        {
            if (sampleList.Count == 0 || sampleList[0].rackSample == null)
            {
                return "Установите стеллаж";
            }

            int countEmptyCells = 0;

            for (int i = 0; i < sampleList.Count; i++)
            {
                for (int j = 0; j < sampleList[i].rackSample.Length; j++)
                {
                    if (sampleList[i].rackSample[j] == 0)
                    {
                        countEmptyCells++;
                    }
                }
                
            }

            return countEmptyCells.ToString();
        }

        string GetPlaceStylageMessage(List<Sample> sampleList)
        {
            if (sampleList.Count == 0 || sampleList[0].rackSample == null)
            {
                return "Установите стеллаж";
            }

            int countPlaceCells = 0;

            for (int i = 0; i < sampleList.Count; i++)
            {
                for (int j = 0; j < sampleList[i].rackSample.Length; j++)
                {
                    if (sampleList[i].rackSample[j] != 0)
                    {
                        countPlaceCells++; 
                    }                    
                }

            }

            return countPlaceCells.ToString();
        }

        Result<string> IBuyFrameSource.BuyItem(int productId, int countProducts, int priceProducts, int money)
        {
            ModelBox itemToBuy = _listLocal.ListBox.FirstOrDefault(item => item.idProduct.id == productId);

            if (itemToBuy == null)
            {
                return Result<string>.Error("Item with the specified ID was not found.");
            }

            List<Sample> sampleList = SaveLoadManager.LoadSampleList(); // List stylage
            
            WareHouseDbMock data = SaveLoadManager.LoadWareHouseDbMockList(); //database in wareHouse
            List<ModelBox> listBoxInWareHouse = data.purchasedItems;

            string resultMessageEmpty = GetEmptyCellCountMessage(sampleList); //Получаем кол-во о пустых ячеек 
            string resultMessageTest = GetPlaceStylageMessage(sampleList); // Кол-во мест

            
            if (resultMessageEmpty == "Установите стеллаж" || resultMessageTest == "Установите стеллаж")
                return Result<string>.Error($"Установите стеллаж");

            int countEmptyCells = Convert.ToInt32(resultMessageEmpty);
            int result = Convert.ToInt32(resultMessageTest);

            int countWareHousePlace = 0;
            int temp = listBoxInWareHouse.Count - result;

            countWareHousePlace = countEmptyCells - temp; 




            if (countWareHousePlace >= countProducts)
            {
                if (money >= priceProducts)
                {
                    int newMoney = money - priceProducts;

                    for (int i = 0; i < countProducts; i++) // Добавляем купленный товар (весь объект itemToBuy) в список класса WareHouseDbMock
                    {
                        _wareHouseDbMock.AddPurchasedItem(itemToBuy); 
                        
                        _playerData.AddExperience(250); // опыт временно / temp exp
                    }
                    _playerData.SetCoins(newMoney);

                    return Result<string>.Success($"Товар куплен успешно: {itemToBuy.idProduct.name}");
                }
                else
                    return Result<string>.Error($"Не достаточно средств");
            }
            else
                return Result<string>.Error("Не достаточно места на складе");
        }

        Result<List<ModelsBuyFrame>> IBuyFrameSource.GetAll()
        {
            List<ModelsBuyFrame> resultList = new List<ModelsBuyFrame>();
   

            foreach (var item in _listLocal.ListBox)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsBuyFrame modelsBuyFrame = new ModelsBuyFrame()
                {

                    idProduct = item.idProduct.id,
                    productName = item.idProduct.name,
                    price = item.price,
                    imageName = item.idProduct.imageName,
                    levelUnlock = item.idProduct.lvlUnlock,
                    lockForGold = item.idProduct.lockForGold,
                    goldenPrice = item.idProduct.goldenPrice
                };


                // Добавляем экземпляр ModelsBuyFrame в результирующий лист
                resultList.Add(modelsBuyFrame);
            }

            return Result<List<ModelsBuyFrame>>.Success(resultList);
        }
    }
}
