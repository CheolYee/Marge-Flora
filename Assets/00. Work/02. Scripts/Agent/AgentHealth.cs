using _00._Work._02._Scripts.Boss.SO;
using _00._Work._08._Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Agent
{
    public class AgentHealth : MonoBehaviour
    {
        public UnityEvent onHitEvent;
        public UnityEvent onDeadEvent;
        
        public float MaxHealth {get; private set;}
        
        public float CurrentHealth {get; private set;}

        public void Initialize(BossDataSo bossData)
        {
            MaxHealth = bossData.bossHp;
            ResetHealth();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            onHitEvent?.Invoke();
            if (CurrentHealth <= 0)
            {
                onDeadEvent?.Invoke();
            }
        }
        
    }
}
