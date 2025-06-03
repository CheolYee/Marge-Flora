using _00._Work._02._Scripts.Character;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.CharacterManager
{
    public class CharacterSelectManager : MonoSingleton<CharacterSelectManager>
    {
        [SerializeField] private CharacterDataSo currentCharacterData;

        public void SelectCharacter(CharacterDataSo characterData)
        {
            currentCharacterData = characterData;
        }
    }
}
