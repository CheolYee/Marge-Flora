using _00._Work._02._Scripts.Character.Skills;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterDisplayPanel : MonoBehaviour
    {
        [SerializeField] private Image characterImage; // 캐릭터 메인 이미지 변경
        [SerializeField] private TextMeshProUGUI nameText; // 캐릭터 이름 텍스트 변경
        [SerializeField] private TextMeshProUGUI descriptionText; //캐릭터 설명 텍스트 변경
        
        [Header("Skill Description")]
        [SerializeField] private Image elementImage; // 속성 이미지
        [SerializeField] private Image skillIconImage; // 스킬 아이콘 이미지
        [SerializeField] private Image passiveSkillIconImage; // 패시브 스킬 아이콘 이미지
        
        [Header("ToolTip")]
        [SerializeField] private ToolTipTrigger skillToolTip; // 툴팁 표시할 오브젝트
        [SerializeField] private ToolTipTrigger passiveToolTip; // 툴팁 표시할 오브젝트
    
        public void Display(CharacterDataSo charData) //캐릭터 표시
        {
            characterImage.sprite = charData.characterSprite; //캐릭터 이미지 변경
            nameText.text = charData.characterName; // 캐릭터 이름 Text 변경
            descriptionText.text = charData.characterDescription; // 캐릭터 설명 Text 변경
            elementImage.sprite = charData.element; // 캐릭터 속성 이미지 변경

            PopulateSkills(charData.skillData, charData.passiveData);
        }
        
        private void PopulateSkills(SkillDataSo skills, PassiveDataSo passiveData) //캐릭터 스킬 아이콘과 툴팁 내용 변경
        {
            skillIconImage.sprite = skills.skillIcon; // 캐릭터 스킬 아이콘 변경
            passiveSkillIconImage.sprite = passiveData.passiveIcon; // 캐릭터 스킬 아이콘 변경
            skillToolTip.SetSkill(skills); //툴팁에 스킬 데이터 보내서 세팅
            passiveToolTip.SetPassive(passiveData); //툴팁에 스킬 데이터 보내서 세팅
        }
    }
}
