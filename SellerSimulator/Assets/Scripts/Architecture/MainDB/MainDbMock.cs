using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDb
{
    class MainDbMock
    {
        const int RIGINALLY_COUNT_BOX = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        List<ModelBox> listBox = new List<ModelBox>()
        {
            new ModelBox()
            {
                id = 1,
                nameBox = "Коробка с Iphone 14",
                imageBox = "png file",
                price = 120000,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                idProduct = new ModelProduct()
                {
                    id = 1,
                    name = "Iphone 14",
                    imageName = "None"
                },
            },

            new ModelBox()
            {
                id = 1,
                nameBox = "Коробка с AirPods",
                imageBox = "png file",
                price = 80000,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 40,
                idProduct = new ModelProduct()
                {
                    id = 2,
                    name = "AirPods",
                    imageName = "None"
                },
            },

            new ModelBox()
            {
                id = 1,
                nameBox = "Коробка с PowerBanks",
                imageBox = "png file",
                price = 120000,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                idProduct = new ModelProduct()
                {
                    id = 3,
                    name = "PowerBank",
                    imageName = "None"
                },
            },

        };

    }
}
