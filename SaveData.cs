using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// How to Build A Save System in Unity: https://youtu.be/5roZtuqZyuw
[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
                _current = new SaveData();
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public PlayerData playerData;
    public List<SavableData> savables;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 pos;
    public Quaternion rot;
}
[System.Serializable]
public class SavableData
{
    public string ID;
    public Vector3 pos;
    public Quaternion rot;
    public bool active;
}
