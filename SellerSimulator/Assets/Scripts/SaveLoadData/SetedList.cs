using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Architecture.WareHouseDb;

[Serializable]
public class SetedList
{
    public int idFrame { get; set; }
    public List<ModelWareHouse> setedList { get; set; }
}