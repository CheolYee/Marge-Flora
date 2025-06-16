using System.Collections.Generic;
using UnityEngine;

namespace _00._Work._02._Scripts.Story.SO
{
    [CreateAssetMenu(fileName = "CharacterSpriteDatabase", menuName = "SO/Story/CharacterSpriteDatabase", order = 0)]
    public class CharacterSpriteDatabase : ScriptableObject //캐릭터별 표정을 관리하기 위해 만든 데이터베이스
    {
        [System.Serializable]
        public class CharacterEmotionSet 
        {
            public string characterName;
            public Sprite neutral;
            public Sprite happy;
            public Sprite sad;
            public Sprite angry;
        }
        
        public List<CharacterEmotionSet> characterSprites;

        public Sprite GetSprite(string characterName, Emotion emotion)
        {
            var set = characterSprites.Find(s => s.characterName == characterName);
            if (set == null) return null;

            return emotion switch
            {
                Emotion.Default => set.neutral,
                Emotion.Happy => set.happy,
                Emotion.Sad => set.sad,
                Emotion.Angry => set.angry,
                _ => null
            };
        }
    }
}