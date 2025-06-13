using System;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Combat
{
    public class CombatInitializer : MonoBehaviour
    {
        [Header("UI")]
        public Image characterIcon;
        public TextMeshProUGUI characterNameText;
        public Image skillIcon;
        public Image passiveIcon;
        
        [Header("Boss")]
        public GameObject bossPrefab;

        private void Start()
        {
            var character = GameManager.Instance.selectedCharacterData;
            var weaponEcho = GameManager.Instance.selectedWeaponEchoData;
            var dungeonData = GameManager.Instance.selectedDungeonData;

            characterIcon.sprite = character.characterProfile;
            characterNameText.text = character.characterName;
            
            skillIcon.sprite = character.skillData.skillIcon;
            passiveIcon.sprite = character.passiveData.passiveIcon;

            bossPrefab = GameManager.Instance.selectedDungeonData.enemyPrefab;
            
            GameObject boss = Instantiate(bossPrefab, dungeonData.enemySpawnPos, Quaternion.identity);
            
            TimerManager.Instance.StartTimer(dungeonData.timeLimit);
        }
    }
}
