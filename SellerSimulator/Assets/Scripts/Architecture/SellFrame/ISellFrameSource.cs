using Assets.Scripts.Architecture.OnSaleFrame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.WareHouse
{
    public interface ISellFrameSource // не доработано, додумать все методы
    {

        Result <List<ModelsSaleFrame>>GetAll();
        Result<bool> PutOnSale(int idBox, int countProduct);

        Result<bool> InstantSale(int idProduct, int countProduct, int currentPrice);


    }
}
