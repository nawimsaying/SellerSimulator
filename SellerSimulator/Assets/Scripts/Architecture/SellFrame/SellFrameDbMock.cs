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

namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameDbMock : ISellFrameSource
    {
        List<ModelBox> listBoxFromWareHouse = new List<ModelBox>(); // хранит в себе id коробки, которая хранит уже все остальные данные самой коробки

        private WareHouseDbMock _listWareHouse;


        public SellFrameDbMock()
        {
            _listWareHouse = SaveLoadManager.LoadWareHouseDbMockList();

        }

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

            //Переделать GetAll Создать доп лист, затем сложить каждый idProduct по кол-ву товара, добавить проверку на активные продажи. Затем вывести список 

            return idList;
        }

        public Result<List<ModelsSaleFrame>> GetAll()
        {
            List<ModelsSaleFrame> resultList = new List<ModelsSaleFrame>();

            List<Sample> sampleList = SaveLoadManager.LoadSampleList(); // List stylage
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



            return Result<List<ModelsSaleFrame>>.Success(resultList);
        }

        public Result<bool> PutOnSale(ulong idBox, int countProduct, int priceSale)//переделать 
        {
            OnSaleFrameDbMock data = SaveLoadManager.LoadOnSaleFrameDbMockList();
            List<ModelBox> listProductsOnSale = data.onSaleProduct; // duplicate

            ModelBox itemToWareHouse = _listWareHouse.purchasedItems.FirstOrDefault(item => item.id == idBox);

            if (itemToWareHouse == null)
            {
                return Result<bool>.Error("Item not found in warehouse.");
            }

            // Создаем копии объектов
            ModelBox dataForSale = new ModelBox
            {
                id = itemToWareHouse.id,
                nameBox = itemToWareHouse.nameBox,
                imageBox = itemToWareHouse.imageBox,
                price = priceSale,
                countBox = itemToWareHouse.countBox,
                countProduct = countProduct,
                sizeBox = itemToWareHouse.sizeBox,
                idProduct = itemToWareHouse.idProduct
                // Копируйте остальные поля по аналогии
            };

            ModelBox dataForWareHouse = new ModelBox
            {
                id = itemToWareHouse.id,
                nameBox = itemToWareHouse.nameBox,
                imageBox = itemToWareHouse.imageBox,
                price = itemToWareHouse.price,
                countBox = itemToWareHouse.countBox,
                countProduct = itemToWareHouse.countProduct - countProduct, // Обновляем количество в складе
                sizeBox = itemToWareHouse.sizeBox,
                idProduct = itemToWareHouse.idProduct
                // Копируйте остальные поля по аналогии
            };

            // Добавляем объект dataForSale в список продаж
            listProductsOnSale.Add(dataForSale);
            data.onSaleProduct = listProductsOnSale;

            // Удаляем объект dataForWareHouse из склада только если продается все содержимое коробки
            List<ModelBox> tempList = new List<ModelBox>(_listWareHouse.purchasedItems);
            if (dataForWareHouse.countProduct == 0) // Сравниваем с нулем, чтобы определить, что продается все содержимое коробки
            {
                // Заменяем айди вместо его удаления
                List<Sample> sampleList = SaveLoadManager.LoadSampleList();
                List<ulong> rackSampleList = new List<ulong>(sampleList[0].rackSample);

                // Заменяем айди на 0
                int index = rackSampleList.IndexOf(idBox);
                if (index >= 0)
                {
                    rackSampleList[index] = 0;
                }

                sampleList[0].rackSample = rackSampleList.ToArray();
                SaveLoadManager.SaveSampleList(sampleList);
            }
            else
            {
                int indexOfItem = tempList.FindIndex(item => item.id == idBox);
                if (indexOfItem >= 0)
                {
                    tempList[indexOfItem] = dataForWareHouse; // Обновляем элемент в списке с учетом продажи
                }
                else
                {
                    return Result<bool>.Error("Error index");
                }
            }

            _listWareHouse.purchasedItems = tempList;

            // Сохраняем изменения
            SaveLoadManager.SaveOnSaleFrameDbMockList(data);
            SaveLoadManager.SaveWareHouseDbMockList(_listWareHouse);

            return Result<bool>.Success(true);
        }




    }
}
