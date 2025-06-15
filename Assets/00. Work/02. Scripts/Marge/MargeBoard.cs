using System.Collections.Generic;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge.DragDrop;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Save;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge
{
    public class MargeBoard : MonoBehaviour
    {
        public GameObject parent;
        public GameObject equipEchoCore;
        public List<Transform> slotList;
        public GameObject echoPrefab;


        private void Awake()
        {
            slotList = new List<Transform>();
            foreach (Transform child in parent.transform)
            {
                slotList.Add(child);
            }
            
        }


        public void SaveBoardState() // 보드의 상태를 저장해요
        {
            MergeBoardSaveData saveData = new MergeBoardSaveData
            {
                characterID = GameManager.Instance.selectedCharacterData.characterID, // 캐릭터 아이디 불러오기
                slotDataList = new List<SlotSaveData>() // 각 슬롯 데이터를 저장해야하므로, 슬롯 데이터를 리스트로 만들기
            }; // 새 세이브 데이터를 저장시킬 곳 만들기

            for (int i = 0; i < slotList.Count; i++) //슬롯 리스트 안의 개수 만큼 반복
            {
                var slot = slotList[i]; //슬롯 리스트의 i번째 칸의 위치 저장
                var child = slot.childCount > 0 ? slot.GetChild(0) : null; 
                //만약 슬롯의 자식 개수가 0이 아니라면, 자신의 0번째 자식 리턴, 아니면 널 리턴
                
                SlotSaveData slotData = new SlotSaveData // 새로운 빈 슬롯 데이터를 만든다
                {
                    slotIndex = i // 슬롯 데이터의 슬롯 인덱스를 i번으로 설정
                };

                if (child != null) //만약 자식이 있다면 (에코코어가 있다면)
                {
                    var echo = child.GetComponent<EchoCore>(); // 에코의 데이터 가져오기 
                    EchoCoreSo echoData = echo.GetCurrentData(); //에코 데이터 안의 so 가져오기
                    slotData.itemName = echoData.coreName; //에코 코어의 아이디 저장
                    slotData.sprite = echoData.echoSprite; //에코 코어의 스프라이트 저장
                    slotData.growthLevel = echoData.growthCount; //에코 코어의 성장 정도 저장
                }
                else // 만약 자식이 없다면 (빈 칸이라면)
                {
                    slotData.itemName = string.Empty; // 예외처리: 빈칸 설정
                    slotData.sprite = null; // 슬롯의 스프라이트 널값 설정
                    slotData.growthLevel = 0; // 그 칸의 성장 레벨 0으로 설정
                }
                
                saveData.slotDataList.Add(slotData); // 저장된 슬롯 데이터를 리스트에 추가
            }

            var equippedSlot = equipEchoCore.transform; //장착 슬롯의 위치 가져오기
            if (equippedSlot.childCount > 0) //만약 가져온 슬롯에 에코코어가 장착중이라면
            {
                var child = equippedSlot.GetChild(0); //자식의 위치 저장
                var echo = child.GetComponent<EchoCore>(); //자식의 에코 데이터 가져오기
                EchoCoreSo echoData = echo.GetCurrentData(); //에코 데이터 안에 있는 실질적 데이터인 SO 가져오기

                saveData.equipmentCoreData = new SlotSaveData() //저장할 새 공간 만들기
                {
                    slotIndex = -1, // 장착중인 에코 코어는 -1로 표기
                    itemName = echoData.coreName, // 이름 설정
                    sprite = echoData.echoSprite, // 스프라이트 설정
                    growthLevel = echoData.growthCount // 성장 단계 설정
                };

            }
            else // 장착중인 에코 코어가 존재하지 않다면
            {
                saveData.equipmentCoreData = new SlotSaveData() //저장할 새 공간 만들기
                {
                    slotIndex = -1, // 장착중인 에코 코어는 -1로 표기
                    itemName = string.Empty, // 이름 설정
                    sprite = null, // 스프라이트 설정
                    growthLevel = 0 // 성장 단계 설정
                };
            }
            SaveManager.Instance.SaveCharacterMergeData(saveData); //모든 슬롯을 저장했다면 전체를 보내 json으로 저장
            
            LoadBoardState(); //저장한 데이터 바로 불러오기
        }

        public void LoadBoardState() // 보드의 저장 값을 바탕으로 보드의 모든 칸의 데이터를 불러온다
        {
            if (parent.activeSelf == false) return;
            
            string characterID = GameManager.Instance.selectedCharacterData.characterID; // 캐릭터의 ID 값을 가져온다 (아이디 값으로 머지 데이터 현황을 가져올 것이기 때문)
            MergeBoardSaveData saveData = SaveManager.Instance.LoadMergeDataForCharacter(characterID); //세이브 데이터를 캐릭터 ID를 통해 가져온다

            if (saveData == null) // 만약 세이브 데이터가 없다면
            {
                Logging.Log($"[MargeBoard] 저장된 데이터가 없습니다. 캐릭터 ID: {characterID}"); // 로그를 출력하고
                return; //종료시킨다
            }
            
            if (saveData.equipmentCoreData != null && !string.IsNullOrEmpty(saveData.equipmentCoreData.itemName))
            {
                if (equipEchoCore.transform.childCount > 0)
                {
                    Destroy(equipEchoCore.transform.GetChild(0).gameObject);
                }
                
                var so = EchoCoreDatabase.Instance.GetEchoCoreSo(saveData.equipmentCoreData.itemName);
                if (so != null)
                {
                    var echoObj = Instantiate(echoPrefab, equipEchoCore.transform);

                    EchoCore echo = echoObj.GetComponent<EchoCore>(); // 생성시킨 에코 코어를 원래 있었던 데이터로 덮어씌우기 위해 컴포넌트를 가져온다
                    echo.SetEchoData(so); // 에코 데이터를 원래 있던 것으로 설정시킨다.
                    if (so != null)
                        GameManager.Instance.selectedWeaponEchoData = so;
                    else
                        GameManager.Instance.selectedWeaponEchoData = null;
                }
            }

            foreach (var slotData in saveData.slotDataList) // 세이브 데이터의 슬롯 리스트를 하나씩 검사한다
            {
                if (slotData.slotIndex < 0 || slotData.slotIndex >= slotList.Count) // 만약 이 슬롯 데이터의 인덱스값이 0보다 작거나,
                                                                                    // 슬롯 데이터의 인덱스값이 총 들어있어야 하는 데이터의 수보다 많다면
                {
                    Logging.LogWarning($"[MargeBoard] 잘못된 슬롯 인덱스: {slotData.slotIndex}"); //오류를 띄우고
                    continue; // 계속한다 (나머지는 로드해야함)
                }
                
                var slot = slotList[slotData.slotIndex]; // 슬롯리스트에 저장되어있는 각 슬롯의 위치를 현재 검사하는 슬롯의 인덱스번호로 찾아온다
                //슬롯 리스트의 인덱스 순서와 저장되어있는 인덱스 순서는 무조건 같다 다르면 인생망한다.

                if (slot.childCount > 0) //만약 검사한 슬롯 안에 에코 코어 오브젝트가 있다면
                {
                    Destroy(slot.GetChild(0).gameObject); // 그 에코 코어를 일단 없애 데이터를 불러오기 위한 준비를 한다 (나중에 풀메니저로 바꿀까)
                }

                //저장된 아이템이 있다면 (저장이 된 아이템이 있다면 이름을 가지고 있을 것이기 때문)
                if (!string.IsNullOrEmpty(slotData.itemName))
                {
                    EchoCoreSo coreData = EchoCoreDatabase.Instance.GetEchoCoreSo(slotData.itemName); // 저장된 슬롯 데이터의 자식의 에코 코어 이름과 같은 이름인 SO를 가져옴
                    if (coreData == null) //만약 가져왔는데 널이라면
                    {
                        Debug.LogWarning($"[MargeBoard] 해당 ID의 에코 데이터 없음: {slotData.itemName}"); //데이터가 없는 것이므로 오류를 띄우고
                        continue; //계속 진행시킨다 (나머지는 불러올 수 있도록)
                    }
                    
                    GameObject echoObj = Instantiate(echoPrefab, slot); // 아이템이 있었던 칸에 새 빈 에코 코어를 생성시킨다
                    EchoCore echo = echoObj.GetComponent<EchoCore>(); // 생성시킨 에코 코어를 원래 있었던 데이터로 덮어씌우기 위해 컴포넌트를 가져온다
                    
                    echo.SetEchoData(coreData); // 에코 데이터를 원래 있던 것으로 설정시킨다.
                }
            }
        }

        public void LoadWeaponData()
        {
            string characterID = GameManager.Instance.selectedCharacterData.characterID; // 캐릭터의 ID 값을 가져온다 (아이디 값으로 머지 데이터 현황을 가져올 것이기 때문)
            MergeBoardSaveData saveData = SaveManager.Instance.LoadMergeDataForCharacter(characterID);

            if (saveData.equipmentCoreData != null && !string.IsNullOrEmpty(saveData.equipmentCoreData.itemName))
            {
                var so = EchoCoreDatabase.Instance.GetEchoCoreSo(saveData.equipmentCoreData.itemName);
                if (so != null)
                {
                    GameManager.Instance.selectedWeaponEchoData = so;
                }
            }
            else
            {
                GameManager.Instance.selectedWeaponEchoData = null;
            }
        }
    }
}
