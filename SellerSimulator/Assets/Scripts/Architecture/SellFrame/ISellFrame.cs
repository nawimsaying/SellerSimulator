using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    interface ISellFrame // не доработано, додумать все методы
    {

        Result <ModelsSaleFrame>SellItem(); 
        

    }
}
