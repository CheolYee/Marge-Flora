using System.Collections;
using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Combat.NormalAttack;
using _00._Work._02._Scripts.Combat.Passive;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.TimerManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        [Header("Player Visual Controller")]
        [SerializeField] private SpriteRenderer playerVisual;
        [SerializeField] private Animator playerVisualAnimator;
        
        [Header("Player Combat")]
        public Transform normalAttackTrm;
        public SkillDataSo skillData;

        private Coroutine _normalAttackRoutine;
        private WaitForSeconds _normalAttackCooldown;

        private void OnEnable()
        {
            skillData = GameManager.Instance.selectedCharacterData.skillData;
            playerVisual.sprite = GameManager.Instance.selectedCharacterData.animationSprite;
            playerVisualAnimator.runtimeAnimatorController = GameManager.Instance.selectedCharacterData.animator;
            float normalAttackCool = skillData.normalAttackCooldown;
            _normalAttackCooldown = new WaitForSeconds(normalAttackCool);
            StartAutoAttack();
            TimerManager.Instance.OnTimerFinished += StopAutoAttack;
        }

        private void OnDisable()
        {
            TimerManager.Instance.OnTimerFinished -= StopAutoAttack;
            StopAutoAttack();
        }

        private void StopAutoAttack()
        {
            if (_normalAttackRoutine != null)
            {
                StopCoroutine(_normalAttackRoutine);
                _normalAttackRoutine = null;
            }
        }

        private void StartAutoAttack()
        {
            if (_normalAttackRoutine == null)
                _normalAttackRoutine = StartCoroutine(AutoAttackRoutine());
        }

        private IEnumerator AutoAttackRoutine()
        {
            while (true)
            {
                FireNormalAttack();
                float normalAttackCool = skillData.normalAttackCooldown - PassiveController.Instance.attackSpeed;
                _normalAttackCooldown = new WaitForSeconds(normalAttackCool);
                yield return _normalAttackCooldown;
            }
        }

        private void FireNormalAttack()
        {
            GameObject projectileObj = Instantiate(skillData.normalAttackPrefab, normalAttackTrm.position, Quaternion.identity);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Initialize(Vector3.right, GameManager.Instance.selectedCharacterData.skillData);
            }
        }
    }
}
