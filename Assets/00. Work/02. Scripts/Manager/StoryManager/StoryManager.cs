using System.Collections;
using _00._Work._02._Scripts.Story;
using _00._Work._02._Scripts.Story.SO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Manager.StoryManager
{
    public class StoryManager : MonoSingleton<StoryManager>
    {
        public TextMeshProUGUI speakerName;
        public GameObject skipButton;
        
        public StoryContainerSo currentStory;
        
        [Header("Dialogue")]
        public TextMeshProUGUI dialogueText;
        public Button dialoguePanelButton;
        public float textSpeed = 0.05f;
        private Coroutine _typingCoroutine;
        private bool _isTyping = false;
        private string _currentDialogueText;
        private int _currentIndex;
        
        [Header("Character")]
        public Image leftCharacterImage;
        public Image rightCharacterImage;
        public Image centerCharacterImage;
        public CharacterSpriteDatabase characterSpriteDatabase;

        /*private void Start()
        {
            dialoguePanelButton.onClick.AddListener(OnDialoguePanelClick);
        }*/

        /*private void OnDialoguePanelClick()
        {
            if (_isTyping)
            {
                // 타이핑 중이면 즉시 전체 출력
                StopCoroutine(typingCoroutine);
                DialogueLine line = currentStory.storyLines[currentIndex];
                dialogueText.text = line.dialogue;
                _isTyping = false;
            }
            else
            {
                _currentIndex++;
                if (_currentIndex >= dialogueListSO.dialogues.Count)
                {
                    Debug.Log("대화 끝!");
                    dialoguePanelButton.interactable = false;
                    return;
                }

                ShowCurrentDialogue();
            }
        }*/
        /*private void ShowCurrentDialogue()
        {
            StoryLineSo line = dialogueListSO.dialogues[currentIndex];

            // 텍스트 출력
            nameText.text = line.characterName;
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeText(line.dialogue));

            // 캐릭터 출력
            characterDisplay.ShowCharacter(line.characterName, line.position, line.emotion);
        }*/
        

        //대사 보여주는 함수
        public void ShowDialogue(string dialogue)
        {
            if (_isTyping)
            {
                DOTween.Kill(dialogueText);
                dialogueText.text = _currentDialogueText;
                _isTyping = false;
            }
            else
            {
                _currentDialogueText = dialogue;
                _typingCoroutine = StartCoroutine(TypeTextCoroutine(dialogue));
            }
        }

        //텍스트 타이핑 코루틴
        private IEnumerator TypeTextCoroutine(string dialogue)
        {
            _isTyping = true;
            _currentDialogueText = "";
            
            int totalLength = dialogue.Length;

            for (int i = 0; i < totalLength; i++)
            {
                _currentDialogueText += dialogue[i];
                yield return new WaitForSeconds(textSpeed);
            }
            
            _isTyping = false;
        }
        
        //캐릭터를 보여주는 함수
        public void ShowCharacter(string characterName, SpeakerPosition position, Emotion emotion)
        {
            Sprite characterSprite = characterSpriteDatabase.GetSprite(characterName, emotion);
            if (characterSprite == null)
            {
                Debug.LogWarning($"캐릭터 스프라이트가 없음: {characterName}, {emotion}");
                return;
            }

            // Center는 단독이므로 좌/우 숨기고 Center만 보여줌
            if (position == SpeakerPosition.Center)
            {
                leftCharacterImage.gameObject.SetActive(false);
                rightCharacterImage.gameObject.SetActive(false);

                centerCharacterImage.sprite = characterSprite;
                centerCharacterImage.gameObject.SetActive(true);
            }
            else
            {
                centerCharacterImage.gameObject.SetActive(false);

                if (position == SpeakerPosition.Left)
                {
                    leftCharacterImage.sprite = characterSprite;
                    leftCharacterImage.gameObject.SetActive(true);
                }
                else if (position == SpeakerPosition.Right)
                {
                    rightCharacterImage.sprite = characterSprite;
                    rightCharacterImage.gameObject.SetActive(true);
                }
            }
        } 

        //모든 캐릭터를 숨기는 함수
        public void HideAllCharacters()
        {
            leftCharacterImage.gameObject.SetActive(false);
            rightCharacterImage.gameObject.SetActive(false);
            centerCharacterImage.gameObject.SetActive(false);
        }
    }
}
