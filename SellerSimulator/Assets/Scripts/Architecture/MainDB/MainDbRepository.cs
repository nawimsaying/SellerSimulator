using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Architecture.MainDB
{
    class MainDbRepository
    {
        private IMainDbSource _local;

        MainDbRepository(IMainDbSource local) => _local = local;

        void getAll()
        {
            if (_local != null) // Создать класс Result для этого дела 
            {
                _local.getAll();
            }
        }

        void BuyItem(int requestBuy)
        {
            if (_local != null) // Создать класс Result для этого дела 
            {
                _local.BuyItem(requestBuy);
            }
        }
    }
}
