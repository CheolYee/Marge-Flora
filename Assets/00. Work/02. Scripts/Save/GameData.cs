using System.Collections.Generic;

namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class GameData
    {
        public int money; // 돈 저장
        public List<string> clearStoryIds; //클리어한 스토리의 아이디 저장
    }
}