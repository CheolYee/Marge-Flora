using System;
using _00._Work._02._Scripts.Manager.CharacterManager;
using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterSelectBtn : MonoBehaviour
    {
        [SerializeField] private CharacterDataSo characterData;
        [SerializeField] private TextMeshProUGUI characterName;

        private void Start()
        {
            characterName.text = characterData.characterName;
        }

        public void OnClick()
        {
            CharacterSelectManager.Instance.SelectCharacter(characterData);
        }
    }
}