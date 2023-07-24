using Assets.Scripts.Architecture.MainDb.ModelsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Assets.Scripts.Architecture.WareHouseDb
{
    public class WareHouseRepository
    {
        private IWareHouseSource _local;

        public WareHouseRepository(IWareHouseSource local) => _local = local;

       

        public List<ModelWareHouse> GetAll()
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
