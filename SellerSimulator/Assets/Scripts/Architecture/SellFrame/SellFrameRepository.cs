﻿using Assets.Scripts.Architecture.MainDB;
using Assets.Scripts.Architecture.OnSaleFrame;
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


        public bool PutOnSale(int id, int countProduct)
        {
            var result = _local.PutOnSale(id, countProduct);

            if (result.IsSuccess())
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Exception);
            }
        }

        public bool InstantSale(int idProduct, int countProduct, int currentPrice)
        {
            var result = _local.InstantSale(idProduct, countProduct, currentPrice);

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
