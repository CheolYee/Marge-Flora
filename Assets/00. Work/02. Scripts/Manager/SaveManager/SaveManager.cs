using System.IO;
using _00._Work._02._Scripts.Save;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.SaveManager
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        private string SavePath => Application.persistentDataPath + "/margeSaveData.json";

        public void SaveCharacterMergeData(MergeBoardSaveData newData) //캐릭터의 합치기 데이터를 저장
        {
            MergeSaveDataContainer container = LoadAllMergeData();

            // 기존 데이터 덮어쓰기
            int index = container.allCharacterBoards.FindIndex(data => data.characterID == newData.characterID);
            if (index >= 0) container.allCharacterBoards[index] = newData;
            else container.allCharacterBoards.Add(newData);

            string json = JsonUtility.ToJson(container, true);
            File.WriteAllText(SavePath, json);
        }
        
        public MergeBoardSaveData LoadMergeDataForCharacter(string characterID) //캐릭터의 데이터를 불러오기
        {
            MergeSaveDataContainer container = LoadAllMergeData();
            return container.allCharacterBoards.Find(data => data.characterID == characterID);
        }

        private MergeSaveDataContainer LoadAllMergeData()
        {
            if (!File.Exists(SavePath))
                return new MergeSaveDataContainer();

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<MergeSaveDataContainer>(json);
        }
    }
}
