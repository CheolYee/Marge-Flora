using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.MoneyManager;
using _00._Work._02._Scripts.Manager.SlotManager;
using _00._Work._02._Scripts.Manager.SoundManager;
using _00._Work._02._Scripts.Marge.DragDrop;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge
{
    public class BuyEchoCore : MonoBehaviour
    {
        //슬롯 매니저 불러오기
        [SerializeField] private SlotManager slotManager;
        //에코 프리팹 불러오기
        [SerializeField] private GameObject echoCorePrefab;
        //처음 소환 단계의 So 가져오기
        [SerializeField] private EchoCoreSo firstStageSo;

        private int _buyMoney;

        private void OnEnable()
        {
            firstStageSo = GameManager.Instance.selectedCharacterData.firstEchoData;
            _buyMoney = GameManager.Instance.selectedCharacterData.buyMoney;
        }

        //에코 생성 버튼 누를 시 실행
        public void SpawnNewEcho()
        {
            if (MoneyManager.Instance.Money >= _buyMoney)
            {
                MoneyManager.Instance.SpendMoney(_buyMoney);
                Slot emptySlot = slotManager.GetEmptySlot();

                if (emptySlot == null)
                {
                    return;
                }

                SoundManager.Instance.PlaySfx("Buy");
                GameObject newItem = Instantiate(echoCorePrefab, emptySlot.transform);
                newItem.transform.localPosition = Vector3.zero;

                EchoCore core = newItem.GetComponent<EchoCore>();
                core.SetEchoData(firstStageSo);
            }
        }
    }
}