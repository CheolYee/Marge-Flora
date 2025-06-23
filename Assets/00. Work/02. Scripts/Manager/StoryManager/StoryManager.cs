using System.Collections;
using System.Collections.Generic;
using _00._Work._02._Scripts.Story;
using _00._Work._02._Scripts.Story.Data;
using _00._Work._02._Scripts.Story.SO;
using _00._Work._08._Utility;
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
        public Image backgroundImage;
        public TextMeshProUGUI dialogueText;
        public Button dialoguePanelButton;
        public float textSpeed = 0.05f; // 텍스트 나오는 속도
        private Coroutine _typingCoroutine; // new 안하고 쓰기 위한 코루틴 담기 변수
        private bool _isTyping; // 문자 작성 코루틴이 돌아가는 중인가?
        private int _currentIndex; //메인 스토리라인 인덱스
        private int _choiceIndex; // 선택지 인덱스
        private bool _isInChoiceReply; //지금 선택지 메시지 출력중인가?
        
        [Header("Character")]
        public Image leftCharacterImage;
        public List<Sprite> backgroundSprites;
        public Image rightCharacterImage;
        public Image centerCharacterImage;
        public CharacterSpriteDatabase characterSpriteDatabase;
        private Image _targetImage;
        private SpeakerPosition _position;
        
        [Header("Choice UI")]
        public GameObject choicePanel;
        public List<Button> choiceButtons;

        private string _playerName;
        private bool _isFading;

        protected override void Awake()
        {
            base.Awake();
            currentStory = GameManager.GameManager.Instance.selectedStoryData;
        }

        private void Start()
        {
            _currentIndex = 0;
            _choiceIndex = 0;
            _playerName = SaveManager.SaveManager.LoadPlayerNameData().playerName;
            dialogueText.text = "";
            ShowNextDialogue();
        }

        public void HandleSkipBtnClick()
        {
            _isTyping = false; //타이핑을 종료시키고
            StopCoroutine(_typingCoroutine); //타이핑을 종료시키고22
            
            SaveManager.SaveManager.SaveStoryID(GameManager.GameManager.Instance.SaveStoryData, 
                currentStory.storyId);
            
            FadeManager.FadeManager.Instance.FadeToScene(1); //돌아가요
        }

        public void HandleDialogueClick()
        {
            if (_isFading) return;
            
            if (_isTyping)
            {
                _isTyping = false;
                if (_typingCoroutine != null)
                    StopCoroutine(_typingCoroutine);

                StoryLineSo line = _isInChoiceReply ?
                    currentStory.choiceLines[_choiceIndex] :
                    currentStory.storyLines[_currentIndex];

                dialogueText.text = ReplacePlayerName(line.dialogue);
                return;
            }

            // 타이핑이 끝났고 선택지 상태일 때
            if (_isInChoiceReply)
            {
                _isInChoiceReply = false;
                _currentIndex++; // 선택지 대사 끝났으니 다음으로
                ShowNextDialogue();
                return;
            }

            // 메인 대사에 선택지가 있는 경우: 선택지 먼저 보여주고 대사 인덱스는 올리지 않음
            var line2 = currentStory.storyLines[_currentIndex];
            if (line2.hasChoices && line2.choices.Count > 0)
            {
                ShowChoices(line2.choices);
                return;
            }

            // 일반적인 경우
            _currentIndex++;
            ShowNextDialogue();
        }

        private void ShowNextDialogue()
        {
            StoryLineSo line;
            
            if (_isInChoiceReply)
            {
                if (_choiceIndex >= currentStory.choiceLines.Count)
                {
                    return;
                }

                line = currentStory.choiceLines[_choiceIndex];

                if (_isTyping)
                {
                    _isTyping = false;
                    StopCoroutine(_typingCoroutine);
                    dialogueText.text = line.dialogue;

                    // 선택지 대사 끝났으니 다음 메인 대사로 넘어가기 준비
                    _isInChoiceReply = false;
                    _currentIndex++;
                    return;
                }

                ShowBackground(line.backgroundImgType, line.fadeEffectType, line);
                return;
            }
            
            if (_currentIndex >= currentStory.storyLines.Count)
            {
                _isTyping = false; //타이핑을 종료시키고
                StopCoroutine(_typingCoroutine); //타이핑을 종료시키고22
                dialoguePanelButton.interactable = false;
            
                SaveManager.SaveManager.SaveStoryID(GameManager.GameManager.Instance.SaveStoryData, 
                    currentStory.storyId);
            
                FadeManager.FadeManager.Instance.FadeToScene(1); //돌아가요
                return;
            }
            
            line = currentStory.storyLines[_currentIndex]; //현재 스토리 리스트의 순서대로 so를 뽑아
            
            if (_isTyping) //만약 텍스트바를 눌렀는데 타이핑 중이라면
            {
                if (line.hasChoices && line.choices.Count > 0)
                {
                    ShowChoices(line.choices);
                    return;
                }
                
                _isTyping = false; //타이핑을 종료시키고
                StopCoroutine(_typingCoroutine); //타이핑을 종료시키고22
                dialogueText.text = line.dialogue; //대사를 모두 출력한 후
                _currentIndex++; // 다음 대사를 위해 다음 대사 인덱스로 넘어간다
                return; //다음 예외가 실행되면 안되니 종료시킨다.
            }
            
            ShowBackground(line.backgroundImgType, line.fadeEffectType, line);
        }

        private void ShowAllContents(StoryLineSo line)
        {
            speakerName.text = ReplacePlayerName(line.speakerName);
            ShowDialogue(ReplacePlayerName(line.dialogue));
            ShowCharacter(line.speakerName, line.speakerPosition, line.emotion);
            PlaySoundEffect(line.audioSfxType);
            PlayBgm(line.bgmType);
            PlayEnterEffect(line.enterEffectType);
        }

        private void PlayBgm(BgmType lineBGMType)
        {
            switch (lineBGMType)
            {
                case BgmType.None:
                    break;
                case BgmType.Stop:
                    SoundManager.SoundManager.Instance.StopBgm();
                    break;
                case BgmType.Main:
                    SoundManager.SoundManager.Instance.PlayBgm("Main");
                    break;
                case BgmType.Peaceful:
                    SoundManager.SoundManager.Instance.PlayBgm("Peaceful");
                    break;
                case BgmType.End:
                    SoundManager.SoundManager.Instance.PlayBgm("End");
                    break;
                case BgmType.Marge:
                    SoundManager.SoundManager.Instance.PlayBgm("Marge");
                    break;
                case BgmType.Stupid:
                    SoundManager.SoundManager.Instance.PlayBgm("Stupid");
                    break;
            }
        }

        string ReplacePlayerName(string input)
        {
            return input.Replace("{player}", _playerName);
        }
        

        private void ShowChoices(List<ChoiceData> choices)
        {
            choicePanel.SetActive(true);

            for (int i = 0; i < choices.Count; i++)
            {
                if (i < choices.Count)
                {
                    var choice = choices[i];
                    choiceButtons[i].gameObject.SetActive(true);
                    
                    var buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    buttonText.text = choice.choiceText;

                    int nextLine = choice.choiceLineIndex;
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() =>
                    {
                        choicePanel.SetActive(false);
                        OnSelectChoice(nextLine);  // nextLine은 choiceLines 인덱스
                    });
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnSelectChoice(int replyIndex)
        {
            _isInChoiceReply = true;
            SoundManager.SoundManager.Instance.PlaySfx("ButtonClick");
            _choiceIndex = replyIndex;
            ShowNextDialogue(); // 선택한 대사 출력 시작
        }
        
        //대사 보여주는 함수
        private void ShowDialogue(string dialogue)
        {
            if (_isTyping)
            {
                dialogueText.text = dialogue;
                _isTyping = false;
            }
            else
            {
                _typingCoroutine = StartCoroutine(TypeTextCoroutine(dialogue));
            }
        }

        //텍스트 타이핑 코루틴
        private IEnumerator TypeTextCoroutine(string dialogue)
        {
            _isTyping = true;
            dialogueText.text = "";

            int totalLength = dialogue.Length;

            for (int i = 0; i < totalLength; i++)
            {
                dialogueText.text += dialogue[i];
                yield return new WaitForSeconds(textSpeed);
            }

            _isTyping = false;
        }

        private void ShowBackground(BackgroundImgType backgroundImgType, FadeEffectType fadeEffectType, StoryLineSo line)
        {
            switch (fadeEffectType)
            {
                case FadeEffectType.None:
                    ShowAllContents(line);
                    ShowInBackground(backgroundImgType);
                    break;

                case FadeEffectType.FadeIn:
                    _isFading = true;
                    FadeManager.FadeManager.Instance.FadeIn(() =>
                    {
                        ShowAllContents(line);
                        ShowInBackground(backgroundImgType);
                        _isFading = false;
                    });
                    break;

                case FadeEffectType.FadeOut:
                    _isFading = true;
                    FadeManager.FadeManager.Instance.FadeOut(() =>
                    {
                        ShowInBackground(backgroundImgType);
                        ShowAllContents(line);
                        _isFading = false;
                    });
                    break;

                case FadeEffectType.FadeInOut:
                    _isFading = true;
                    FadeManager.FadeManager.Instance.FadeIn(() =>
                    {
                        ShowAllContents(line);
                        ShowInBackground(backgroundImgType);
                        FadeManager.FadeManager.Instance.FadeOut(() =>
                        {
                            _isFading = false;
                        });
                    });
                    break;
            }
        }

        private void ShowInBackground(BackgroundImgType backgroundImgType)
        {
            switch (backgroundImgType)
            {
                case BackgroundImgType.None:
                    break;
                case BackgroundImgType.Grass:
                    backgroundImage.sprite = backgroundSprites[0];
                    break;
                case BackgroundImgType.House:
                    backgroundImage.sprite = backgroundSprites[1];
                    break;
                case BackgroundImgType.Forest:
                    backgroundImage.sprite = backgroundSprites[2];
                    break;
            }
        }
        
        //캐릭터를 보여주는 함수
        private void ShowCharacter(string characterName, SpeakerPosition position, Emotion emotion)
        {
            if (characterName == SaveManager.SaveManager.LoadPlayerNameData().playerName) return;
            if (characterName == "")
            {
                HideAllCharacters();
                return;
            }
            if (position == SpeakerPosition.None || emotion == Emotion.None) return;
            
            Sprite characterSprite = characterSpriteDatabase.GetSprite(characterName, emotion);
            if (characterSprite == null)
            {
                return;
            }
            
            
            switch (position)
            {
                case SpeakerPosition.Right:
                    centerCharacterImage.gameObject.SetActive(false);
                    rightCharacterImage.sprite = characterSprite;
                    _targetImage = rightCharacterImage;
                    rightCharacterImage.gameObject.SetActive(true);
                    return;
                // Center는 단독이므로 좌/우 숨기고 Center만 보여줌
                case SpeakerPosition.Center:
                    HideAllCharacters();

                    centerCharacterImage.sprite = characterSprite;
                    _targetImage = centerCharacterImage;
                    centerCharacterImage.gameObject.SetActive(true);
                    return;
                case SpeakerPosition.Left:
                    centerCharacterImage.gameObject.SetActive(false);
                    leftCharacterImage.sprite = characterSprite;
                    _targetImage = leftCharacterImage;
                    leftCharacterImage.gameObject.SetActive(true);
                    break;
            }
        }

        private void PlaySoundEffect(AudioSfxType audioSfxType)
        {
            switch (audioSfxType)
            {
                case AudioSfxType.Default:
                    break;
                case AudioSfxType.Angry:
                    SoundManager.SoundManager.Instance.PlaySfx("AngryEmotion");
                    break;
                case AudioSfxType.Happy:
                    SoundManager.SoundManager.Instance.PlaySfx("HappyEmotion");
                    break;
                case AudioSfxType.Sad:
                    SoundManager.SoundManager.Instance.PlaySfx("SadEmotion");
                    break;
                case AudioSfxType.Curious:
                    SoundManager.SoundManager.Instance.PlaySfx("CuriousEmotion");
                    break;
                case AudioSfxType.Surprised:
                    SoundManager.SoundManager.Instance.PlaySfx("SurprisedEmotion");
                    break;
                case AudioSfxType.Run:
                    SoundManager.SoundManager.Instance.PlaySfx("Run");
                    break;
                case AudioSfxType.Blush:
                    SoundManager.SoundManager.Instance.PlaySfx("Blush");
                    break;
                case AudioSfxType.Boom:
                    SoundManager.SoundManager.Instance.PlaySfx("Boom");
                    break;
            }
        }

        private void PlayEnterEffect(EnterEffectType effectType)
        {
            if (_targetImage == null || !_targetImage.gameObject.activeSelf) return;

            RectTransform rect = _targetImage.GetComponent<RectTransform>();
            Vector2 originalPos = rect.anchoredPosition;
            float slideDistance = 800f; // 슬라이드 시작 위치 (화면 밖)
            
            switch (effectType)
            {
                case EnterEffectType.FadeIn:
                    _targetImage.color = new Color(1, 1, 1, 0);
                    _targetImage.DOFade(1f, 0.5f);
                    break;
                case EnterEffectType.SlideFromLeft:
                    rect.anchoredPosition = new Vector2(-slideDistance, originalPos.y); // 왼쪽 밖에서 시작
                    rect.DOAnchorPosX(originalPos.x, 0.5f).SetEase(Ease.OutCubic);
                    break;
                case EnterEffectType.SlideFromRight:
                    rect.anchoredPosition = new Vector2(slideDistance, originalPos.y); // 오른쪽 밖에서 시작
                    rect.DOAnchorPosX(originalPos.x, 0.5f).SetEase(Ease.OutCubic);
                    break;
                case EnterEffectType.Bounce:
                    rect.anchoredPosition = new Vector2(originalPos.x, originalPos.y - 200f);
                    DOTween.Sequence()
                        .Append(rect.DOAnchorPosY(originalPos.y + 30f, 0.3f).SetEase(Ease.OutQuad))
                        .Append(rect.DOAnchorPosY(originalPos.y, 0.2f).SetEase(Ease.InQuad))
                        .Append(rect.DOAnchorPosY(originalPos.y + 10f, 0.15f).SetEase(Ease.OutQuad))
                        .Append(rect.DOAnchorPosY(originalPos.y, 0.15f).SetEase(Ease.InQuad));
                    break;
                case EnterEffectType.None:
                    break;
            }
        }

        //모든 캐릭터를 숨기는 함수
        private void HideAllCharacters()
        {
            leftCharacterImage.gameObject.SetActive(false);
            rightCharacterImage.gameObject.SetActive(false);
            centerCharacterImage.gameObject.SetActive(false);
        }
    }
}
