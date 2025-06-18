using _00._Work._02._Scripts.Manager.SoundManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Buttons
{
    public class MenuUIBtn : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button mainButton;
        public Slider bgmSlider;
        public Slider sfxSlider;
        
        private bool _isPressedEsc = false;

        private void Start()
        {
            bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            sfxSlider.value = SoundManager.Instance.GetSfxVolume();

            bgmSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetBgmVolume(v));
            sfxSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetSfxVolume(v));
            menu.SetActive(false);
        }

        public void MainMenu()
        {
            menu.SetActive(true);
            mainButton.gameObject.SetActive(false);
            TimerManager.Instance?.StopTimer();
            Time.timeScale = 0;
        }
        
        public void ContinueButton()
        {
            _isPressedEsc = !_isPressedEsc;
            
            Time.timeScale = 1;
            mainButton.gameObject.SetActive(true);
            menu.SetActive(false);
            TimerManager.Instance?.RestartTimer();
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}