using System.Collections.Generic;
using _00._Work._02._Scripts.Manager;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "SO/Char/Data")]
    public class CharacterDataSo : ScriptableObject
    {
        public string characterName;
        public Sprite characterSprite;
        public string characterID;
    }
}
