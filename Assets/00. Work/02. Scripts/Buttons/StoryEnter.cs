using _00._Work._02._Scripts.Manager.FadeManager;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Story.SO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Buttons
{
    public class StoryEnter : MonoBehaviour
    {
        [SerializeField] private Image storyIcon;
        [SerializeField] private TextMeshProUGUI storyName;
        [SerializeField] private TextMeshProUGUI storyDescription;
        
        [SerializeField] private StoryContainerSo storyContainerData;

        private void Start()
        {
            storyIcon.sprite = storyContainerData.storyIcon;
            storyName.text = storyContainerData.storyNumber;
            storyDescription.text = $"{storyContainerData.storyName}";
        }

        public void EnterStory()
        {
            GameManager.Instance.selectedStoryData = storyContainerData; // 선택한 스토리 데이터를 메니저에 등록
            
            FadeManager.Instance.FadeToScene(3); //스토리 씬으로 이동
        }
    }
}