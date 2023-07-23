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
        private IWareHouseSource _local;

        WareHouseRepository(IWareHouseSource local) => _local = local;

       
    }
}
