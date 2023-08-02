using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Architecture.WareHouseDb;

[Serializable]
public class Sample
{
    public int idFrame;
    public ulong[] rackSample;

    public Sample(int currentCameraPosition, int idSample)
    {
        if (idSample == 0)
        {
            idFrame = currentCameraPosition;

            rackSample = new ulong[21];

            for (int i = 0; i < 21; i++)
            {
                rackSample[i] = 0;
            }
        }
        else
        {
            idFrame = currentCameraPosition;

            rackSample = new ulong[10];

            for (int i = 0; i < 10; i++)
            {
                rackSample[i] = 0;
            }
        }
    }
}