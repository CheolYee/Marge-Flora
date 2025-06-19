using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Save;
using _00._Work._02._Scripts.Story.SO;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public CharacterDataSo selectedCharacterData;
        public EchoCoreSo selectedWeaponEchoData;
        public DungeonDataSo selectedDungeonData;
        public StoryContainerSo selectedStoryData;
        [field: SerializeField] public GameData SelectedGameData { get; private set; }
        [field: SerializeField] public StorySaveData SaveStoryData { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }

        private void Start()
        {
            MoneyManager.MoneyManager.Instance.OnMoneyChanged += SaveGameData;
            LoadWeaopnData();
            LoadStoryData();
        }

        private void LoadWeaopnData()
        {
            MergeBoardSaveData saveData = SaveManager.SaveManager.Instance.LoadMergeDataForCharacter(selectedCharacterData.characterID);
            if (saveData != null)
            {
                EchoCoreSo savedEcho = EchoCoreDatabase.Instance.GetEchoCoreSo(saveData.equipmentCoreData.itemName);
                selectedWeaponEchoData = savedEcho;
            }
        }

        public void LoadGameData()
        {
            SelectedGameData = SaveManager.SaveManager.LoadGameData();
        }

        private void LoadStoryData()
        {
            SaveStoryData = SaveManager.SaveManager.LoadStoryData();
            if (SaveStoryData.isFirstStory)
            {
                FadeManager.FadeManager.Instance.FadeToScene(3);
            }
        }

    public void SaveGameData()
        {
            SaveManager.SaveManager.SaveGameData(SelectedGameData);
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}
