using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Marge.DragDrop
{
    public class EacoCore : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;
        
        //드래그 시작
        public void OnBeginDrag(PointerEventData eventData)
        {
            //올바르지 않은 에코 코어에 드롭했을 때 돌아올 위치 저장
            _startPosition = this.transform.position;
            //드래그 시작 시 레이케스트 타겟을 꺼줘야 오류 발생 X
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //현재 드래그 중인 좌표를 저장해 오브젝트가 손을 따라갈 수 있게 오브젝트의 좌표로 설정
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
            currentPos.z = 90f;
            currentPos.y -= 160f;
            this.transform.position = currentPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // 손님 오브젝트에서 음식 오브젝트에 대한 처리를 하고 오브젝트를 제거한다
            // 제거되지 않았다면 올바르지 않은 곳에 드롭된 것이기 때문에 원래 위치로 돌려준다
            this.transform.position = _startPosition;
            // 레이캐스트 타겟도 원래대로 돌려준다
            GetComponent<Image>().raycastTarget = true;
        }
    }
}
