using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.WareHouseDb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseDbMock : IWareHouse
{

    //Лист в котором будет хранится купленные коробки

    Result<ModelWareHouse> IWareHouse.GetCountBox(int id)
    {
        throw new System.NotImplementedException();
    }
}
