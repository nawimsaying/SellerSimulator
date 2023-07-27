using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerData
    {
        public int Level { get; private set; }
        public int Coins { get; private set; }
        public int Gold { get; private set; }
        public int Experience { get; private set; }
        public int ExperienceToNextLevel { get; private set; }

        public PlayerData(int initLevel, int initCoins, int initGold, int initExperience, int initExperienceToNextLevel)
        {
            Level = initLevel;
            Coins = initCoins;
            Gold = initGold;
            Experience = initExperience;
            ExperienceToNextLevel = initExperienceToNextLevel;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
            SavePlayerData();
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            SavePlayerData();
        }


        public void SetCoins(int amount)
        {
            Coins = amount;
            SavePlayerData();
        }

        // Метод для добавления опыта
        public void AddExperience(int amount)
        {
            Experience += amount;

            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }

            SavePlayerData();
        }

        private void LevelUp()
        {
            Level++;
            Experience -= ExperienceToNextLevel;
            ExperienceToNextLevel += 50;

            SavePlayerData();
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt("Level", Level);
            PlayerPrefs.SetInt("Coins", Coins);
            PlayerPrefs.SetInt("Gold", Gold);
            PlayerPrefs.SetInt("Experience", Experience);
            PlayerPrefs.SetInt("ExperienceToNextLevel", ExperienceToNextLevel);
            PlayerPrefs.Save();
        }
    }
}
