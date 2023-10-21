using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.OnSaleFrame
{
    public class OnSaleFrameDbMock : IOnSaleFrameSource
    {
        public List<ModelsOnSaleFrame> onSaleProduct = new List<ModelsOnSaleFrame>(); // stores the products on display for sale
       
        

        public Result<string> CancelSell(int idSell)
        {
            throw new NotImplementedException();
        }
    }
}
