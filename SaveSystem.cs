using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveSlot(SlotData slotData, int slotNumber) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/slot" +  slotNumber + ".dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        SlotData data = new SlotData(slotData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SlotData LoadSlot(int slotNumber) {
        string path = Application.persistentDataPath + "/slot" +  slotNumber + ".dat";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SlotData data = formatter.Deserialize(stream) as SlotData;
            stream.Close();

            return data;
        }
        else {
            SlotData data = new SlotData();
            data.slotNumber = slotNumber;
            return data;
        }
    }
}
