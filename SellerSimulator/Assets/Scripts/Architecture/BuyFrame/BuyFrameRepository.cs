using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Architecture.MainDB
{
    class BuyFrameRepository // более менее готовый репозиторий
    {
        private IBuyFrame _local;

        BuyFrameRepository(IBuyFrame local) => _local = local;

        List<ModelsBuyFrame> GetAll()
        {
            var result = _local.GetAll();

            if(result.IsSuccess())
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Exception);
            }
        }

        ModelsBuyFrame BuyItem(int money)
        {
            var result = _local.BuyItem(money);

            if(result.IsSuccess())
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
