using System;
using _00._Work._02._Scripts.Manager.CharacterManager;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge;
using UnityEngine;

namespace _00._Work._02._Scripts.Character
{
    public class CharacterSelectBtn : MonoBehaviour
    {
        [SerializeField] private CharacterDataSo characterData; //자신의 캐릭터 데이터를 넣을 공간
        [SerializeField] private GameObject characterLockpanel;

        private void Start()
        {
            if (!SaveManager.Instance.IsCharacterUnlocked(characterData.characterID)) //캐릭이 언락 아니라면
            {
                characterLockpanel.SetActive(true); //켜주기
            }
        }

        public void OnClick() //버튼을 클릭하였을 때
        {
            if (SaveManager.Instance.IsCharacterUnlocked(characterData.characterID)) // 만약 캐릭이 언락 상태라면
            {
                CharacterSelectManager.Instance.SelectCharacter(characterData); // 캐릭터 선택시 그 SO의 데이터들을 읽어와 UI에 뿌림
                GameManager.Instance.selectedCharacterData = characterData; // 저장한 데이터를 불러오기 위해 게임메니저에 보내기

                if (SaveManager.Instance.LoadMergeDataForCharacter(characterData.characterID) != null)
                {
                    GameManager.Instance.selectedWeaponEchoData = EchoCoreDatabase.Instance.GetEchoCoreSo(
                        SaveManager.Instance.LoadMergeDataForCharacter(characterData.characterID)?.equipmentCoreData.itemName);
                }
                else
                {
                    GameManager.Instance.selectedWeaponEchoData = null;
                }
                
                SaveManager.Instance.SaveLastUsedCharacterID(characterData.characterID); //캐릭터도 세이브
            }
        }
    }
}