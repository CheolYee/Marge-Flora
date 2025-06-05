namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class SlotSaveData //슬롯 저장을 위한 클래스
    {
        public int slotIndex; // 슬롯 인덱스(몇번 칸에 있었나)
        public string itemID; // 아이템 아이디 (이름)
        public int growthLevel; // 성장 단계
    }
}
