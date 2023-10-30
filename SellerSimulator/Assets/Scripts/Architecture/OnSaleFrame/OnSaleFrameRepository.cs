using Assets.Scripts.Architecture.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.OnSaleFrame
{
    public class OnSaleFrameRepository
    {

        private IOnSaleFrameSource _local;

        public OnSaleFrameRepository(IOnSaleFrameSource local)=> _local = local;

        public List<ModelsOnSaleFrame> GetAll()
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
