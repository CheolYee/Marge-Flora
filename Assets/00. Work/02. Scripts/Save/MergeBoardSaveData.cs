using System.Collections.Generic;

namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class MergeBoardSaveData
    {
        public string characterID; // 캐릭터 별로 저장
        public List<SlotSaveData> slotDataList = new List<SlotSaveData>(); //각 슬롯 세이브 데이터를 저장할 리스트
    }
}
