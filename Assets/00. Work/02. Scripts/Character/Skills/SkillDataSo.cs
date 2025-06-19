using UnityEngine;

namespace _00._Work._02._Scripts.Character.Skills
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "SO/Char/Skill")]
    public class SkillDataSo : ScriptableObject
    {
        public string skillName;
        [TextArea] public string skillDescription;
        public Sprite skillIcon;
        [Header("NormalAttack")]
        public GameObject normalAttackPrefab; // 평타 프리팹

        public float normalAttackCooldown; // 기본 쿨타임
        public float normalAttackDamage; // 기본 데미지
        public float projectileSpeed; // 발사체 속도
        
        [Header("Skill")]
        public GameObject skillPrefab; // 스킬 프리팹
        
        public float skillCooldown; //스킬 쿨타임
        public float skillDamage; //스킬 데미지
        public string skillSfxName;
        
    }
}
