using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void SaveGame(Nature nature, GridMap gridmap, ShopManager shopManager, InventoryManager inventoryManager, TimeController timeController, int index)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/GestaoBiomasSave" + index + ".bin";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(nature, gridmap, shopManager, inventoryManager, timeController, index);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadGame(int index)
    {
        string path = Application.persistentDataPath + "/GestaoBiomasSave" + index + ".bin";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }
}
