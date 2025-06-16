using System;
using _00._Work._02._Scripts.Character;
using _00._Work._02._Scripts.Marge.SO;
using _00._Work._02._Scripts.Save;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public CharacterDataSo selectedCharacterData;
        public EchoCoreSo selectedWeaponEchoData;
        public DungeonDataSo selectedDungeonData;
        [field:SerializeField] public GameData SelectedGameData { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }

        private void Start()
        {
            MoneyManager.MoneyManager.Instance.OnMoneyChanged += SaveGameData;
        }

        public void LoadGameData()
        {
            SelectedGameData = SaveManager.SaveManager.LoadGameData();
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
