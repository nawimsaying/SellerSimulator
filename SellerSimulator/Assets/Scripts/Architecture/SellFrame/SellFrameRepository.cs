using Assets.Scripts.Architecture.MainDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    class SellFrameRepository
    {
        private ISellFrameSource _local;

        SellFrameRepository(ISellFrameSource local) => _local = local;

    
        ModelsSaleFrame SellItem()
        {
            var result = _local.SellItem();

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
