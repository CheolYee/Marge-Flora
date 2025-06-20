using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _00._Work._02._Scripts.UI
{
    public class StartLogoUI : MonoBehaviour
    {
        public RectTransform logo;
        public Vector2 startPosition = new Vector2(0, 800);   // 시작 위치
        public Vector2 targetPosition = new Vector2(0, 0);    // 최종 위치
        
        public GameObject startButton;

        public float duration = 1.5f;     // 내려오는 시간
        public Ease ease = Ease.OutBounce; // 이징 설정

        void Start()
        {
            logo.anchoredPosition = startPosition;  // 시작 위치 세팅
            logo.DOAnchorPos(targetPosition, duration)
                .SetEase(ease).OnComplete(() => StartCoroutine(ActiveStartBtn()));
        }

        public IEnumerator ActiveStartBtn()
        {
            yield return new WaitForSeconds(0.5f);
            startButton.SetActive(true);
        }
    }
}