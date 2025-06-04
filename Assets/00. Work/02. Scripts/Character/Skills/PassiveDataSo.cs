using UnityEngine;

namespace _00._Work._02._Scripts.Character.Skills
{
    [CreateAssetMenu(fileName = "PassiveSkillData", menuName = "SO/Char/Passive")]
    public class PassiveDataSo : ScriptableObject
    {
        public string passiveSkillName;
        [TextArea] public string passiveDescription;
        public Sprite passiveIcon;
    }
}
