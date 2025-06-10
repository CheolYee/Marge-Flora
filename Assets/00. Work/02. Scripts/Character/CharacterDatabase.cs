using System.Collections.Generic;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterDatabase : MonoBehaviour
    {
        public List<CharacterDataSo> allCharacters;
        
        [SerializeField] private CharacterDisplayPanel characterDisplayPanel;

        private void Start()
        {
            SaveManager.Instance.LoadUnlockData();
            
            string lastID = SaveManager.Instance.LoadLastUsedCharacterID();
            
            if (!string.IsNullOrEmpty(lastID))
            {
                CharacterDataSo found = GetCharacterByID(lastID); // 따로 함수 만들어주세요
                if (found != null)
                {
                    GameManager.Instance.selectedCharacterData = found;
                    characterDisplayPanel.Display(found); // 캐릭터 디스플레이 창에 띄우기
                }
            }
        }

        private CharacterDataSo GetCharacterByID(string id)
        {
            return allCharacters.Find(c => c.characterID == id);
        }
    }
}
