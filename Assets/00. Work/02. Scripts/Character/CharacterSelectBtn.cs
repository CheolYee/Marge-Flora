using _00._Work._02._Scripts.Manager.CharacterManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterSelectBtn : MonoBehaviour
    {
        [SerializeField] private CharacterDataSo characterData;

        public void OnClick()
        {
            if (characterData.isUnlocked)
                CharacterSelectManager.Instance.SelectCharacter(characterData);
            else
                Debug.LogWarning("캐릭터가 언락되지 않았습니다.");
        }
    }
}