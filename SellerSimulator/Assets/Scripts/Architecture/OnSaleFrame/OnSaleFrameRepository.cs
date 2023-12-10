using Assets.Scripts.Architecture.DataBases.AdvertisingDb;
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

        public List<ModelAdvertising> GetAllAds()
        {
            var result = _local.GetAllAds();

            if (result.IsSuccess())
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Exception);
            }
        }

        public bool SetBuffForItem(ModelsOnSaleFrame item, ModelAdvertising ads)
        {
            var result = _local.SetBuffForItem(item, ads);

            if (result.IsSuccess())
            {
                return result.Data;
            }
            else
            {
                throw new Exception(result.Exception);
            }
        }

        public bool SaveDataList(List<ModelsOnSaleFrame> list)
        {
            var result = _local.SaveDataList(list);

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
