using System;
using _00._Work._02._Scripts.Manager.SaveManager;
using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.UI
{
    public class PlayerNameSetUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;

        private void Start()
        {
            nameText.text = SaveManager.LoadPlayerNameData().playerName;
        }
    }
}