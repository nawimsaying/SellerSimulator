using Assets.Scripts.Architecture.DataBases.AdvertisingDb;
using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Player;
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
        private PlayerData _playerData;

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
                    priceProduct = item.priceProduct,
                    idProduct = item.idProduct,
                    imageName = item.imageName,
                    countProduct = item.countProduct,
                    nameProduct = item.nameProduct,
                    liquidity = item.liquidity,
                    buffLiquidity = item.buffLiquidity,
                    bufAds = item.bufAds,
                    uniqueOrderId = item.uniqueOrderId
                };

                // Добавляем экземпляр ModelsBuyFrame в результирующий лист
                resultList.Add(listProductsOnSale);
            }

            return Result<List<ModelsOnSaleFrame>>.Success(resultList);
        }


        public Result<List<ModelAdvertising>> GetAllAds()
        {
            List<ModelAdvertising> listAds = AdvertisingDbMock.ListAds;

            List<ModelAdvertising> resultListAds = new List<ModelAdvertising>();

            foreach (var item in listAds)
            {
                ModelAdvertising listProductsOnSale = new ModelAdvertising()
                {
                    id = item.id,
                    nameAds = item.nameAds,
                    description = item.description,
                    imageName = item.imageName,
                    priceWatchAds = item.priceWatchAds,
                    goldenPrice = item.goldenPrice,
                    buffLiquidity = item.buffLiquidity,

                };

                resultListAds.Add(listProductsOnSale);
            }

            return Result<List<ModelAdvertising>>.Success(resultListAds);
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
                    i--;
                }

            }

            if (newListOnSaleFrame != null && listSaleItems.Count != 0)
            {
                List<ModelBox> listWareHouse = new List<ModelBox>();

                _mainDbMock = SaveLoadManager.LoadWareHouseDbMockList();
                listWareHouse = _mainDbMock.purchasedItems;

                foreach (var saleItem in listSaleItems)
                {
                    while (saleItem.countProduct > 0)
                    {
                        var matchingBoxes = listWareHouse.Where(box => box.idProduct.id == saleItem.idProduct);

                        if (matchingBoxes.Any())
                        {
                            ModelBox boxWithMinCount = matchingBoxes.OrderBy(box => box.countProduct).First();

                            if (boxWithMinCount.countProduct >= saleItem.countProduct)
                            {
                                boxWithMinCount.countProduct -= saleItem.countProduct;
                                saleItem.countProduct = 0;
                            }
                            else if (boxWithMinCount.countProduct < saleItem.countProduct)
                            {
                                saleItem.countProduct -= boxWithMinCount.countProduct;
                                boxWithMinCount.countProduct = 0;
                            }

                            if (boxWithMinCount.countProduct == 0)
                            {
                                listWareHouse.Remove(boxWithMinCount);
                            }
                        }

                    
                        // Находим коробку с минимальным количеством countProduct


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




        public Result<bool> SetBuffForItem(ModelsOnSaleFrame item, ModelAdvertising ads)
        {
            _playerData = PlayerDataHolder.playerData;

            if (ads.priceWatchAds)
            {
                //implementation of advertising display to the user
            }
            else if (_playerData.Gold >= ads.goldenPrice)
            {
                _playerData.RemoveGold(ads.goldenPrice);
            }
            else
            {
                return Result<bool>.Success(false);
            }

            OnSaleFrameRepository onSaleFrameRepository = new OnSaleFrameRepository(new OnSaleFrameDbMock());
            List<ModelsOnSaleFrame> listSaleItems = onSaleFrameRepository.GetAll();

            var saleItem = listSaleItems.FirstOrDefault(sale => sale.uniqueOrderId == item.uniqueOrderId);            
            
            if(saleItem != null)
            {
                if (saleItem.bufAds == false)
                {
                    saleItem.buffLiquidity = ads.buffLiquidity;
                    saleItem.bufAds = true;
                }
                else
                {
                    return Result<bool>.Error("Buff advertising is already connected");
                }               
            }
            else
            {
                return Result<bool>.Success(false);
            }
            _listOnSale = new OnSaleFrameDbMock();
            _listOnSale.onSaleProduct = listSaleItems;
            SaveLoadManager.SaveOnSaleFrameDbMockList(_listOnSale);

            return Result<bool>.Success(true);
        }
    }
}
