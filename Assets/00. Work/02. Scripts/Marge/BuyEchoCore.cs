using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SlotManager;
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

        private void OnEnable()
        {
            Logging.Log("데이터 가져오기");
            firstStageSo = GameManager.Instance.selectedCharacterData.firstEchoData;
        }

        //에코 생성 버튼 누를 시 실행
        public void SpawnNewEcho()
        {
            Slot emptySlot = slotManager.GetEmptySlot();

            if (emptySlot == null)
            {
                Debug.LogWarning("빈 슬롯이 없습니다!");
                return;
            }

            GameObject newItem = Instantiate(echoCorePrefab, emptySlot.transform);
            newItem.transform.localPosition = Vector3.zero;

            EchoCore core = newItem.GetComponent<EchoCore>();
            core.SetEchoData(firstStageSo);
        }
    }
}