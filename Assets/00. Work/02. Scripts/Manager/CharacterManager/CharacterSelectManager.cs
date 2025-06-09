using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Character;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.CharacterManager
{
    public class CharacterSelectManager : MonoBehaviour
    {
        public static CharacterSelectManager Instance { get; private set; }
        
        [SerializeField] private CharacterDisplayPanel characterDisplayPanel; //캐릭터 패널 

        private void Start()
        {
            Instance = this;
        }

        public void SelectCharacter(CharacterDataSo characterData) //캐릭터 선택시 호출
        {
            characterDisplayPanel.Display(characterData);
        }
    }
}
