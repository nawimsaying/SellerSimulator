using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    class MainDbMock : IMainDbSource
    {
        const int originallyCountBox = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        List<ModelsDb> listDb = new List<ModelsDb>()
        {
            new ModelsDb()
            {
                idProduct = 1,
                productName = "Iphone 14",
                price = 1000,
                imageName = "IphoneIcon.png", // Сделать отдельный класс переменных констат которые будут хранить в себе путь к файлу или сам файл
                idBoxProduct = new BoxProductInfo()
                {
                    id = 1,
                    countProduct = 30,
                    boxName = "Коробка с Iphone 14",
                    countBox = originallyCountBox
                },


            },
            new ModelsDb()
            {
                idProduct = 2,
                productName = "headphones pro",
                price = 800,
                imageName = "HeadIcon.png", // Сделать отдельный класс переменных констат которые будут хранить в себе путь к файлу или сам файл
                idBoxProduct = new BoxProductInfo()
                {
                    id = 1,
                    countProduct = 30,
                    boxName = "Коробка с headphones pro",
                    countBox = originallyCountBox
                },
            },
            new ModelsDb()
            {
                idProduct = 3,
                productName = "Macbook Pro 14",
                price = 800,
                imageName = "MacBookIcon.png", // Сделать отдельный класс переменных констат которые будут хранить в себе путь к файлу или сам файл
                idBoxProduct = new BoxProductInfo()
                {
                    id = 1,
                    countProduct = 30,
                    boxName = "Коробка с Macbook Pro 14",
                    countBox = originallyCountBox
                },
            }
        };


        public void BuyItem(int requestBuy) //Реализовать метод покупки товара
        {
            // для начала реализовать перемещение коробок
            throw new NotImplementedException();
        }

        public void getAll()
        {
            throw new NotImplementedException();
        }
    }
}
