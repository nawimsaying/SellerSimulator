using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Architecture.WareHouseDb;

[Serializable]
public class SamplesOnFrames
{
    public int idFrame { get; set; }
    public GameObject sample { get; set; }

    public SamplesOnFrames(int _idFrame, GameObject _sample)
    {
        idFrame = _idFrame;
        sample = _sample;
    }
}