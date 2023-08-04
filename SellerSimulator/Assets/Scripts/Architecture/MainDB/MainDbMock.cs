using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDb
{
    public class MainDbMock
    {
        const int RIGINALLY_COUNT_BOX = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        public List<ModelBox> ListBox { get; } = new List<ModelBox>()
        {
            new ModelBox()
            {
                id =1,
                nameBox = "Коробка с Кнопочным телефоном",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 1,
                    name = "Кнопочный телефон",
                    imageName = "iconIphone",
                    lvlUnlock = 1
                },
            },

            new ModelBox()
            {
                id = 2,
                nameBox = "Коробка со Флешками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 100,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 2,
                    name = "USB Flash 32gb",
                    imageName = "iconIphone",
                    lvlUnlock = 1
                },
            },

            new ModelBox()
            {
                id = 3,
                nameBox = "Коробка с Клавиатурой",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 10,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 3,
                    name = "Клавиатура",
                    imageName = "iconIphone",
                    lvlUnlock = 1,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },

            new ModelBox()
            {
                id = 4,
                nameBox = "Коробка с Кубиком Рубика",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 4,
                    name = "Кубик Рубика",
                    imageName = "iconIphone",
                    lvlUnlock = 2,
                },
            },

            new ModelBox()
            {
                id = 5,
                nameBox = "Коробка с Обычными Часами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 5,
                    name = "Обычные часы",
                    imageName = "iconIphone",
                    lvlUnlock = 4,
                },
            },

            new ModelBox()
            {
                id = 6,
                nameBox = "Коробка с Маленькими Колонками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 6,
                    name = "Маленькая колонка",
                    imageName = "iconIphone",
                    lvlUnlock = 6,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },

            new ModelBox()
            {
                id = 7,
                nameBox = "Коробка со Флешками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 100,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 7,
                    name = "USB Flash 256gb",
                    imageName = "iconIphone",
                    lvlUnlock = 7,
                },
            },

            new ModelBox()
            {
                id = 8,
                nameBox = "Коробка с Андроидами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 8,
                    name = "Андроид",
                    imageName = "iconIphone",
                    lvlUnlock = 7,
                },
            },


            new ModelBox()
            {
                id = 9,
                nameBox = "Коробка с Обычными ноутами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 10,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 9,
                    name = "Обычный ноут",
                    imageName = "iconIphone",
                    lvlUnlock = 10,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },

            new ModelBox()
            {
                id = 10,
                nameBox = "Коробка с Квадратными телевизорами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 10,
                    name = "Квадратный телевизор",
                    imageName = "iconIphone",
                    lvlUnlock = 12,
                },
            },


            new ModelBox()
            {
                id = 11,
                nameBox = "Коробка со Флешками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 100,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 11,
                    name = "USB Flash 1024gb",
                    imageName = "iconIphone",
                    lvlUnlock = 14,
                },
            },


            new ModelBox()
            {
                id = 12,
                nameBox = "Коробка с Обычными компьютерами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 12,
                    name = "Обычный компьютер",
                    imageName = "iconIphone",
                    lvlUnlock = 15,
                },
            },

             new ModelBox()
            {
                id= 13,
                nameBox = "Коробка с Дешевыми микроволновками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 13,
                    name = "Дешевая микроволновка",
                    imageName = "iconIphone",
                    lvlUnlock = 18,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },


            new ModelBox()
            { 
                id= 14,
                nameBox = "Коробка с Сенсорнами часами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 14,
                    name = "Сенсорные часы",
                    imageName = "iconIphone",
                    lvlUnlock = 20,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },


            new ModelBox()
            {
                id = 15,
                nameBox = "Коробка с Наушниками беспроводными",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 15,
                    name = "Наушники беспроводные",
                    imageName = "iconIphone",
                    lvlUnlock = 23
                },
            },


            new ModelBox()
            {
                id = 16,
                nameBox = "Коробка с Обычными телевизорами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 16,
                    name = "Обычный телевизор",
                    imageName = "iconIphone",
                    lvlUnlock = 26,
                },
            },


            new ModelBox()
            {
                id = 17,
                nameBox = "Коробка с Полу-Игровыми компьютерами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 17,
                    name = "Полу-Игровой Компьютер",
                    imageName = "iconIphone",
                    lvlUnlock = 29,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },


            new ModelBox()
            {
                id = 18,
                nameBox = "Коробка с Холодильниками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 18,
                    name = "Холодильник",
                    imageName = "iconIphone",
                    lvlUnlock = 32,
                },
            },

            new ModelBox()
            {
                id = 19,
                nameBox = "Коробка с Айфонами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 19,
                    name = "Айфон",
                    imageName = "iconIphone",
                    lvlUnlock = 35,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },


            new ModelBox()
            {
                id = 20,
                nameBox = "Коробка со Стиральными Машинами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 20,
                    name = "Стиральная машина",
                    imageName = "iconIphone",
                    lvlUnlock = 37,
                },
            },


            new ModelBox()
            {
                id = 21,
                nameBox = "Коробка с Игровыми Наушниками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 20,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 21,
                    name = "Игровые Наушники",
                    imageName = "iconIphone",
                    lvlUnlock = 39,
                },
            },


            new ModelBox()
            {
                id = 22,
                nameBox = "Коробка с Большими Колонками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 22,
                    name = "Большая колонка",
                    imageName = "iconIphone",
                    lvlUnlock = 42
                },
            },

            new ModelBox()
            {
                id = 23,
                nameBox = "Коробка с Дорогими клавиатурами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 10,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 23,
                    name = "Дорогая клавиатура",
                    imageName = "iconIphone",
                    lvlUnlock = 45,
                },
            },

            new ModelBox()
            {
                id = 24,
                nameBox = "Коробка с Дорогими микроволновками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 24,
                    name = "Дорогая микроволновка",
                    imageName = "iconIphone",
                    lvlUnlock = 48,
                },
            },


            new ModelBox()
            {
                id = 25,
                nameBox = "Коробка с Игровыми ноутбуками",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 10,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 25,
                    name = "Игровой ноутбук",
                    imageName = "iconIphone",
                    lvlUnlock = 51,
                    lockForGold = true, 
                    goldenPrice = 20
                },
            },

            new ModelBox()
            {
                id = 26,
                nameBox = "Коробка с Игровыми Компьютерами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 26,
                    name = "Игровой Компьютер",
                    imageName = "iconIphone",
                    lvlUnlock = 54,
                },
            },


            new ModelBox()
            {
                id = 27,
                nameBox = "Коробка с Большой плазмой",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 5,
                sizeBox = "Big",
                idProduct = new ModelProduct()
                {
                    id = 27,
                    name = "Большая плазма",
                    imageName = "iconIphone",
                    lvlUnlock = 57,
                },
            },


            new ModelBox()
            {
                id = 28,       
                nameBox = "Коробка с Золотыми Часами",
                imageBox = "png file",
                price = 1200,
                countBox = RIGINALLY_COUNT_BOX,
                countProduct = 30,
                sizeBox = "Small",
                idProduct = new ModelProduct()
                {
                    id = 28,
                    name = "Золотые часы",
                    imageName = "iconIphone",
                    lvlUnlock = 60,
                    lockForGold = true,
                    goldenPrice = 20
                },
            },

        };

    }
}
