using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

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

        public Result<bool> SaveDataList(List<ModelsOnSaleFrame> newListOnSaleFrame, List<ModelsOnSaleFrame> listSaleItems)
        {
            if(newListOnSaleFrame != null && listSaleItems != null)
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

                _mainDbMock.purchasedItems = listWareHouse;
                SaveLoadManager.SaveWareHouseDbMockList(_mainDbMock);
                ////////////////////////////////////////////////////////
                // Save OnSaleFrame
                /////////////////////////////////////////////////////////
                _listOnSale.onSaleProduct = newListOnSaleFrame;
                SaveLoadManager.SaveOnSaleFrameDbMockList(_listOnSale);
                /////////////////////////////////////////////////////////

                return Result<bool>.Success(true);
            }
            return Result<bool>.Error("List null");      
            
            //Переделать реализацию метода. Нужно удалять кол-во из склада выбраной коробки. 

        }  

    }
}
