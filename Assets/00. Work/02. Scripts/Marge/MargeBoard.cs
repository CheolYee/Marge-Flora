using System.Collections.Generic;
using _00._Work._02._Scripts.Manager.GameManager;
using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Save;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge
{
    public class MargeBoard : MonoBehaviour
    {
        public List<Transform> slotList;

        public void SaveBoardState() // 보드의 상태를 저장해요
        {
            MergeBoardSaveData saveData = new MergeBoardSaveData(); // 새 세이브 데이터를 저장시킬 곳 만들기
            saveData.characterID = GameManager.Instance.selectedCharacterData.characterID; // 캐릭터 아이디 불러오기
            saveData.slotDataList = new List<SlotSaveData>(); // 각 슬롯 데이터를 저장해야하므로, 슬롯 데이터를 리스트로 만들기

            for (int i = 0; i < slotList.Count; i++) //슬롯 리스트 안의 개수 만큼 반복
            {
                var slot = slotList[i]; //슬롯 리스트의 i번째 칸의 위치 저장
                var child = slot.childCount > 0 ? slot.GetChild(0) : null; 
                //만약 슬롯의 자식 개수가 0이 아니라면, 자신의 0번째 자식 리턴, 아니면 널 리턴
                
                SlotSaveData slotData = new SlotSaveData(); // 새로운 빈 슬롯 데이터를 만든다
                slotData.slotIndex = i; // 슬롯 데이터의 슬롯 인덱스를 i번으로 설정

                if (child != null) //만약 자식이 있다면 (에코코어가 있다면)
                {
                    var echo = child.GetComponent<EchoCoreSo>(); // 에코의 데이터 가져오기 
                    slotData.itemID = echo.coreID.ToString(); //에코 코어의 아이디 저장
                    slotData.growthLevel = echo.growthCount; //에코 코어의 성장 정도 저장
                }
                else // 만약 자식이 없다면 (빈 칸이라면)
                {
                    slotData.itemID = string.Empty; // 예외처리: 빈칸 설정
                    slotData.growthLevel = 0; // 그 칸의 성장 레벨 0으로 설정
                }
                
                saveData.slotDataList.Add(slotData); // 저장된 슬롯 데이터를 리스트에 추가
            }
            
            SaveManager.Instance.SaveCharacterMergeData(saveData); //모든 슬롯을 저장했다면 전체를 보내 json으로 저장
        }
    }
}
