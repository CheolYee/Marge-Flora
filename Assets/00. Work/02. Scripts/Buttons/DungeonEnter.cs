using _00._Work._02._Scripts.Manager.FadeManager;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._08._Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Buttons
{
    public class DungeonEnter : MonoBehaviour
    {
        [SerializeField] private Image dungeonIcon;
        [SerializeField] private Image elementIcon;
        [SerializeField] private TextMeshProUGUI dungeonName;
        [SerializeField] private TextMeshProUGUI dungeonDescription;
        [SerializeField] private GameObject dungeonLockPanel;
        [SerializeField] private GameObject nullWeaponPanel;
        
        [SerializeField] private DungeonDataSo dungeonData;

        private void Start()
        {
            dungeonIcon.sprite = dungeonData.dungeonIcon;
            elementIcon.sprite = dungeonData.dungeonElementIcon;
            dungeonName.text = dungeonData.dungeonName;
            dungeonDescription.text = $"권장 단계: {dungeonData.clearRecommendText}\n" +
                                      $"보상: {dungeonData.rewordGold}G";

            if (!string.IsNullOrEmpty(dungeonData.dungeonId))
            {
                bool isUnlocked = SaveManager.IsDungeonCleared(dungeonData.dungeonId);
                dungeonLockPanel.SetActive(!isUnlocked);
            }
        }

        public void EnterDungeon()
        {
            if (GameManager.Instance.selectedWeaponEchoData == null)
            {
                nullWeaponPanel.SetActive(true);
                return;
            }
            
            GameManager.Instance.selectedDungeonData = dungeonData; // 선택한 던전 데이터를 메니저에 등록
            
            FadeManager.Instance.FadeToScene(2); //전투 씬으로 이동
        }
    }
}
