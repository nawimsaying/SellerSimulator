using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.OnSaleFrame;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UIElements;


namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameDbMock : ISellFrameSource
    {

        List<ModelBox> listBoxFromWareHouse = new List<ModelBox>(); // хранит в себе id коробки, которая хранит уже все остальные данные самой коробки

        private WareHouseDbMock _listWareHouse;
        private OnSaleFrameDbMock _listOnSale;
        private PlayerData _playerData;
        double defaultValue = 1;

        public SellFrameDbMock()
        {
            _playerData = PlayerDataHolder.playerData;
        }

        public Result<List<ModelsSaleFrame>> GetAll()
        {
            _listOnSale = SaveLoadManager.LoadOnSaleFrameDbMockList();

            List<Sample> sampleList = SaveLoadManager.LoadSampleList(); // List stylage

            List<ulong> ChekIdBoxOnStylage(List<Sample> sampleList) // Local Method 
            {
                List<ulong> idList = new List<ulong>();

                if (sampleList.Count == 0 || sampleList[0].rackSample == null)   
                    return idList;
                                                                                            
                for (int i = 0; i < sampleList.Count; i++)
                {
                    for (int j  = 0; j < sampleList[i].rackSample.Length; j++)
                    {
                        if (sampleList[i].rackSample[j] != 0)
                            idList.Add(sampleList[i].rackSample[j]);
                    }
                }

                return idList;
            }

            List<ulong> idListBox = ChekIdBoxOnStylage(sampleList);

            _listWareHouse = SaveLoadManager.LoadWareHouseDbMockList();
            listBoxFromWareHouse = _listWareHouse.purchasedItems; //duplicate list

            List<ModelBox> listBoxFromStylageWareHouse = new List<ModelBox>();

            if (idListBox.Count == 0)
                return Result<List<ModelsSaleFrame>>.Success(new List<ModelsSaleFrame>());  // Возвращаем пустой список или null, чтобы обозначить отсутствие данных


            for (int i = 0; i < idListBox.Count; i++)
            {
                ModelBox item = listBoxFromWareHouse.FirstOrDefault(item => item.id == idListBox[i]);
                if(item != null)
                    listBoxFromStylageWareHouse.Add(item);

            }

            Dictionary<int, ModelBox> productCountDict = new Dictionary<int, ModelBox>();

            foreach (var item in listBoxFromStylageWareHouse)
            {
                if (productCountDict.ContainsKey(item.idProduct.id))
                {
                    // Если idProduct уже есть в словаре, добавляем к существующему значению countProduct
                    productCountDict[item.idProduct.id].countProduct += item.countProduct;
                }
                else
                {
                    // Если idProduct отсутствует в словаре, добавляем его с текущим элементом
                    productCountDict[item.idProduct.id] = item;
                }
            }

            List<ModelBox> listSumProducts = productCountDict.Values.ToList();

            foreach (var item in listBoxFromStylageWareHouse)
            {
                if (!productCountDict.ContainsKey(item.idProduct.id))
                {
                    listSumProducts.Add(item);
                }
            }
    
            List<ModelsSaleFrame> formattedListProducts = new List<ModelsSaleFrame>();

            foreach (var item in listSumProducts)
            {
                // Создаем экземпляр ModelsBuyFrame и заполняем его данными из modelBox
                ModelsSaleFrame modelsSaleFrame = new ModelsSaleFrame()
                {
                    idBox = item.id,
                    idProduct = item.idProduct.id,
                    countProduct = item.countProduct,
                    productName = item.idProduct.name,
                    price = item.price,
                    imageName = item.idProduct.imageName,
                    liquidity = item.idProduct.liquidity
                };

                formattedListProducts.Add(modelsSaleFrame);
            }

            List<ModelsSaleFrame> resultList = new List<ModelsSaleFrame>(); //List contain item

            foreach (var item in formattedListProducts)
            {
                ModelsSaleFrame modelsSaleFrame = new ModelsSaleFrame()
                {
                    idBox = item.idBox,
                    idProduct = item.idProduct,
                    countProduct = item.countProduct,
                    productName = item.productName,
                    price = item.price,
                    imageName = item.imageName,
                    liquidity = item.liquidity
                };

                var matchingItems = _listOnSale.onSaleProduct
                    .Where(saleItem => saleItem.idProduct == item.idProduct)
                    .ToList(); //Содержит в себе повторяющиеся объекты из  _listOnSale.onSaleProduct, которые есть в resultListBoxs

                if (matchingItems.Any())
                {
                    // Если есть совпадения, вычисляем сумму countProduct для всех совпадающих элементов
                    int totalToSubtract = matchingItems.Sum(matchingItem => matchingItem.countProduct);
                    // Отнимаем сумму
                    modelsSaleFrame.countProduct -= totalToSubtract;    
                }
                resultList.Add(modelsSaleFrame);
            }
            resultList.RemoveAll(item => item.countProduct == 0);
                     
            return Result<List<ModelsSaleFrame>>.Success(resultList);
        }

        public Result<bool> InstantSale(int idProduct, int countProduct, int currentPrice)
        {
            List<ModelBox> listWareHouse = new List<ModelBox>();

            int countProductForExp = countProduct;

            _listWareHouse = SaveLoadManager.LoadWareHouseDbMockList();
            listWareHouse = _listWareHouse.purchasedItems;

            while (countProduct > 0)
            {
                var matchingBoxes = listWareHouse.Where(box => box.idProduct.id == idProduct);

                if (matchingBoxes.Any())
                {
                    // Находим коробку с минимальным количеством countProduct
                    ModelBox boxWithMinCount = matchingBoxes.OrderBy(box => box.countProduct).First();

                    if(boxWithMinCount.countProduct >= countProduct)
                    {
                        boxWithMinCount.countProduct -= countProduct;
                        countProduct = 0;
                    }else if (boxWithMinCount.countProduct < countProduct)
                    {
                        countProduct -= boxWithMinCount.countProduct;
                        boxWithMinCount.countProduct = 0;
                    }                   

                    if (boxWithMinCount.countProduct == 0)
                    {
                        listWareHouse.Remove(boxWithMinCount);
                    }
                }
            }
            _listWareHouse.purchasedItems = listWareHouse;
            SaveLoadManager.SaveWareHouseDbMockList(_listWareHouse);

            int expForSale = countProductForExp * 32;
            _playerData.AddCoins(currentPrice);
            _playerData.AddExperience(expForSale);
            return Result<bool>.Success(true);
        }

        public Result<bool> PutOnSale(int idProduct, int countProduct)
        {
            _listOnSale = SaveLoadManager.LoadOnSaleFrameDbMockList();
            List<ModelsOnSaleFrame> list = _listOnSale.onSaleProduct; // duplicate
      
            ModelBox item = listBoxFromWareHouse.FirstOrDefault(item => item.idProduct.id == idProduct);

            ModelsOnSaleFrame listOnSale = new ModelsOnSaleFrame()
            {
                idSell = list.Count == 0 ? 1 : list.Max(item => item.idSell) + 1,
                priceProduct = SalePrice(item.countProduct, item.price),
                idProduct = idProduct,
                countProduct = countProduct,
                imageName = item.idProduct.imageName,
                nameProduct = item.idProduct.name,
                liquidity = item.idProduct.liquidity,
                bufAds = false,
                buffLiquidity = defaultValue,
                uniqueOrderId = Guid.NewGuid()
            };

            _listOnSale.onSaleProduct.Add(listOnSale);
            SaveLoadManager.SaveOnSaleFrameDbMockList(_listOnSale);          

            return Result<bool>.Success(true);
        }

        private int SalePrice(int countProduct, int priceBox)
        {
            double result = (priceBox / countProduct) * 1.2;

            return Convert.ToInt32(result);
        }

    }
}
