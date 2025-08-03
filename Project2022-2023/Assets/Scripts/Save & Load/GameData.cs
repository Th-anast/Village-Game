using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float health;
    public float level;
    public int levelUp;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> deadEnemies;

    public GameData()
    {
        health = 100f;
        level = 0f;
        levelUp = 1;
        playerPosition = new Vector3(1533.609f, 85.9f, 1441.59f);
        deadEnemies = new SerializableDictionary<string, bool>();
    }
}
