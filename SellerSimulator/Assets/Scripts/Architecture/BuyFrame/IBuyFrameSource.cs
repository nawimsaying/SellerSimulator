using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    public interface IBuyFrameSource // не доработано 
    {
        Result<List<ModelsBuyFrame>> GetAll();
        Result<string> BuyItem(int productId, int money);
        Result<bool> UnlockItemForGold(int productId, int gold);
    }
}
    