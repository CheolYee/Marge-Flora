using _00._Work._02._Scripts.Agent;
using _00._Work._02._Scripts.Boss.SO;
using _00._Work._02._Scripts.Manager.CombatManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Boss
{
    public class Boss : MonoBehaviour
    {
        public BossDataSo bossData;

        public AgentHealth BossHealth {get; private set;}

        private void Awake()
        {
            BossHealth = gameObject.GetComponent<AgentHealth>();
        }

        private void Start()
        {
            BossHealth.Initialize(bossData);
            CombatManager.Instance.bossHpSlider.maxValue = BossHealth.CurrentHealth;
            CombatManager.Instance.bossName.text = bossData.bossName;
            HealthUIUpdate();
        }

        public void HealthUIUpdate()
        {
            CombatManager.Instance.bossHpSlider.value = BossHealth.CurrentHealth;
            CombatManager.Instance.healthText.text = $"{BossHealth.MaxHealth} / {BossHealth.CurrentHealth}";
        }
    }
}
