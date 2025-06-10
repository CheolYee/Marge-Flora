using System.IO;
using _00._Work._02._Scripts.Save;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.SaveManager
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
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
        
        private string UnlockSavePath => Application.persistentDataPath + "/characterUnlockData.json";
        public CharacterUnlockSaveData unlockData;

        public void SaveUnlockData()
        {
            string json = JsonUtility.ToJson(unlockData, true);
            File.WriteAllText(UnlockSavePath, json);
        }

        public void LoadUnlockData()
        {
            if (File.Exists(UnlockSavePath))
            {
                string json = File.ReadAllText(UnlockSavePath);
                unlockData = JsonUtility.FromJson<CharacterUnlockSaveData>(json);
            }
            else
            {
                unlockData = new CharacterUnlockSaveData();
                unlockData.unlockList.Add(new CharacterUnlockData { characterID = "1", isUnlocked = true });
                SaveUnlockData();
            }
        }

        public bool IsCharacterUnlocked(string characterID)
        {
            var data = unlockData.unlockList.Find(c => c.characterID == characterID);
            return data != null && data.isUnlocked;
        }

        public void UnlockCharacter(string characterID)
        {
            var data = unlockData.unlockList.Find(c => c.characterID == characterID);
            if (data != null)
                data.isUnlocked = true;
            else
                unlockData.unlockList.Add(new CharacterUnlockData { characterID = characterID, isUnlocked = true });

            SaveUnlockData();
        }
    }
}
