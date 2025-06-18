using UnityEngine;

namespace _00._Work._02._Scripts.Character.Skills
{
    [CreateAssetMenu(fileName = "PassiveSkillData", menuName = "SO/Char/Passive")]
    public class PassiveDataSo : ScriptableObject
    {
        public string passiveSkillName;
        [TextArea] public string passiveDescription;
        public Sprite passiveIcon;
        
        [Header("AttackBuffData")]
        public bool buffPassive; // 이 패시브가 버프 스킬인가?
        public float cooldown; // 버프 쿨타임 주기
        public float buffDuration; //버프 적용 시간
        public float attackMultiplier; //공격력 몇배율

        [Header("AttackSpeedBuffData")] 
        public bool attackSpeedBuffPassive;
        public float attackSpeedBuffCooldown; // 버프 쿨타임 주기
        public float attackSpeed; //공격속도 몇감소
        
        public GameObject buffEffectPrefab; //패시브 이펙트 프리팹
    }
}
