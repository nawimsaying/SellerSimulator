using Assets.Scripts.Architecture.MainDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    public class SellFrameRepository
    {
        private ISellFrameSource _local;

        public SellFrameRepository(ISellFrameSource local) => _local = local;

    
        public List<ModelsSaleFrame> GetAll()
        {
            var result = _local.GetAll();

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
