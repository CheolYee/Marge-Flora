using System.Globalization;
using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Combat.Effect;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._08._Utility;
using UnityEngine;


namespace _00._Work._02._Scripts.Combat.NormalAttack
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 3f;
        public float damage = 10f;
        public Rigidbody2D rbCompo;
        public GameObject hitEffectPrefab;
        
        public SkillDataSo skillData;
        
        private Vector3 _direction;
        
        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            damage = skillData.normalAttackDamage;
            speed = skillData.projectileSpeed;
        }

        void FixedUpdate()
        {
            rbCompo.linearVelocity = speed * _direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.TryGetComponent(out AgentHealth targetHealth))
            {
                targetHealth.TakeDamage(DamageCalculate());
                if (hitEffectPrefab != null)
                {
                    EffectPlayer effect = Instantiate(hitEffectPrefab).GetComponent<EffectPlayer>();
                    effect.SetPositionAndPlay(transform.position);
                }
            }
            
            Destroy(gameObject);
        }

        private float DamageCalculate()
        {
            float baseDamage = damage;
            float weaponDamage = GameManager.Instance.selectedWeaponEchoData.growthCount * 0.3f * baseDamage;
            float isWeakness = GameManager.Instance.selectedDungeonData.weaknessElement ==
                GameManager.Instance.selectedCharacterData.characterElementType ? 2f : 1f;
            
            Logging.Log($"{baseDamage}, {weaponDamage}, {isWeakness} = {(baseDamage + weaponDamage) * isWeakness}");
            return (baseDamage + weaponDamage) * isWeakness;
        }
    }
}