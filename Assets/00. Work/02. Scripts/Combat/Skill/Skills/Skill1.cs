
using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SoundManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat.Skill.Skills
{
    public class Skill1 : AgentSkillEffect
    {
        [SerializeField] private int hitCount = 3;
        
        private int _currentHitCount;

        public void DealDamageByAnimationEvent()
        {
            if (_currentHitCount >= hitCount) return;

            SoundManager.Instance.PlaySfx("Hit");
            float calculatedDamage = DamageCalculate(Damage,
                GameManager.Instance.selectedCharacterData.skillData.skillDamage,
                GameManager.Instance.selectedCharacterData.characterElementType);

            if (Target != null && //타겟이 널이 아니고
                CombatInitializer.Instance.spawnBoss != null && //스폰 보스도 널이 아니고
                CombatInitializer.Instance.spawnBoss.TryGetComponent(out AgentHealth agentHealth)) //agentHealth가 있으면
            {
                agentHealth.TakeDamage(calculatedDamage); //데미지 주기
            }

            _currentHitCount++;

            // 모든 타격 끝나면 이펙트 제거
            if (_currentHitCount >= hitCount)
            {
                Destroy(gameObject, 0.5f); // 약간의 여유 시간 후 제거
            }
        }
    }
}
