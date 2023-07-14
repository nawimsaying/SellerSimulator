using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    interface IMainDbSource // не доработано 
    {
        void getAll();
        void BuyItem(int requestBuy);
    }
}
