using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.TimerManager
{
    public class TimerTextFinder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private void OnEnable()
        {
            if (TimerManager.Instance != null)
                TimerManager.Instance.timerText = timerText;
        }
    }
}
