using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge;
using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.Buttons
{
    public class UIContainer : MonoBehaviour
    {
        public static UIContainer Instance { get; private set; }

        [SerializeField] public GameObject characterUI;
        [SerializeField] public GameObject characterImg;
        [SerializeField] public GameObject storyUI;
        [SerializeField] public GameObject dungeonUI;
        [SerializeField] public GameObject margeBoardUI;
        [SerializeField] public TextMeshProUGUI weaponName;
        [SerializeField] public TextMeshProUGUI weaponDesc;
        [SerializeField] public RectTransform effectParent;

        [SerializeField] public MargeBoard margeBoardScript;
        [SerializeField] public CharacterDisplayPanel characterDisplayPanel;

        private void Awake()
        {
            Instance = this;
        }
        
    }
}
