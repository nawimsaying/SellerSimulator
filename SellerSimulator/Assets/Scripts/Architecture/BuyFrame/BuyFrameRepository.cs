﻿using System;
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
        private IBuyFrameSource _local;

        public BuyFrameRepository(IBuyFrameSource local) => _local = local;

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

        public string BuyItem(int id,int money)
        {
            var result = _local.BuyItem(id,money);

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
