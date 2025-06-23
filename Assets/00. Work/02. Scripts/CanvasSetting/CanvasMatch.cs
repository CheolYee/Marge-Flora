using _00._Work._08._Utility;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.CanvasSetting
{
    public class CanvasMatch : MonoBehaviour
    {
        private CanvasScaler _canvasScaler;

        
        //Default 해상도 비율
        float _fixedAspectRatio = 9f / 16f; 

    //현재 해상도의 비율
        float _currentAspectRatio = (float)Screen.width / (float)Screen.height;

        private void Awake()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        void Start()
        {
            //현재 해상도 가로 비율이 더 길 경우
            if (_currentAspectRatio > _fixedAspectRatio) _canvasScaler.matchWidthOrHeight = 1;       
            //현재 해상도의 세로 비율이 더 길 경우
            else if (_currentAspectRatio < _fixedAspectRatio) _canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
