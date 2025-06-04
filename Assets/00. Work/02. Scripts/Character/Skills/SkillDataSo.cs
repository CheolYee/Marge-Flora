using UnityEngine;

namespace _00._Work._02._Scripts.Character.Skills
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "SO/Char/Skill")]
    public class SkillDataSo : ScriptableObject
    {
        public string skillName;
        [TextArea] public string skillDescription;
        public Sprite skillIcon;
        
    }
}
