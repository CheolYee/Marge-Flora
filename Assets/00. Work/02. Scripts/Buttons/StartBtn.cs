using System;
using _00._Work._02._Scripts.Manager.FadeManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Manager.SoundManager;
using _00._Work._08._Utility;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Buttons
{
    public class StartBtn : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject namePanel;

        private void Start()
        {
            SoundManager.Instance.PlayBgm("Peaceful");
        }

        public void ActiveName()
        {
            if (SaveManager.ExistPlayerName())
            {
                startButton.interactable = false;
                FadeManager.Instance.FadeToScene(1);
            }
            else
            {
                startButton.interactable = false;
                namePanel.SetActive(true);
            }
            
        }
    }
}