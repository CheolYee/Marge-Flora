using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Manager.CombatManager
{
    public class CombatManager : MonoSingleton<CombatManager>
    {
        [Header("UI")] 
        public Slider bossHpSlider;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI bossName;
        public float combatTimer;

        [Header("Enemy")] 
        public GameObject boss;
    }
}
