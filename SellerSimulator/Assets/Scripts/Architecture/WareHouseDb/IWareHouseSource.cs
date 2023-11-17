using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouseDb
{
    public interface IWareHouseSource
    {
        void AddPurchasedItem(ModelBox item);
        Result<List<ModelWareHouse>> GetAll();


        
    }
}
