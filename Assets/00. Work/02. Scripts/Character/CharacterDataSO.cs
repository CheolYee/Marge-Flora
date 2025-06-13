using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "SO/Char/Data")]
    public class CharacterDataSo : ScriptableObject
    {
        //캐릭터 데이터
        [Header("Character Data")]
        //캐릭터 이미지
        public Sprite characterSprite;
        //캐릭터 ID(몇 번째 캐릭인지 확인)
        public string characterID;
        //캐릭터 이름
        public string characterName;
        //캐릭터 속성 타입
        public string characterElementType;
        //캐릭터 속성 이미지
        public Sprite element;
        //캐릭터 선택 프로필에 들어갈 이미지
        public Sprite characterProfile;
        
        //캐릭터 설명
        [Header("Character Description")]
        [TextArea] public string characterDescription;
        
        //캐릭터 스킬
        [Header("Character Skill")]
        public SkillDataSo skillData;
        public PassiveDataSo passiveData;
        
        [Header("Character First Echo")]
        public EchoCoreSo firstEchoData;
    }
}
