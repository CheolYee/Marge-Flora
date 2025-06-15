using System.Collections.Generic;
using _00._Work._02._Scripts.Buttons;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterDatabase : MonoBehaviour
    {
        public List<CharacterDataSo> allCharacters;
        
        private void Start()
        {
            SaveManager.Instance.LoadUnlockData();
            
            string lastID = SaveManager.Instance.LoadLastUsedCharacterID();
            
            if (!string.IsNullOrEmpty(lastID))
            {
                CharacterDataSo found = GetCharacterByID(lastID);
                if (found != null)
                {
                    GameManager.Instance.selectedCharacterData = found;
                    UIContainer.Instance.margeBoardScript.LoadWeaponData();
                    UIContainer.Instance.characterDisplayPanel.Display(found); // 캐릭터 디스플레이 창에 띄우기
                }
            }
        }

        private CharacterDataSo GetCharacterByID(string id)
        {
            return allCharacters.Find(c => c.characterID == id);
        }
    }
}
