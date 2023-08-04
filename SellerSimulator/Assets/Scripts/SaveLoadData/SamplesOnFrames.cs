using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Architecture.WareHouseDb;

[Serializable]
public class SamplesOnFrames
{
    public int idFrame { get; set; }
    public string sampleName { get; set; }

    public SamplesOnFrames(int _idFrame, string _sample)
    {
        idFrame = _idFrame;
        sampleName = _sample;
    }
}