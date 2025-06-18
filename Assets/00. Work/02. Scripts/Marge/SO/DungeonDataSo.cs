using _00._Work._02._Scripts.Story.SO;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge.SO
{
    [CreateAssetMenu(fileName = "DungeonDataSo", menuName = "SO/DungeonDataSo")]
    public class DungeonDataSo : ScriptableObject
    {
        [Header("Dungeon UI")]
        public string dungeonName; //던전 이름
        public Sprite dungeonIcon; //던전 아이콘
        public Sprite dungeonElementIcon; //던전 +데미지 속성 아이콘
        public int rewordGold; //성공 시 획득 골드
        public string clearRecommendText; //권장 무기 레벨
        
        [Header("CombatData")]
        public string dungeonId; //던전 아이디
        public string weaknessElement; //던전 약점 속성
        public GameObject enemyPrefab; //던전 적 프리팹
        public Transform enemySpawnPos; //던전 적 생성 위치
        public float timeLimit; //시간 제한

        [Header("Story")] 
        public bool hasStory;
        public StoryContainerSo story;
        
        [Header("Unlock")]
        public bool hasUnlock;
        public string unlockId;
    }
}
