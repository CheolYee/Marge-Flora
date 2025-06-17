
using System.Collections.Generic;
using _00._Work._02._Scripts.Story.Data;
using UnityEngine;

namespace _00._Work._02._Scripts.Story.SO
{
    [CreateAssetMenu(fileName = "StoryLine", menuName = "SO/Story/StoryLine")]
    public class StoryLineSo : ScriptableObject
    {
        public string speakerName;
        public Emotion emotion; // 감정
        public SpeakerPosition speakerPosition; // 포지션
        public AudioSfxType audioSfxType; // 등장 효과음
        public EnterEffectType enterEffectType; //등장 이펙트
        
        [TextArea(2, 5)] public string dialogue; //대사
        
        public bool hasChoices; // 선택지를 가지고 있는 대사인가?
        public List<ChoiceData> choices = new List<ChoiceData>(); // 선택지 목록

    }
}