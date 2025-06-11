using UnityEngine;
using UnityEngine.Serialization;

namespace _00._Work._02._Scripts.Button
{
    public class UIContainer : MonoBehaviour
    {
        public static UIContainer Instance { get; private set; }

        [SerializeField] public GameObject characterUI;
        [SerializeField] public GameObject dungeonUI;
        [SerializeField] public GameObject margeBoardUI;

        private void Awake()
        {
            Instance = this;
        }
        
    }
}
