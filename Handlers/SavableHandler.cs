using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// How to Build A Save System in Unity: https://youtu.be/5roZtuqZyuw

public class SavableHandler : MonoBehaviour
{
    SavableData savableData;

    public SavableData save()
    {
        if (savableData == null) savableData = new SavableData();

        savableData.active = gameObject.activeSelf;
        savableData.pos = transform.position;
        savableData.rot = transform.rotation;
        return savableData;
    }

    public void load(SavableData data)
    {
        savableData = data;
        gameObject.SetActive(data.active);
        transform.position = data.pos;
        transform.rotation = data.rot;
    }
}
