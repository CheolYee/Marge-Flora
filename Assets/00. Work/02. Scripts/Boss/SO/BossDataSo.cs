using UnityEngine;

namespace _00._Work._02._Scripts.Boss.SO
{
    [CreateAssetMenu(fileName = "BossData", menuName = "SO/BossDataSo")]
    public class BossDataSo : ScriptableObject
    {
        public string bossName;
        public int bossHp;
        public Sprite bossSprite;
    }
}
