using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// How to Build A Save System in Unity: https://youtu.be/5roZtuqZyuw

public class SavableHandler : MonoBehaviour
{
    PadData savableData;

    //public Savable save()
    //{
    //    if (savableData == null) savableData = new PadData();

    //    savableData.active = gameObject.activeSelf;
    //    savableData.pos = transform.position;
    //    savableData.rot = transform.rotation;

    //    return savableData;
    //}

    //public void load(Savable data)
    //{
    //    if (data is PadData)
    //    {
    //        savableData = (PadData)(data);

    //        gameObject.SetActive(savableData.active);
    //        transform.position = savableData.pos;
    //        transform.rotation = savableData.rot;
    //    }
    //}
}
