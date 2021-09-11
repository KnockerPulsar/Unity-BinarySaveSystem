using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// How to Build A Save System in Unity: https://youtu.be/5roZtuqZyuw

public class PlayerHandler : MonoBehaviour
{
    PlayerData playerData;
    public PlayerData save()
    {
        if (playerData == null)
            playerData = new PlayerData();
        playerData.pos = transform.position;
        playerData.rot = transform.rotation;
        return playerData;
    }

    public void load(PlayerData playerData)
    {
        if (playerData != null)
        {
            transform.position = playerData.pos;
            transform.rotation = playerData.rot;
        }
    }
}
