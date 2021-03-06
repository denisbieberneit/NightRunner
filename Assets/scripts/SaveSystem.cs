using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/inventory.noob";

        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static InventoryData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.noob";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Error while loading inventory");
            return null;
        }
    }

    public static void SaveLevels(LevelItem[] levelItems)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/levelItems.level";

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelItems);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static LevelData LoadLevels()
    {
        string path = Application.persistentDataPath + "/levelItems.level";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Error while loading levels");
            return null;
        }
    }

}
