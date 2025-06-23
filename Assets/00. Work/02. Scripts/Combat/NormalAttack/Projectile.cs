using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Combat.Effect;
using _00._Work._02._Scripts.Combat.Passive;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SoundManager;
using UnityEngine;


namespace _00._Work._02._Scripts.Combat.NormalAttack
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 3f;
        public float damage = 10f;
        public Rigidbody2D rbCompo;
        public GameObject hitEffectPrefab;
        
        private Vector3 _direction;
        
        public void Initialize(Vector3 direction, SkillDataSo skillData)
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
                SoundManager.Instance.PlaySfx("Hit");
                targetHealth.TakeDamage(DamageCalculate(damage, GameManager.Instance.selectedWeaponEchoData.damage, 
                    GameManager.Instance.selectedCharacterData.characterElementType));
                if (hitEffectPrefab != null)
                {
                    EffectPlayer effect = Instantiate(hitEffectPrefab).GetComponent<EffectPlayer>();
                    effect.SetPositionAndPlay(transform.position);
                }
            }
            
            Destroy(gameObject);
        }

        private float DamageCalculate(float dealDamage, float weaponDealDamage, string elementType)
        {
            float baseDamage = dealDamage;
            float weaponDamage = weaponDealDamage;
            float isWeakness = GameManager.Instance.selectedDungeonData.weaknessElement ==
                elementType ? 2f : 1f;
            float buff = PassiveController.Instance.multiplier;
            
            return (baseDamage + weaponDamage) * isWeakness * buff;
        }
    }
}