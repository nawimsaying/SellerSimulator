using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouseDb
{
    // Добавить еще нужные методы, поговорить с Даней
    interface IWareHouse
    {
        Result<ModelWareHouse> GetCountBox(int id);
    }
}
