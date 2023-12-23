using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InfoBoxCells
{
    private static PlayerData _playerData;


    public static void BarBoxUpdate()
    {
        string GetPlaceStylageMessage(List<Sample> sampleList)
        {
            if (sampleList.Count == 0 || sampleList[0].rackSample == null)
            {
                return "0";
            }

            int countPlaceCells = 0;

            for (int i = 0; i < sampleList.Count; i++)
            {
                for (int j = 0; j < sampleList[i].rackSample.Length; j++)
                {

                    countPlaceCells++;

                }

            }

            return countPlaceCells.ToString();
        }
        string GetEmptyCellCountMessage(List<Sample> sampleList)
        {
            if (sampleList.Count == 0 || sampleList[0].rackSample == null)
            {
                return "0";
            }

            int countEmptyCells = 0;

            for (int i = 0; i < sampleList.Count; i++)
            {
                for (int j = 0; j < sampleList[i].rackSample.Length; j++)
                {
                    if (sampleList[i].rackSample[j] != 0)
                    {
                        countEmptyCells++;
                    }
                }

            }

            return countEmptyCells.ToString();
        }

        List<Sample> sampleList = SaveLoadManager.LoadSampleList();
        int number¿vailableCells = Convert.ToInt32(GetEmptyCellCountMessage(sampleList));
        int numberAllCells = Convert.ToInt32(GetPlaceStylageMessage(sampleList));

        _playerData = PlayerDataHolder.playerData;
        _playerData.ChangeNumber¿vailableCells(number¿vailableCells);
        _playerData.ChangeNumberAllCells(numberAllCells);
    }
}
