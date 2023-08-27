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
    }
}
