using Assets.Scripts.Architecture.MainDb;
using Assets.Scripts.Architecture.MainDb.ModelsDb;
using Assets.Scripts.Architecture.OnSaleFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameDbMock : ISellFrameSource
    {
        List<ModelBox> listBoxFromWareHouse = new List<ModelBox>(); // хранит в себе id коробки, которая хранит уже все остальные данные самой коробки

        private WareHouseDbMock _listWareHouse;
        private OnSaleFrameDbMock _listOnSale;


        public SellFrameDbMock()
        {
            _listWareHouse = SaveLoadManager.LoadWareHouseDbMockList();
            _listOnSale = SaveLoadManager.LoadOnSaleFrameDbMockList();
        }

       

        public Result<List<ModelsSaleFrame>> GetAll()
        {
            List<ModelsSaleFrame> resultList = new List<ModelsSaleFrame>();

            List<Sample> sampleList = SaveLoadManager.LoadSampleList(); // List stylage

            List<ulong> ChekIdBoxOnStylage(List<Sample> sampleList) // Local Method 
            {
                List<ulong> idList = new List<ulong>();

                if (sampleList.Count == 0 || sampleList[0].rackSample == null)
                {
                    return idList;
                }
                for (int i = 0; i < sampleList.Count; i++)
                {
                    for (int j  = 0; j < sampleList[i].rackSample.Length; j++)
                    {
                        if (sampleList[i].rackSample[j] != 0)
                        {
                            idList.Add(sampleList[i].rackSample[j]);
                        }
                    }
                }
                //Переделать GetAll Создать доп лист, затем сложить каждый idProduct по кол-ву товара, добавить проверку на активные продажи. Затем вывести список 

                return idList;
            }

            List<ulong> idListBox = ChekIdBoxOnStylage(sampleList);

            listBoxFromWareHouse = _listWareHouse.purchasedItems; //duplicate list

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

            List<ModelBox> result = productCountDict.Values.ToList();

            // Теперь uniqueProducts содержит только уникальные idProduct, а productCountDict содержит суммарные countProduct

            // Добавляем оставшиеся элементы из исходного списка
            foreach (var item in listBoxFromStylageWareHouse)
            {
                if (!productCountDict.ContainsKey(item.idProduct.id))
                {
                    result.Add(item);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //
            /// !!!Добавить проверку на продажу товаров на данный момент. (Вычесть Весь список от списка продаж на данный момент) и вывести
            //
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            foreach (var item in result)
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


            List<ModelsSaleFrame> resultListBeta = new List<ModelsSaleFrame>();

            foreach (var item in resultList)
            {
                ModelsSaleFrame modelsSaleFrame = new ModelsSaleFrame()
                {
                    idBox = item.idBox,
                    idProduct = item.idProduct,
                    countProduct = item.countProduct,
                    productName = item.productName,
                    price = item.price,
                    imageName = item.imageName
                };

                var matchingItems = _listOnSale.onSaleProduct
                    .Where(saleItem => saleItem.idProduct == item.idProduct)
                    .ToList();

                if (matchingItems.Any())
                {
                    // Если есть совпадения, вычисляем сумму countProduct для всех совпадающих элементов
                    int totalToSubtract = matchingItems.Sum(matchingItem => matchingItem.countProduct);

                    // Отнимаем сумму
                    modelsSaleFrame.countProduct -= totalToSubtract;

                    
                }

                resultListBeta.Add(modelsSaleFrame);
            }
            resultListBeta.RemoveAll(item => item.countProduct == 0);



            return Result<List<ModelsSaleFrame>>.Success(resultListBeta);
        }

        public Result<bool> PutOnSale(int idProduct, int countProduct)
        {            
            List<ModelsOnSaleFrame> list = _listOnSale.onSaleProduct; // duplicate

            ModelsOnSaleFrame listOnSale = new ModelsOnSaleFrame()
            {
                idSell = list.Count == 0 ? 1 : list.Max(item => item.idSell) + 1,
                idProduct = idProduct,
                countProduct = countProduct,
            };

            _listOnSale.onSaleProduct.Add(listOnSale);

            SaveLoadManager.SaveOnSaleFrameDbMockList(_listOnSale);          

            return Result<bool>.Success(true);
        }




    }
}
