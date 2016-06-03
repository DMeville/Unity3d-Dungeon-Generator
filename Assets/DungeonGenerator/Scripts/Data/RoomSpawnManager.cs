using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoomSpawnManager : ScriptableObject{
    public List<RoomSpawnData> enemySpawnLocations = new List<RoomSpawnData>();
}
