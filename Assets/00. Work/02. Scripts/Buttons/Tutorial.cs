using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Buttons
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private List<Sprite> tutoImgs;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private Image currentTutoImage;

        private int _currentTutoIndex;
        private void Start()
        {
            _currentTutoIndex = 0;
            currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
        }

        public void OpenTutorial()
        {
            if (tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }
            else
            {
                tutorialPanel.SetActive(true);
            }
        }

        public void NextTutorial()
        {
            if (_currentTutoIndex < tutoImgs.Count -1)
            {
                _currentTutoIndex++;
                currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
            }
        }

        public void BackTutorial()
        {
            if (_currentTutoIndex <= 0 == false)
            {
                _currentTutoIndex--;
                currentTutoImage.sprite = tutoImgs[_currentTutoIndex];
            }
        }
    }
}