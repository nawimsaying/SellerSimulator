using Assets.Scripts.Architecture;
using Assets.Scripts.Architecture.WareHouseDb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseDbMock : IWareHouse
{

    //���� � ������� ����� �������� ��������� �������

    Result<ModelWareHouse> IWareHouse.GetCountBox(int id)
    {
        throw new System.NotImplementedException();
    }
}
