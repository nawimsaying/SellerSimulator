using Assets.Scripts.Player;
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
        }


    }
}
