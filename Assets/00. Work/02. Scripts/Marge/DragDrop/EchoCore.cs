using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Marge.DragDrop
{
    public class EchoCore : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // 현재 드래그 되고 있는 오브젝트를 저장하기 위함
        public static GameObject draggingObject;
        
        //현재 드래그 중인 오브젝트의 데이터를 담은 SO를 가져오기 위함
        public static EchoCoreSo echoCoreData;

        // 슬롯이 아닌 곳에 드롭했을 때 다시 돌아오게 하기 위한 시작 좌표 저장용
        private Vector3 _startPosition;
        
        [SerializeField] private EchoCoreSo currentEchoData;
        
        // 슬롯이 아닌 다른 오브젝트에 드롭했을 경우 원상복구할 부모 백업용
        [SerializeField] private Transform onDragParent;
        
        [HideInInspector] public Transform startParent;
        
        [SerializeField] private Image currentImage;

        const string MainPanel = "MainPanel";
        
        private void OnEnable()
        {
            onDragParent = GameObject.FindGameObjectWithTag(MainPanel).transform;
            currentImage = gameObject.GetComponent<Image>();
            currentImage.sprite = currentEchoData.echoSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //드래그가 시작될 때 대상 게임 오브젝트를 현재 드래그 오브젝트에 할당
            draggingObject = gameObject;
            echoCoreData = currentEchoData;
            
            
            //백업용 위치와 부모 트랜스폼 저장
            _startPosition = transform.position;
            startParent = transform.parent;
            
            //드래그를 정상적으로 감지하기 위해 RectTransform을 무시
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            
            //드래그 시작할 때 부모 transform을 변경
            transform.SetParent(onDragParent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //드래그 중에는 드래그 대상 마우스 위치에 고정
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //드래그 대상을 지우고 드래그 대상의 이벤트 감지를 허용
            draggingObject = null;
            echoCoreData = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            
            //드랍 이벤트에 따라 부모가 변경되지 않고
            //이동중에 할당 되었던 부모의 transform과 같다면
            //드래그 대상과 부모를 원상복구시킨다
            if (transform.parent == onDragParent)
            {
                transform.position = _startPosition;
                transform.SetParent(startParent);
            }
        }
        
        public EchoCoreSo GetCurrentData()
        {
            return currentEchoData;
        }

        public void SetEchoData(EchoCoreSo newData)
        {
            currentEchoData = newData;
            currentImage.sprite = currentEchoData.echoSprite;
        }
    }
}
