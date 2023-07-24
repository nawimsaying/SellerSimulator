using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDb.ModelsDb
{
    public class ModelBox
    {
        public ulong id;
        public string nameBox;
        public string imageBox;
        public int price;
        public int countBox;
        public int countProduct;
        public string sizeBox;
        public ModelProduct idProduct;

    }
}
