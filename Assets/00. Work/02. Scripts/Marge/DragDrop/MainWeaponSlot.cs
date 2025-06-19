using _00._Work._02._Scripts.Buttons;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00._Work._02._Scripts.Marge.DragDrop
{
    public class MainWeaponSlot : MonoBehaviour, IDropHandler
    {
        private GameObject EchoCoreCounter()
        {
            return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        }
        
        //드롭 시 발생 이벤트
        public void OnDrop(PointerEventData eventData)
        {
            GameObject existingItem = EchoCoreCounter();
            
            //슬롯이 비어있다면 아이템을 자식으로 위치변경
            if (EmptyEchoCore()) return;

            EchoCoreSo draggingData = EchoCore.echoCoreData;
            EchoCore draggingCore = EchoCore.draggingObject?.GetComponent<EchoCore>();
            EchoCore existingCore = existingItem.GetComponent<EchoCore>();

            if (draggingData == null || draggingCore == null || existingCore == null)
            {
                EmptyEchoCore();
                Debug.LogWarning("드래그/슬롯 아이템 중 EchoCore or EchoCoreSo 없음");
            }
        }

        private void Update()
        {
            EchoCore core = EchoCoreCounter()?.GetComponent<EchoCore>();

            if (core != null)
            {
                var data = core.GetCurrentData();
                UIContainer.Instance.weaponName.text = data.coreName;
                UIContainer.Instance.weaponDesc.text = $"등급: {data.growthCount}\n" +
                                                       $"공격력: {data.damage}";
            }
            else
            {
                UIContainer.Instance.weaponName.text = "";
                UIContainer.Instance.weaponDesc.text = "";
            }
        }

        //자식 오브젝트가 있나 없나 검사 후 있으면 자식 리턴, 없으면 null 리턴
        //null이 리턴되었으면 슬롯이 비었다는 뜻이니 드래그된 오브젝트를 집어넣어준다.
        private bool EmptyEchoCore()
        {
            if (EchoCoreCounter() == null)
            {
                EchoCore.draggingObject.transform.SetParent(transform);
                EchoCore.draggingObject.transform.position = transform.position;
                return true;
            }
            
            return false;
        }
    }
}
