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

namespace Assets.Scripts.Architecture.MainDB
{
    class BuyFrameDbMock : IBuyFrameSource // переделать класс
    {
        const int originallyCountBox = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        private MainDbMock mainDbMock;
        private WareHouseDbMock wareHouseDbMock;


        public BuyFrameDbMock()
        {
            mainDbMock = new MainDbMock();
            wareHouseDbMock = WareHouseDbMock.Instance;
        }



        Result<string> IBuyFrameSource.BuyItem(int productId, int money)
        {
            ModelBox itemToBuy = mainDbMock.ListBox.FirstOrDefault(item => item.idProduct.id == productId);

            if (itemToBuy == null)
            {
                return Result<string>.Error("Item with the specified ID was not found.");
            }

            // Добавляем купленный товар (весь объект itemToBuy) в список класса WareHouseDbMock
            wareHouseDbMock.AddPurchasedItem(itemToBuy);

            return Result<string>.Success($"Товар куплен успешно: {itemToBuy.idProduct.name}");
        }

        Result<List<ModelsBuyFrame>> IBuyFrameSource.GetAll()
        {
            List<ModelsBuyFrame> result = new List<ModelsBuyFrame>();

            

            foreach (var modelBox in mainDbMock.ListBox)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsBuyFrame modelsBuyFrame = new ModelsBuyFrame()
                {
                    
                    idProduct = modelBox.idProduct.id,
                    productName = modelBox.idProduct.name,
                    price = modelBox.price,
                    imageName = modelBox.idProduct.imageName
                };


                // Добавляем экземпляр ModelsBuyFrame в результирующий лист
                result.Add(modelsBuyFrame);
            }

            return Result<List<ModelsBuyFrame>>.Success(result);
        }
    }
}
