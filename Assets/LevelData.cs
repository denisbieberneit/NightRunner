using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string[] ownedLevelNames;

    public LevelData(LevelItem[] levels)
    {
        int i = 0;
        foreach (LevelItem item in levels)
        {
            if (item.owned)
            {
                i++;
            }
        }
        ownedLevelNames = new string[i];
        i = 0;
        foreach (LevelItem item in levels)
        {
            if (item.owned)
            {
                ownedLevelNames[i] = item.levelName;
                i++;
            }
        }
    }
}
