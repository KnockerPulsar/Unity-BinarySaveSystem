using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RefSerializationTestData : SavableData
{
    public int value;
}

public class RefSerialzationTest : SavableBehaviour
{
    public int val = 69;

    public override void load(SavableData data)
    {
        if (data is RefSerializationTestData)
        {
            ID = data.ID;
            val = ((RefSerializationTestData)(data)).value;
        }
        else
            Debug.LogError("Passed data is of invalid type");
    }

    public override SavableData save()
    {
        RefSerializationTestData data = new RefSerializationTestData();
        data.ID = ID;
        data.value = val;
        return data;
    }
}
