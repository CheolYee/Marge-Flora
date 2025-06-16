using System.IO;
using _00._Work._02._Scripts.Save;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.SaveManager
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        public void Start()
        {
            if (Instance == this)
                DontDestroyOnLoad(this);
        }

        private string SavePath => Application.persistentDataPath + "/margeSaveData.json"; // 각 캐릭의 합치기 데이터가 저장될 파일 이름

        public void SaveCharacterMergeData(MergeBoardSaveData newData) //캐릭터의 합치기 데이터를 저장한다
        {
            MergeSaveDataContainer container = LoadAllMergeData(); // 컨테이너에 모든 합치기 데이터를 담는다

            // 컨테이너에 있는 합치기 데이터를 기존 데이터에 옮겨담는다
            int index = container.allCharacterBoards.FindIndex(data => data.characterID == newData.characterID);
            if (index >= 0) container.allCharacterBoards[index] = newData;
            else container.allCharacterBoards.Add(newData);

            string json = JsonUtility.ToJson(container, true); // 다 담겨진 기존 데이터를 Json 파일로 저장시킨다
            File.WriteAllText(SavePath, json); // 모든 텍스트를 읽는다
            Logging.Log($"Saved {SavePath}"); //로그를 띄워 어디 저장되었는지 경로를 알려준다
        }
        
        public MergeBoardSaveData LoadMergeDataForCharacter(string characterID) //각 캐릭터의 데이터를 불러오기
        {
            MergeSaveDataContainer container = LoadAllMergeData(); // 그 캐릭터의 머지 데이터를 컨테이너에 담는다
            return container.allCharacterBoards.Find(data => data.characterID == characterID); // 캐릭터 아이디를 찾아 그에 맞는 데이터를 보낸다
        }

        private MergeSaveDataContainer LoadAllMergeData() // 모든 데이터를 로드한다
        {
            if (!File.Exists(SavePath)) //만약 저장 경로에 데이터가 없으면, 새로운 컨테이너를 만든다
                return new MergeSaveDataContainer();

            string json = File.ReadAllText(SavePath); // 세이브 경로에 있는 모든 파일의 텍스트를 읽어온다
            return JsonUtility.FromJson<MergeSaveDataContainer>(json); // 데이터를 보낸다
        }
        
        private string CharacterSavePath => Application.persistentDataPath + "/lastCharacter.json"; // 현재 쓰고 있는 캐릭터가 저장 될 경로

        public void SaveLastUsedCharacterID(string id) // 마지막으로 쓴 캐릭터의 아이디를 저장한다
        {
            File.WriteAllText(CharacterSavePath, id);
        }

        public string LoadLastUsedCharacterID() //마지막으로 쓴 캐릭터의 아이디를 불러온다
        {
            if (!File.Exists(CharacterSavePath)) //만약 파일 경로에 파일이 없다면
                return null; //널을 반환한다

            return File.ReadAllText(CharacterSavePath); // 파일이 있다면 모든 텍스트를 읽고 반환한다
        }
        
        private string UnlockSavePath => Application.persistentDataPath + "/characterUnlockData.json"; // 캐릭터의 해금 유무를 저장할 경로
        private CharacterUnlockSaveData _unlockData; // 캐릭터 해금 유무를 가져오기 위해 불러올 세이브 데이터 컨테이너

        private void SaveUnlockData() // 캐릭터 해금 유무 저장
        {
            string json = JsonUtility.ToJson(_unlockData, true); //언락 데이터의 리스트를 JSON 파일로 뽑기
            File.WriteAllText(UnlockSavePath, json); // 모든 텍스트를 읽은 후 덮어쓰기
        }

        public void LoadUnlockData() // 캐릭터 해금 유무 로드
        {
            if (File.Exists(UnlockSavePath)) //만약 캐릭터 해금 파일이 존재하면
            {
                string json = File.ReadAllText(UnlockSavePath); // 현재 저장된 데이터 파일의 경로로 찾아가 모든 파일 데이터를 읽고
                _unlockData = JsonUtility.FromJson<CharacterUnlockSaveData>(json); //데이터를 덮어씌운다
            }
            else
            {
                _unlockData = new CharacterUnlockSaveData(); //새 캐릭터 해금 데이터 컨테이너를 생성하고
                _unlockData.unlockList.Add(new CharacterUnlockData { characterID = "1", isUnlocked = true }); // 기본 캐릭터를 해금시켜준다
                SaveUnlockData(); // 새로 만든 데이터 컨테이너를 저장한다
            }
        }

        public bool IsCharacterUnlocked(string characterID) //캐릭터가 해금되어있는지 확인
        {
            //확인할 캐릭터의 아이디와 저장되어있는 캐릭터의 아이디 값을 비교하여 저장
            var data = _unlockData.unlockList.Find(c => c.characterID == characterID);
            return data != null && data.isUnlocked; //만약 찾은 데이터가 없지 않고, 캐릭터가 열려있다면 true, 아니면 false를 반환한다.
        }

        public void UnlockCharacter(string characterID) // 캐릭터의 언락 데이터 가져오기
        {
            var data = _unlockData.unlockList.Find(c => c.characterID == characterID);
            if (data != null)
                data.isUnlocked = true;
            else
                _unlockData.unlockList.Add(new CharacterUnlockData { characterID = characterID, isUnlocked = true });
            SaveUnlockData();
        }
        private static string GameDataSavePath => Application.persistentDataPath + "/gameSaveData.json";

        public static void SaveGameData(GameData data) //게임 데이터 저장
        {
            string json = JsonUtility.ToJson(data, true); //트루는 중간에 보기 좋게 들여쓰기
            File.WriteAllText(GameDataSavePath, json);
        }

        public static GameData LoadGameData() //게임 데이터 로드
        {
            if (!File.Exists(GameDataSavePath))
            {
                Logging.Log($"세이브 데이터를 찾을 수 없어 새로 생성합니다");
                return new GameData();
            }
            
            string json = File.ReadAllText(GameDataSavePath);
            return JsonUtility.FromJson<GameData>(json);
        }
    }
}
