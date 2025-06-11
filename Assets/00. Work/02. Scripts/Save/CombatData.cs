using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge.SO;

namespace _00._Work._02._Scripts.Save
{
    [System.Serializable]
    public class CombatData // 보내기 용도
    {
        public CharacterDataSo characterData; //선택된 캐릭터 데이터
        public EchoCoreSo equippedEcho; //선택된 캐릭터의 무기 에코코어
        public DungeonDataSo dungeonData; //던전 데이터
        
    }
}
