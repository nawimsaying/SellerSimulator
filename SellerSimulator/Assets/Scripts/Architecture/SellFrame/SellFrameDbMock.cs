using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameDbMock : ISellFrameSource
    {
        List<ModelBox> listBoxFromWareHouse = new List<ModelBox>(); // хранит в себе id коробки, которая хранит уже все остальные данные самой коробки

        public List<ulong> ChekIdBoxOnStylage(List<Sample> sampleList)
        {
            List<ulong> idList = new List<ulong>();

            if (sampleList.Count == 0 || sampleList[0].rackSample == null)
            {
                return idList;
            }

            for (int i = 0; i < sampleList[0].rackSample.Length; i++)
            {
                if (sampleList[0].rackSample[i] != 0)
                {
                    idList.Add(sampleList[0].rackSample[i]);
                }
            }

            return idList;
        }

        public Result<List<ModelsSaleFrame>> GetAll()
        {
            List<ModelsSaleFrame> resultList = new List<ModelsSaleFrame>();

            List<Sample> sampleList = SaveLoadManager.LoadSampleList(); // List stylage
            List<ulong> idListBox = ChekIdBoxOnStylage(sampleList);

            WareHouseDbMock data = SaveLoadManager.LoadWareHouseDbMockList(); // record
            listBoxFromWareHouse = data.purchasedItems; //duplicate list

            List<ModelBox> listBoxFromStylageWareHouse = new List<ModelBox>();



            if (idListBox.Count == 0)
            {
                // Возвращаем пустой список или null, чтобы обозначить отсутствие данных
                return Result<List<ModelsSaleFrame>>.Success(new List<ModelsSaleFrame>());
            }

            for (int i = 0; i < idListBox.Count; i++)
            {
                ModelBox item = listBoxFromWareHouse.FirstOrDefault(item => item.id == idListBox[i]);
                listBoxFromStylageWareHouse.Add(item);
            }



            foreach (var item in listBoxFromStylageWareHouse)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsSaleFrame modelsSaleFrame = new ModelsSaleFrame()
                {
                    idBox = item.id,
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

        public Result<bool> PutOnSale(ulong idBox, int countProduct, int priceSale)
        {
            throw new NotImplementedException();
        }
    }
}
