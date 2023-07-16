using Assets.Scripts.Architecture.MainDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    class BuyFrameDbMock : IBuyFrame // переделать класс
    {
        const int originallyCountBox = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        private MainDbMock mainDbMock;

        public BuyFrameDbMock()
        {
            mainDbMock = new MainDbMock();
        }  
           


        Result<ModelsBuyFrame> IBuyFrame.BuyItem(int money)
        {
            throw new NotImplementedException();
        }

        Result<List<ModelsBuyFrame>> IBuyFrame.GetAll()
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
