using System;
using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Button
{
    public class DungeonEnter : MonoBehaviour
    {
        [SerializeField] private Image dungeonIcon;
        [SerializeField] private Image elementIcon;
        [SerializeField] private TextMeshProUGUI dungeonName;
        [SerializeField] private TextMeshProUGUI dungeonDescription;
        
        [SerializeField] private DungeonDataSo dungeonData;

        private void Start()
        {
            dungeonIcon.sprite = dungeonData.dungeonIcon;
            elementIcon.sprite = dungeonData.dungeonElementIcon;
            dungeonName.text = dungeonData.dungeonName;
            dungeonDescription.text = $"권장 단계: {dungeonData.clearRecommendText}\n" +
                                      $"보상: {dungeonData.rewordGold}G";
        }

        public void EnterDungeon()
        {
            GameManager.Instance.selectedDungeonData = dungeonData; // 선택한 던전 데이터를 메니저에 등록
            
            SceneManager.LoadScene(2); //전투 씬으로 이동
        }
    }
}
