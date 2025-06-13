using System.Collections;
using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Combat.NormalAttack;
using _00._Work._02._Scripts.Manager.GameManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        public Transform normalAttackTrm;
        public SkillDataSo skillData;

        private Coroutine _normalAttackRoutine;
        private WaitForSeconds _normalAttackCooldown;

        private void OnEnable()
        {
            skillData = GameManager.Instance.selectedCharacterData.skillData;
            _normalAttackCooldown = new WaitForSeconds(skillData.normalAttackCooldown);
            StartAutoAttack();
        }

        private void OnDisable()
        {
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
                yield return _normalAttackCooldown;
            }
        }

        private void FireNormalAttack()
        {
            GameObject projectileObj = Instantiate(skillData.normalAttackPrefab, normalAttackTrm.position, Quaternion.identity);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Initialize(Vector3.right);
            }
        }
    }
}
