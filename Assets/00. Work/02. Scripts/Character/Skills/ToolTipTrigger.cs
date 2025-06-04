using _00._Work._02._Scripts.Manager.ToolTipManager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _00._Work._02._Scripts.Character.Skills
{
    public enum SkillType
    {
        Skill,
        PassiveSkill,
        None
    }
    public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SkillType _skillType;
        private SkillDataSo _skillData;
        private PassiveDataSo _passiveSkillData;

        private void Start()
        {
            _skillType = SkillType.None;
        }

        public void SetSkill(SkillDataSo skillData)
        {
            _skillType = SkillType.Skill;
            _skillData = skillData;
        }
        
        public void SetPassive(PassiveDataSo passiveData)
        {
            _skillType = SkillType.PassiveSkill;
            _passiveSkillData = passiveData;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_skillType == SkillType.None) return;
            
            switch (_skillType)
            {
                case SkillType.Skill:
                    ToolTipManager.Instance.ShowToolTip(_skillData);
                    Debug.Log(_skillData);
                    break;
                case SkillType.PassiveSkill:
                    ToolTipManager.Instance.ShowToolTip(_passiveSkillData);
                    break;
                    
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipManager.Instance.HideToolTip();
        }
    }
}
