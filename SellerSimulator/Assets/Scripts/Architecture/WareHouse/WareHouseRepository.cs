using Assets.Scripts.Architecture.MainDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    class WareHouseRepository
    {
        private IWareHouse _local;

        WareHouseRepository(IWareHouse local) => _local = local;

        void getAll()
        {
            if (_local != null) // Создать класс Result для этого дела 
            {
                _local.getAll();
            }
        }

        void sellItem()
        {
            if ( _local != null) // Создать класс Result для этого дела 
            {
                _local.SellItem();
            }
        }
    }
}
