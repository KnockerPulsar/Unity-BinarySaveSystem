using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static PlayerHandler playerHandler;
    private static SavableHandler[] savableHandlers;

    private void Start()
    {
        playerHandler = GetComponent<PlayerHandler>();
        savableHandlers = FindObjectsOfType<SavableHandler>();
        SaveData.current.playerData = new PlayerData();
        SaveData.current.savables = new List<SavableData>(savableHandlers.Length);
        for (int i = 0; i < savableHandlers.Length; i++)
        {
            SavableData curr = savableHandlers[i].save();
            SaveData.current.savables.Add(curr);
        }
        Save();
    }

    public static void Save()
    {
        SaveData.current.playerData = playerHandler.save();
        for (int i = 0; i < savableHandlers.Length; i++)
        {
            SavableData curr = savableHandlers[i].save();
            SaveData.current.savables[i] = curr;
        }
        SerializationManager.save("save1", SaveData.current);
    }

    public static void Load()
    {
        SaveData.current = (SaveData)SerializationManager.load(Application.persistentDataPath + "/saves/" + "save1" + ".save");
        playerHandler.load(SaveData.current.playerData);
        for (int i = 0; i < SaveData.current.savables.Count; i++)
        {
            SavableData curr = SaveData.current.savables[i];
            savableHandlers[i].load(curr);
        }
    }
}
