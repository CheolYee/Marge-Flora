using _00._Work._02._Scripts.Manager.CombatManager;
using _00._Work._02._Scripts.Manager.FadeManager;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.MoneyManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using _00._Work._02._Scripts.Save;
using _00._Work._02._Scripts.Story.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00._Work._02._Scripts.Combat.Finisher
{
    public class DungeonFinish : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject successPanel;
        [SerializeField] private GameObject failPanel;

        private void Start()
        {
            mainPanel.SetActive(false);
            successPanel.SetActive(false);
            failPanel.SetActive(false);
            TimerManager.Instance.OnTimerFinished += Finish;
        }

        private void OnDisable()
        {
            TimerManager.Instance.OnTimerFinished -= Finish;
        }

        private void Finish()
        {
            mainPanel.SetActive(true);
            if (CombatManager.Instance.bossIsDead) ShowSuccessUI();
            else ShowFailUI();
        }
        
        private void ShowSuccessUI()
        {
            successPanel.SetActive(true);
        }

        private void ShowFailUI()
        {
            failPanel.SetActive(true);
        }

        public void OnSuccessReturnToMain()
        {
            MoneyManager.Instance.AddMoney(GameManager.Instance.selectedDungeonData.rewordGold);
            
            if (GameManager.Instance.selectedDungeonData.hasUnlock) 
                SaveManager.Instance.UnlockCharacter(GameManager.Instance.selectedDungeonData.unlockId);


            WhatIsCurrentStory(GameManager.Instance.selectedDungeonData.story);
            
            if (GameManager.Instance.selectedDungeonData.hasStory)
            {
                StorySaveData saveData = SaveManager.LoadStoryData();
                foreach (var currentStoryID in saveData.saveStoryIds)
                {
                    if (currentStoryID == GameManager.Instance.selectedStoryData.storyId) // 만약 본 스토리 리스트에 있으면
                    {
                        FadeManager.Instance.FadeToScene(1); // 스토리 스킵하고 메뉴
                        return;
                    }
                }
            }
            
            FadeManager.Instance.FadeToScene(
                GameManager.Instance.selectedDungeonData.hasStory ? 3 : 1); //스토리가 있는 씬이면 이동
        }

        private void WhatIsCurrentStory(StoryContainerSo storyContainerData)
        {
            GameManager.Instance.selectedStoryData = storyContainerData;
        }
        
        public void OnFailReturnToMain()
        {
            FadeManager.Instance.FadeToScene(GameManager.Instance.selectedDungeonData.hasStory ? 3 : 1); // 만약 스토리가 없다면 다시 메인 씬으로
        }
    }
}
