using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00._Work._02._Scripts.Marge.DragDrop
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        //슬롯에 아이템(자식 오브젝트)가 있으면 자식 1개를 리턴
        //슬롯에 아이템(자식 오브젝트)가 없으면 null 리턴
        private GameObject Icon()
        {
            return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        }
        
        //드롭 시 발생 이벤트
        public void OnDrop(PointerEventData eventData)
        {
            GameObject existingItem = Icon();
            
            //슬롯이 비어있다면 아이템을 자식으로 위치변경
            if (ReturnEchoCore() == false) return;

            EchoCoreSo draggingData = EchoCore.EchoCoreData;
            EchoCore draggingCore = EchoCore.DraggingObject?.GetComponent<EchoCore>();
            EchoCore existingCore = existingItem.GetComponent<EchoCore>();

            if (draggingData == null || draggingCore == null || existingCore == null)
            {
                ReturnEchoCore();
                Debug.LogWarning("드래그/슬롯 아이템 중 EchoCore or EchoCoreSo 없음");
                return;
            }

            EchoCoreSo existingData = existingCore.GetCurrentData();
            
            if (draggingData.growthCount == existingData.growthCount)
            {
                // 다음 성장 단계 존재 확인
                if (draggingData.nextEchoData != null)
                {
                    // 기존 아이템에 nextItem 적용
                    existingCore.SetEchoData(draggingData.nextEchoData);
                    
                    // 드래그된 아이템 제거 (혹은 풀로 반환)
                    Destroy(EchoCore.DraggingObject);
                    EchoCore.DraggingObject = null;
                }
                else
                {
                    Debug.Log("더 이상 성장할 수 없습니다.");
                    ReturnEchoCore(); // 그냥 돌려보냄
                }
            }
            else
            {
                Debug.Log("등급이 같지 않습니다.");
                ReturnEchoCore(); // 등급이 다르면 그냥 복귀
            }
        }

        private bool ReturnEchoCore()
        {
            if (Icon() == null)
            {
                EchoCore.DraggingObject.transform.SetParent(transform);
                EchoCore.DraggingObject.transform.position = transform.position;
                return false;
            }
            
            return true;
        }
    }
}
