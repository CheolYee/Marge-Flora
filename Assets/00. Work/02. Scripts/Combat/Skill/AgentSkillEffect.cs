using _00._Work._02._Scripts.Combat.Passive;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SoundManager;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat.Skill
{
    public abstract class AgentSkillEffect : MonoBehaviour
    {
        protected Transform Target;
        
        protected float Damage = 10f;

        protected string SfxNames;

        public void Initialize(Transform bossTrm, float damageAmount, string sfxName)
        {
            Target = bossTrm;
            Damage = damageAmount;
            
            var currentTrm = transform.position;
            currentTrm.x = Target.position.x;
            transform.position = currentTrm;
            SfxNames = sfxName;
        }

        public void SoundStart()
        {
            SoundManager.Instance.PlaySfx(SfxNames);
        }

        protected float DamageCalculate(float dealDamage, float weaponDealDamage, string elementType)
        {
            float baseDamage = dealDamage;
            float weaponDamage = weaponDealDamage * 0.1f * baseDamage;
            float isWeakness = GameManager.Instance.selectedDungeonData.weaknessElement ==
                               elementType ? 2f : 1f;
            float buff = PassiveController.Instance.multiplier;
            
            Logging.Log($"{baseDamage}, {weaponDamage}, {isWeakness}, {buff} = {(baseDamage + weaponDamage) * isWeakness * buff}");
            return (baseDamage + weaponDamage) * isWeakness * buff;
        }
    }
}
