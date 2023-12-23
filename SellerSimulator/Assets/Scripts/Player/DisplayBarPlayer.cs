using Assets.Scripts.Player;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class DisplayBarPlayer : MonoBehaviour 
    {
        [SerializeField] private TextMeshProUGUI textMoney;
        [SerializeField] private TextMeshProUGUI textGold;
        [SerializeField] private TextMeshProUGUI textLevel;
        [SerializeField] private TextMeshProUGUI textBox;

        [SerializeField] private Image expBar;

        void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            PlayerData playerData = PlayerDataHolder.playerData;

            // Обновляем текстовые элементы на основе данных из PlayerData
            textMoney.text = playerData.Coins.ToString();
            textGold.text = playerData.Gold.ToString();
            textLevel.text = playerData.Level.ToString();
            string numberАvailableCells = playerData.NumberАvailableCells.ToString();
            string numberAllCells = playerData.NumberAllCells.ToString();
            textBox.text = $"{numberАvailableCells}/{numberAllCells}";


            // Обновляем полоску опыта
            if (expBar != null)
            {
                int currentExp = playerData.Experience;
                int neededExp = playerData.ExperienceToNextLevel;

                float currentFill = (float)currentExp / (float)neededExp;
                expBar.fillAmount = currentFill;
                Debug.Log(Convert.ToString(expBar.fillAmount));
            }
        }
    }
}
