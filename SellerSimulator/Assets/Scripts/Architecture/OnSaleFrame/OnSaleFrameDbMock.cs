using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.OnSaleFrame
{
    public class OnSaleFrameDbMock : IOnSaleFrameSource
    {
        public List<ModelsOnSaleFrame> onSaleProduct = new List<ModelsOnSaleFrame>(); // stores the products on display for sale

        OnSaleFrameDbMock _listOnSale;

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
    }
}
