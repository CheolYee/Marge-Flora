using System.Collections;
using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Manager.GameManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat.Skill.Skills
{
    public class Skill2 : AgentSkillEffect
    {
        [SerializeField] private int hitCount = 4;
        
        private int _currentHitCount;


        protected override IEnumerator DealDamageRoutine()
        {
            yield break;
        }

        public void DealDamageByAnimationEvent()
        {
            if (_currentHitCount >= hitCount) return;

            float calculatedDamage = DamageCalculate(Damage,
                GameManager.Instance.selectedCharacterData.skillData.skillDamage,
                GameManager.Instance.selectedCharacterData.characterElementType);

            if (Target != null && CombatInitializer.Instance.spawnBoss.TryGetComponent(out AgentHealth agentHealth))
            {
                agentHealth.TakeDamage(calculatedDamage);
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