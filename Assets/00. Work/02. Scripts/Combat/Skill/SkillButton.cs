using _00._Work._02._Scripts.Character.Skills;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._08._Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Combat.Skill
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Button skillBtn;
        [SerializeField] private Image cooldownImage;
        [SerializeField] private TextMeshProUGUI cooldownText;
        
        private float _cooldown;
        private float _skillCooldown;
        private bool _isSkillCooldown;
        
        private SkillDataSo _skillData;
        private Transform _bossTrm;

        private void Start()
        {
            Initialize(GameManager.Instance.selectedCharacterData.skillData, 
                GameManager.Instance.selectedDungeonData.enemySpawnPos);
        }

        public void Initialize(SkillDataSo skillData, Transform bossTrm)
        {
            _skillData = skillData;
            _bossTrm = bossTrm;
            _skillCooldown = _skillData.skillCooldown;
            cooldownImage.fillAmount = 0;
            cooldownText.text = "";
            
            skillBtn.onClick.AddListener(UseSkill);
        }

        private void Update()
        {
            if (!_isSkillCooldown) return;
            
            _skillCooldown -= Time.deltaTime;
            cooldownImage.fillAmount = _skillCooldown / _skillData.skillCooldown;
            cooldownText.text = _skillCooldown.ToString("0.0");

            if (_skillCooldown <= 0f)
            {
                _isSkillCooldown = false;
                _skillCooldown = 0;
                cooldownImage.fillAmount = 0;
                cooldownText.text = "";
            }
        }

        private void UseSkill()
        {
            if (_isSkillCooldown) return;
            
            GameObject skill = Instantiate(_skillData.skillPrefab, _bossTrm.position, Quaternion.identity);
            if (skill.TryGetComponent(out AgentSkillEffect skillComponent))
            {
                skillComponent.Initialize(_bossTrm, _skillData.skillDamage, _skillData.skillSfxName);
            }
            else Logging.LogWarning("스킬 프리팹에 스킬 이펙트 상속 스크립트가 없음");
            
            
            _skillCooldown = _skillData.skillCooldown;
            _isSkillCooldown = true;
            cooldownImage.fillAmount = 1;
            cooldownText.text = _skillCooldown.ToString("0.0");
        }
    }
}
