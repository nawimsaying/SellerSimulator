using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    public interface IBuyFrame // не доработано 
    {
        Result<List<ModelsBuyFrame>> GetAll();
        Result<ModelsBuyFrame> BuyItem(int money);
    }
}
