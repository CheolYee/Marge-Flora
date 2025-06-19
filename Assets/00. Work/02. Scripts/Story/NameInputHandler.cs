using _00._Work._02._Scripts.Manager.FadeManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Story
{
    public class NameInputHandler : MonoBehaviour
    {
        public TMP_InputField inputField;
        public Button confirmButton;
        public TextMeshProUGUI errorText;

        private void Start()
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            errorText.text = "이름은 한번 설정 시 변경이 불가능합니다";
            gameObject.SetActive(false);
        }

        private void OnConfirmButtonClicked()
        {
            string inputName = inputField.text.Trim();

            if (string.IsNullOrEmpty(inputName))
            {
                errorText.text = "이름이 없습니다";
                return;
            }

            if (inputName.Length > 4)
            {
                errorText.text = "이름은 최대 4자 까지 가능합니다";
                return;
            }
            
            
            PlayerNameData playerNameData = new PlayerNameData
            {
                playerName = inputName
            };
            SaveManager.SavePlayerNameData(playerNameData);
            
            FadeManager.Instance.FadeToScene(1);
        }
    }
}