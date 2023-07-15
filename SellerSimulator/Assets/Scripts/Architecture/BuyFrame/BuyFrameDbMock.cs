using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Architecture.MainDB
{
    class BuyFrameDbMock : IBuyFrame // переделать класс
    {
        const int originallyCountBox = 1; // При покупки 1 товара, всегда вместе с ним идет 1 коробка

        List<ModelsBuyFrame> listBox = new List<ModelsBuyFrame>();
      
           


        Result<ModelsBuyFrame> IBuyFrame.BuyItem(int money)
        {
            throw new NotImplementedException();
        }

        Result<List<ModelsBuyFrame>> IBuyFrame.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
