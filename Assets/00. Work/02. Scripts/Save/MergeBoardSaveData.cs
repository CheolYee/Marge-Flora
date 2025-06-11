using System.Collections.Generic;

namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class MergeBoardSaveData
    {
        public string characterID; // 캐릭터 별로 저장
        public List<SlotSaveData> slotDataList = new List<SlotSaveData>(); //각 슬롯 세이브 데이터를 저장할 리스트
        
        public SlotSaveData equipmentCoreData; //만약 에코 코어가 장착되어있다면 장착한 코어의 데이터를 저장할 공간 마련
    }
}
