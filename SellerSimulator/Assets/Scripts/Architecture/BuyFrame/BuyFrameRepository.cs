using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Architecture.MainDB
{
    public class BuyFrameRepository // более менее готовый репозиторий
    {
        private IBuyFrame _local;

        public BuyFrameRepository(IBuyFrame local) => _local = local;

        public List<ModelsBuyFrame> GetAll()
        {
            Debug.Log("Попал");
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

        public ModelsBuyFrame BuyItem(int money)
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
