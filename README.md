# Unity SaveSystem
 A save system using the binary formatter for unity. Inspired by  [How to Build A Save System in Unity](https://youtu.be/5roZtuqZyuw), reference "serialization" made possible by [Bunny83's UUID system](https://github.com/Bunny83/UUID).

 What's currently Implemented
  1. A `SavableData` base class that all savables should inherit from
  2. A `SaveData` class that collects all the data before we write it.
  3. A `SaveManager` class that's responsible for handling the updating of the data upon saving and calling the serializer. Also, it calls the serializer to fetch the saved data, converts it back to SaveData, and goes over the objects we saved before and overrides/loads the data.
  The `SaveManager` is also responsible for fetching the save handlers upon starting and initializing the data of SaveData.
  4. A `SavableBehaviour` class built on Bunny83's UUID class. I added 2 extra functions `save()` and `load()`. You should inherit from this class when you want to save a component's data. `SavableBehaviour` already inherits from `MonoBehaviour` so you can use it the same as any other monobehaviour.
  5. Save data classes like `SavableData` and its children for example, are data containers used to store the data inside `SaveData.current`.  As said before, upon starting, the SaveManager fetches all `SavableBehaviour`s, initializes the `SaveData.current` and does one initial save if no save data is found.

How it works?
  1. Saving
    1. Upon starting (when `Start()` is called), the save manager grabs all the `SavableBehaviour`s and stores them, then it goes over every property of SaveData.current and initializes it. Then calls `Save()` if no save data can be loaded.
    2. `SaveManager.Save()` calls the `save()` method of every `SavableBehaviour` and stores the returned values in the corresponding container in `SaveData.current`. So for example, for player data, it requires only calling `playerHandler.save()` and storing the result in `SaveData.current.playerData`. For a list of savables, it iterates over each savableHandler, calls `save()`, stores the returned value in `SaveData.current.savables`.
    3. Finally, `SerializationManager.Save()` is called with the save name as the first parameter (hardcoded as "save1" for now) and `SaveData.current` as the second parameter.
  1. Loading
    1. Upon calling the `Load()` method inside SaveManager, the serialization manager fetches the binary data, deserializes it, and converts the resulting object to a `SaveData` object, which is stored inside `SaveData.current`.
    2. Then we go over each `SavableBehaviour` type,  calling the `load()` function and passing in the appropriate data as a parameter, the handler then updates the gameobject's data as defined in the function.

What can be improved?
  1. Code clean up and documentation.
  2. Moving the player to `SavableBehaviour` and ditching the handler implementation.
	
Notes
  1. This method allows for serializing built in C# types (bools, ints, floats, strings, etc..) for unity's types, you'll have to  implement a serialization surrogate to handle the conversion manually. Examples for this are the Vector3 surrogate (Vec3SS) and  the Quaternion surrogate (QuatSS)
  2. You can serialize references to monobehaviours by storing their GUID and checking for them on load.
  3. Currently, not everything uses SavableBehaviour. But that's the direction I'm going in.
  4. I haven't tested this in a **real** project, so it might have a few ~~hundred~~ bugs to iron out.
	







