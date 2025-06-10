using System.Collections.Generic;
using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge
{
    public class EchoCoreDatabase : MonoBehaviour
    {
        public static EchoCoreDatabase Instance;

        public List<EchoCoreSo> allEchoCoreSos;

        private void Awake()
        {
            Instance = this;
        }

        public EchoCoreSo GetEchoCoreSo(string coreName)
        {
            return allEchoCoreSos.Find(core => core.coreName == coreName);
        }
    }
}
