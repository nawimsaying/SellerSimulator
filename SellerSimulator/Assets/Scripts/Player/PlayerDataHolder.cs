﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerDataHolder : MonoBehaviour
    {
        public static PlayerData playerData;

        void Awake()
        {
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
            // Проверяем, есть ли сохраненные данные в PlayerPrefs
            bool hasSavedData = PlayerPrefs.HasKey("Level") && PlayerPrefs.HasKey("Coins") && PlayerPrefs.HasKey("Gold") && PlayerPrefs.HasKey("Experience") && PlayerPrefs.HasKey("ExperienceToNextLevel") && PlayerPrefs.HasKey("NumberАvailableCells") && PlayerPrefs.HasKey("NumberAllCells");

            if (hasSavedData)
            {
                int level = PlayerPrefs.GetInt("Level");
                int coins = PlayerPrefs.GetInt("Coins");
                int gold = PlayerPrefs.GetInt("Gold");
                int experience = PlayerPrefs.GetInt("Experience");
                int experienceToNextLevel = PlayerPrefs.GetInt("ExperienceToNextLevel");
                int numberАvailableCells = PlayerPrefs.GetInt("NumberАvailableCells");
                int numberAllCells = PlayerPrefs.GetInt("NumberAllCells");


                // Создаем экземпляр PlayerData и загружаем данные из PlayerPrefs
                playerData = new PlayerData(level, coins, gold, experience, experienceToNextLevel, numberАvailableCells, numberAllCells);
            }
            else
            {
                // Если данных нет, создаем новый экземпляр PlayerData с значениями по умолчанию
                playerData = new PlayerData(initLevel: 1, initCoins: 1500000, initExperience: 0, initGold: 25, initExperienceToNextLevel: 5000, numberАvailableCells: 0, numberAllCells: 0); // Lvl, Money, Gold, Exp, nextExp
                //playerData = new PlayerData(initLevel: 200, initCoins: 15000000, initExperience: 100000, initGold: 1000, initExperienceToNextLevel: 100);
                // Сохраняем новые данные по умолчанию в PlayerPrefs
                SavePlayerData();
            }
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt("Level", playerData.Level);
            PlayerPrefs.SetInt("Coins", playerData.Coins);
            PlayerPrefs.SetInt("Gold", playerData.Gold);
            PlayerPrefs.SetInt("Experience", playerData.Experience);
            PlayerPrefs.SetInt("ExperienceToNextLevel", playerData.ExperienceToNextLevel);
            PlayerPrefs.SetInt("NumberАvailableCells", playerData.NumberАvailableCells);
            PlayerPrefs.SetInt("NumberAllCells", playerData.NumberAllCells);
            PlayerPrefs.Save();
        }
    }
}
