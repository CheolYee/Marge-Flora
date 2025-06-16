using UnityEngine;

namespace _00._Work._02._Scripts.Marge.SO
{
    [CreateAssetMenu(fileName = "EchoCoreSO", menuName = "SO/EchoCore/EchoCoreItem")]
    public class EchoCoreSo : ScriptableObject
    {
        [Header("EchoCore Sprite")]
        public Sprite echoSprite;
        
        [Header("EchoCore Values")]
        public string coreName;
        public int growthCount;
        public int damage;
        
        [Header("NextEcho")]
        public EchoCoreSo nextEchoData;
    }
}
