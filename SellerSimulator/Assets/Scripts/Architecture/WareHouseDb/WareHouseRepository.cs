using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Assets.Scripts.Architecture.WareHouseDb
{
    class WareHouseRepository
    {
        private IWareHouse _local;

        WareHouseRepository(IWareHouse local) => _local = local;

        ModelWareHouse GetCountBox(int id)
        {
            var result = _local.GetCountBox(id);

            if (result.IsSuccess())
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Exception);
            }
        }
    }
}
