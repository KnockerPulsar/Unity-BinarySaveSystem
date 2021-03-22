# Unity SaveSystem
 A save system using the binary formatter for unity. Inspired by  "How to Build A Save System in Unity: https://youtu.be/5roZtuqZyuw"

 What's currently Implemented
  1. A SaveData Object that houses all the data we need saved, ie. PlayerData and SavableData.
  2. A SaveManager class that's responsible for handling the updating of the data upon saving and calling the serializer. Also,   itcalls the serializer to fetch the saved data, converts it back to SaveData, and goes over the objects we saved before andupdates/  load the data.
  The SaveManager is also responsible for fetching the save handlers upon starting and initializing the data of SaveData.
  3. Handlers, for each type of save data, we will require a handler that stores a copy of the data (not really needed, as we   candirectly access SaveData.current.attrib to access the currently saved data, it helps with making things clear though). Each   gameobject that should be saved should have a handler component.
  4. Save data classes like PlayerData and SavableData for example, are data containers used to store the data inside SaveDatacurrent   and handlers.
  As said before, upon starting, the SaveManager fetches all handlers, initializes the SaveData.current and does one initial save.

How it works?
  1. Saving
    1. Upon starting (when Start() is called), the save manager grabs all the handlers and stores them, then it goes over every    property of SaveData.current and initializes it. Then calls Save()
    2. Save() calls the save() method of every handler and stores the returned values in the corresponding container in SaveData    current. So for example, for player data, it requires only calling playerHandler.save() and storing the result in SaveData    current.playerData. For a list of savables, it iterates over each savableHandler, calls save(), stores the returned value in    SaveData.current.savables.
    3. Finally, SerializationManager.Save() is called with the save name as the first parameter (hardcoded as "save1" for now) and    SaveData.current as the second parameter.
  1. Loading
    1. Upon calling the Load() inside SaveManager, the serialization manager fetches the binary data, deserializes it, and converts    the resulting object to a SaveData object, which is stored inside SaveData.current.
    2. Then we go over each handler type,  calling the load() function  and passing in the appropriate data as a parameter, the    handler then updates the gameobject's data as defined in the function.

What can be improved?
  1. We can implement a base handler interface (IHandler) to unify the function signature. From this, we can inherit handlers for  each data type we wish to serialize.
	
Notes
  1. This method allows for serializing built in C# types (bools, ints, floats, strings, etc..) for unity's types, you'll have to  implement a serialization surrogate to handle the conversion manually. Examples for this are the Vector3 surrogate (Vec3SS) and  the Quaternion surrogate (QuatSS)


	







