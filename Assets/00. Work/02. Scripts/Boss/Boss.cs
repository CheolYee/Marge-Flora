using System.Collections;
using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Boss.SO;
using _00._Work._02._Scripts.Manager.CombatManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Boss
{
    public class Boss : MonoBehaviour
    {
        public BossDataSo bossData;
        public AgentHealth BossHealth {get; private set;}
        
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float feedbackDuration = 0.1f;
        
        private Material _material;

        private readonly int _hit = Animator.StringToHash("Hit");
        private readonly int _die = Animator.StringToHash("IsDead");
        private readonly int _isBlinkHash = Shader.PropertyToID("_IsBlink");
        private void Awake()
        {
            BossHealth = gameObject.GetComponent<AgentHealth>();
            _material = spriteRenderer.material;
        }

        private void Start()
        {
            BossHealth.Initialize(bossData);
            CombatManager.Instance.bossHpSlider.maxValue = BossHealth.CurrentHealth;
            CombatManager.Instance.bossName.text = bossData.bossName;
            HealthUIUpdate();
        }

        public void HitFeedBack()
        {
            if (spriteRenderer != null)
            {
                animator.SetTrigger(_hit);
                _material.SetInt(_isBlinkHash, 1);
                StartCoroutine(DelayBlinkCoroutine());
            }
        }

        private IEnumerator DelayBlinkCoroutine()
        {
            yield return new WaitForSeconds(feedbackDuration);
            _material.SetInt(_isBlinkHash, 0);
        }

        public void IsDead()
        {
            if (CombatManager.Instance.bossIsDead) return;
            
            CombatManager.Instance.bossIsDead = true;
            
            animator.SetBool(_die, true);
            TimerManager.Instance.SetTimer(1f);
            StartCoroutine(DestroyBoss());
        }

        private IEnumerator DestroyBoss()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        public void HealthUIUpdate()
        {
            CombatManager.Instance.bossHpSlider.value = BossHealth.CurrentHealth;
            CombatManager.Instance.healthText.text = $"{BossHealth.CurrentHealth} / {BossHealth.MaxHealth}";
        }
    }
}
