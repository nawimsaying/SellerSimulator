using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerDataHolder : MonoBehaviour
    {
        public static PlayerData playerData;

        void Start()
        {
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
            // Проверяем, есть ли сохраненные данные в PlayerPrefs
            bool hasSavedData = PlayerPrefs.HasKey("Level") && PlayerPrefs.HasKey("Coins") && PlayerPrefs.HasKey("Gold") && PlayerPrefs.HasKey("Experience") && PlayerPrefs.HasKey("ExperienceToNextLevel");

            if (hasSavedData)
            {
                int level = PlayerPrefs.GetInt("Level");
                int coins = PlayerPrefs.GetInt("Coins");
                int gold = PlayerPrefs.GetInt("Gold");
                int experience = PlayerPrefs.GetInt("Experience");
                int experienceToNextLevel = PlayerPrefs.GetInt("ExperienceToNextLevel");

                // Создаем экземпляр PlayerData и загружаем данные из PlayerPrefs
                playerData = new PlayerData(level, coins, gold, experience, experienceToNextLevel);
            }
            else
            {
                // Если данных нет, создаем новый экземпляр PlayerData с значениями по умолчанию
                playerData = new PlayerData(20, 1000000, 1000, 0, 100); // Lvl, Money, Gold, Exp, nextExp

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
            PlayerPrefs.Save();
        }
    }
}
