using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameDbMock : ISellFrameSource
    {
        List<ModelBox> listBoxFromWareHouse = new List<ModelBox>(); // хранит в себе id коробки, которая хранит уже все остальные данные самой коробки

        public Result<List<ModelsSaleFrame>> GetAll()
        {
            List<ModelsSaleFrame> resultList = new List<ModelsSaleFrame>();


            WareHouseDbMock data = SaveLoadManager.LoadWareHouseDbMockList(); // record
            listBoxFromWareHouse = data.purchasedItems; //duplicate list

          
                    foreach (var item in listBoxFromWareHouse)
                    {
                        // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                        ModelsSaleFrame modelsSaleFrame = new ModelsSaleFrame()
                        {

                            idProduct = item.idProduct.id,
                            countProduct = item.countProduct,
                            productName = item.idProduct.name,
                            price = item.price,
                            imageName = item.idProduct.imageName
                        };

                        resultList.Add(modelsSaleFrame);
                    }
               
           

            return Result<List<ModelsSaleFrame>>.Success(resultList);
        }
    }
}
