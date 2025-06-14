using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge;
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

        [SerializeField] public MargeBoard margeBoardScript;
        [SerializeField] public CharacterDisplayPanel characterDisplayPanel;

        private void Awake()
        {
            Instance = this;
        }
        
    }
}
