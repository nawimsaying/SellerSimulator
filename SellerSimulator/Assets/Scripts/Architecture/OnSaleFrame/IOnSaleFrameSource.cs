using Assets.Scripts.Architecture.DataBases.AdvertisingDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.OnSaleFrame
{
    public interface IOnSaleFrameSource
    {
        Result<string> CancelSell(int idSell);
        Result<List<ModelsOnSaleFrame>> GetAll();

        Result<List<ModelAdvertising>> GetAllAds();
        Result<bool> SetBuffForItem(ModelsOnSaleFrame item, ModelAdvertising ads); 
        Result<bool> SaveDataList(List<ModelsOnSaleFrame> newListOnSaleFrame);
    }
}
