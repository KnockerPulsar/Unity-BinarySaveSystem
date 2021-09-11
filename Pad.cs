using System;
using UnityEngine;

[System.Serializable]
public class PadData : SavableData
{
    public string testRefID;
    public Vector3 pos;
    public Quaternion rot;
    public bool active;
}

public class Pad : SavableBehaviour
{
    public RefSerialzationTest testRef;

    public override void load(SavableData data)
    {
        if (data is PadData)
        {
            PadData savableData = (PadData)(data);

            gameObject.SetActive(savableData.active);
            ID = savableData.ID;
            transform.position = savableData.pos;
            transform.rotation = savableData.rot;

                if (savableData.testRefID != String.Empty)
                testRef = SavableBehaviour.GetObject(savableData.testRefID)?.GetComponent<RefSerialzationTest>();
            else testRef = null;
        }
        else
            Debug.LogError("Passed data is of invalid type");

    }

    public override SavableData save()
    {
        PadData savableData = new PadData();
        savableData.ID = SavableBehaviour.GetID(this);
        savableData.active = gameObject.activeSelf;
        savableData.pos = transform.position;
        savableData.rot = transform.rotation;

        SavableBehaviour testRefSavable = testRef?.GetComponent<SavableBehaviour>();
        if (testRefSavable != null)
            savableData.testRefID = testRefSavable.ID;
        else
            savableData.testRefID = System.String.Empty;

        return savableData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            testRef = FindObjectOfType<RefSerialzationTest>();
            gameObject.SetActive(false);
        }
    }

    //private void Start()
    //{
    //    print(gameObject.ToString() + componentID);
    //}
}


// SerializationManager: manages data serialization
// SaveData: manages the data to be saved. Player data, object data 
// PlayerHandler: Handles player data saving
// Maybe create a SavableHandler? 
// It would do what? 
// It would have a SavableData object inside.

// Then use a SaveManager to manage the handlers?
// This would have a reference to all save handlers on start
// Then when saving, it would go through them in a specific order, then update the SaveData.current object, then call the serialization manager.
// When loading, get the saved data, then go and update the objects accordingly

// One extension would be to turn to IDs for storing references
// When saving, you would save a reference to the object you're referencing to
// When loading, you would get the component you want from that object
// This would probably need a custom function for every class/component that is savable though...