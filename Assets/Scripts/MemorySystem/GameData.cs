using UnityEngine;
using System;

[Serializable]
public class GameData
{
    [SerializeField]
    string gameName;
    [SerializeField]
    int level;
    [SerializeField]
    float currentExp;
    [SerializeField]
    float money;


    public string GameName
    {
        get { return gameName; }
    }

    public GameData(string gameName, int level, float currentExp, float money)
    {
        this.gameName = gameName;
        this.level = level;
        this.currentExp = currentExp;
        this.money = money;
    }

    public GameData()
    {

    }
}
