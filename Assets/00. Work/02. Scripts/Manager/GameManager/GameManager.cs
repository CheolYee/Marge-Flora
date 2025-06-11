using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge.SO;

namespace _00._Work._02._Scripts.Manager.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public CharacterDataSo selectedCharacterData;
        public EchoCoreSo selectedWeaponEchoData;
        public DungeonDataSo selectedDungeonData;
    }
}
