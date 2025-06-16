
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
        public CharacterSpriteDatabase characterSpriteDatabase; //캐릭 스프라이트 데이터들
        
        [TextArea(2, 5)] public string dialogue; //대사
        
        public EnterEffectType enterEffectType; //등장 이펙트
    }
}