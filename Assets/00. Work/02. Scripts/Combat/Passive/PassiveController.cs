using System.Collections;
using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Manager;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat.Passive
{
    public class PassiveController : MonoSingleton<PassiveController>
    {
        [SerializeField] private PassiveDataSo passiveData;
        [SerializeField] private GameObject buffEffect;
        
        public float multiplier = 1f;
        public float attackSpeed = 0;
        
        private bool _isFinished;

        private void Start()
        {
            TimerManager.Instance.OnTimerFinished += () => _isFinished = true;
            
            passiveData = GameManager.Instance.selectedCharacterData.passiveData;
            buffEffect = GameManager.Instance.selectedCharacterData.passiveData.buffEffectPrefab;
            StartCoroutine(PassiveRoutine());
        }

        private IEnumerator PassiveRoutine()
        {
            if (passiveData.buffPassive)
            {
                StartCoroutine(ApplyAttackBuff());
                
                while (!_isFinished)
                {
                    yield return new WaitForSeconds(passiveData.cooldown);
                    
                    if (_isFinished) break;
                    StartCoroutine(ApplyAttackBuff());
                }
            }
            
            if (passiveData.attackSpeedBuffPassive)
            {
                StartCoroutine(ApplyAttackSpeedBuff());
                
                while (!_isFinished)
                {
                    yield return new WaitForSeconds(passiveData.attackSpeedBuffCooldown);
                    
                    if (_isFinished) break;
                    StartCoroutine(ApplyAttackSpeedBuff());
                }
            }
        }

        private IEnumerator ApplyAttackBuff()
        {
            Logging.Log("공버프 실행");
                
            GameObject currentBuffEffect = Instantiate(buffEffect, buffEffect.transform.position, buffEffect.transform.rotation);
            
            multiplier = passiveData.attackMultiplier;
            
            yield return new WaitForSeconds(passiveData.buffDuration);

            multiplier = 1;
            Destroy(currentBuffEffect);
            Logging.Log("공버프 종료");
        }
        
        private IEnumerator ApplyAttackSpeedBuff()
        {
            Logging.Log("공감버프 실행");
                
            GameObject currentBuffEffect = Instantiate(buffEffect, buffEffect.transform.position, buffEffect.transform.rotation);
            
            attackSpeed += passiveData.attackSpeed;
            Logging.LogWarning($"{attackSpeed}");
            Destroy(currentBuffEffect, 1);
            
            yield return new WaitForSeconds(passiveData.attackSpeedBuffCooldown);
        }
    }
}
