using _00._Work._02._Scripts.Buttons;
using _00._Work._02._Scripts.Combat.Effect;
using _00._Work._02._Scripts.Manager.SoundManager;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._08._Utility;
using Coffee.UIExtensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00._Work._02._Scripts.Marge.DragDrop
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private GameObject effectPrefab;
        //슬롯에 아이템(자식 오브젝트)가 있으면 자식 1개를 리턴
        //슬롯에 아이템(자식 오브젝트)가 없으면 null 리턴
        private GameObject EchoCoreCounter()
        {
            return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        }
        
        //드롭 시 발생 이벤트
        public void OnDrop(PointerEventData eventData)
        {
            GameObject existingItem = EchoCoreCounter();
            
            //슬롯이 비어있다면 아이템을 자식으로 위치변경
            //슬롯이 차있다면 종료
            if (EmptyEchoCore()) return;

            EchoCoreSo draggingData = EchoCore.EchoCoreData;
            EchoCore draggingCore = EchoCore.DraggingObject?.GetComponent<EchoCore>();
            EchoCore existingCore = existingItem.GetComponent<EchoCore>();

            if (draggingData == null || draggingCore == null || existingCore == null)
            {
                EmptyEchoCore();
                Debug.LogWarning("드래그/슬롯 아이템 중 EchoCore or EchoCoreSo 없음");
                return;
            }

            EchoCoreSo existingData = existingCore.GetCurrentData();
            
            if (draggingData.growthCount == existingData.growthCount)
            {
                // 다음 성장 단계 존재 확인
                if (draggingData.nextEchoData != null)
                {
                    if (effectPrefab != null)
                    {
                        SoundManager.Instance.PlaySfx("Marge");
                        Effect();
                    }
                    // 기존 아이템에 nextItem 적용
                    existingCore.SetEchoData(draggingData.nextEchoData);
                    
                    // 드래그된 아이템 제거 (혹은 풀로 반환)
                    Destroy(EchoCore.DraggingObject);
                    EchoCore.DraggingObject = null;
                }
                else
                {
                    Logging.Log("더 이상 성장할 수 없습니다.");
                    EmptyEchoCore(); // 그냥 돌려보냄
                }
            }
            else
            {
                Debug.Log("등급이 같지 않습니다.");
                EmptyEchoCore(); // 등급이 다르면 그냥 복귀
            }
        }

        private void Effect()
        {
            UIParticle effect = Instantiate(effectPrefab, transform).GetComponent<UIParticle>();
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                UIContainer.Instance.effectParent, screenPos, Camera.main, out Vector2 localPos);
            effect.GetComponent<RectTransform>().anchoredPosition = localPos;
            effect.transform.SetParent(UIContainer.Instance.effectParent, false);
            effect.transform.localScale = Vector3.one;
            effect.Play();
            Destroy(effect, 1);
        }

        //자식 오브젝트가 있나 없나 검사 후 있으면 자식 리턴, 없으면 null 리턴
        //null이 리턴되었으면 슬롯이 비었다는 뜻이니 드래그된 오브젝트를 집어넣어준다.
        private bool EmptyEchoCore()
        {
            if (EchoCoreCounter() == null)
            {
                EchoCore.DraggingObject.transform.SetParent(transform);
                EchoCore.DraggingObject.transform.position = transform.position;
                return true;
            }
            
            return false;
        }
        
        //빈 슬롯이 있는지 화인하기 위함
        public bool HasItem()
        {
            return transform.childCount > 0;
        }
    }
}
