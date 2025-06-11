using _00._Work._02._Scripts.Save;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.DungeonDataManager
{
    public class DungeonDataManager : MonoSingleton<DungeonDataManager>
    {
        public CombatData CurrentCombatData { get; private set; }
    }
}
