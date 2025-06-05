using System.Collections.Generic;

namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class MergeSaveDataContainer
    {
        public List<MergeBoardSaveData> allCharacterBoards = new List<MergeBoardSaveData>(); //모든 캐릭터의 합치기 보드 통합 관리
    }
}