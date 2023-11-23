using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using static UnityEditor.Progress;

namespace Assets.Scripts.Architecture.OnSaleFrame
{
    public class OnSaleFrameDbMock : IOnSaleFrameSource
    {
        public List<ModelsOnSaleFrame> onSaleProduct = new List<ModelsOnSaleFrame>(); // stores the products on display for sale

        OnSaleFrameDbMock _listOnSale;
        WareHouseDbMock _mainDbMock;

        public Result<string> CancelSell(int idSell)
        {
            throw new NotImplementedException();
        }

        public Result<List<ModelsOnSaleFrame>> GetAll()
        {
            List<ModelsOnSaleFrame> resultList = new List<ModelsOnSaleFrame>();
            _listOnSale = SaveLoadManager.LoadOnSaleFrameDbMockList();

            List<ModelsOnSaleFrame> list = _listOnSale.onSaleProduct;

            foreach (var item in list)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsOnSaleFrame listProductsOnSale = new ModelsOnSaleFrame()
                {
                    idSell = item.idSell,
                    idProduct = item.idProduct,
                    imageName = item.imageName,
                    countProduct = item.countProduct,
                    nameProduct = item.nameProduct
                };

                // Добавляем экземпляр ModelsBuyFrame в результирующий лист
                resultList.Add(listProductsOnSale);
            }

            return Result<List<ModelsOnSaleFrame>>.Success(resultList);
        }

        public Result<bool> SaveDataList(List<ModelsOnSaleFrame> newListOnSaleFrame)
        { 
            OnSaleFrameRepository onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
            List<ModelsOnSaleFrame> listSaleItems = onSaleFrameRepository.GetAll();

            for (int i = 0; i < newListOnSaleFrame.Count; i++)
            {
                listSaleItems[i].countProduct -= newListOnSaleFrame[i].countProduct;
            }

            for (int i = 0; i < newListOnSaleFrame.Count; i++)
            {
                if (newListOnSaleFrame[i].countProduct == 0)
                {
                    newListOnSaleFrame.Remove(newListOnSaleFrame[i]);
                }

            }

            if (newListOnSaleFrame != null && listSaleItems.Count != 0)
            {
                List<ModelBox> listWareHouse = new List<ModelBox>();

                _mainDbMock = SaveLoadManager.LoadWareHouseDbMockList();
                listWareHouse = _mainDbMock.purchasedItems;

                foreach (var saleItem in listSaleItems)
                {
                    var matchingBoxes = listWareHouse.Where(box => box.idProduct.id == saleItem.idProduct);

                    if (matchingBoxes.Any())
                    {
                        // Находим коробку с минимальным количеством countProduct
                        ModelBox boxWithMinCount = matchingBoxes.OrderBy(box => box.countProduct).First();


                        boxWithMinCount.countProduct -= saleItem.countProduct;

                        if(boxWithMinCount.countProduct == 0)
                        {
                            listWareHouse.Remove(boxWithMinCount);
                        }
                        
                    }
                }
                ////////////////////////////////////////////////////////
                // Save OnSaleFrame
                /////////////////////////////////////////////////////////
                _listOnSale.onSaleProduct = newListOnSaleFrame;
                SaveLoadManager.SaveOnSaleFrameDbMockList(_listOnSale);
                /////////////////////////////////////////////////////////

                _mainDbMock.purchasedItems = listWareHouse;
                SaveLoadManager.SaveWareHouseDbMockList(_mainDbMock);
                

                return Result<bool>.Success(true);
            }
            return Result<bool>.Error("List null");      
            

        }  

    }
}
