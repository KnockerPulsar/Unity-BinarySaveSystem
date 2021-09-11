using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEditor;

public class SaveManager : MonoBehaviour
{
    private static PlayerHandler playerHandler;
    private static SavableBehaviour[] savableHandlers;
    //private static MonoBehaviour[] monos;
    //private static Dictionary<string, MonoBehaviour> guidsToComponents = new Dictionary<string, MonoBehaviour>();
    //private static Dictionary<MonoBehaviour, string> compsToGuids = new Dictionary<MonoBehaviour, string>();

    private void Awake()
    {
        playerHandler = GetComponent<PlayerHandler>();
        var temp = FindObjectsOfType<MonoBehaviour>();
        savableHandlers = temp.OfType<SavableBehaviour>().ToArray();

        Load();

        // No save data loaded, create new save data
        if (SaveData.current.playerData == null)
        {
            SaveData.current.playerData = new PlayerData();
            SaveData.current.savables = new List<SavableData>(savableHandlers.Length);
            SaveData.current.IDs = new List<string>(savableHandlers.Length);
            Save();
        }
    }

    public static void Save()
    {
        SaveData.current.playerData = playerHandler.save();
        if (SaveData.current.savables.Count > 0)
            for (int i = 0; i < savableHandlers.Length; i++)
            {
                SavableData curr = savableHandlers[i].save();
                SaveData.current.savables[i] = curr;
                SaveData.current.IDs[i] = curr.ID;
            }
        else
            for (int i = 0; i < savableHandlers.Length; i++)
            {
                SavableData curr = savableHandlers[i].save();
                SaveData.current.savables.Add(curr);
                SaveData.current.IDs.Add(curr.ID);
            }
        SerializationManager.save("save1", SaveData.current);
    }

    public static void Load()
    {
        SaveData.current = (SaveData)SerializationManager.load(Application.persistentDataPath + "/saves/" + "save1" + ".save");

        playerHandler.load(SaveData.current.playerData);
        for (int i = 0; i < SaveData.current.savables?.Count; i++)
        {
            SavableData curr = SaveData.current.savables[i];
            savableHandlers[i].load(curr);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData.current = new SaveData();
        }
    }
}
