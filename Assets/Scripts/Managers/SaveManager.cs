using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    static public void SaveData<T>(T data)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/" + typeof(T).ToString() + ".data";
        var stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    static public T LoadData<T>()
    {
        var path = Application.persistentDataPath + "/" + typeof(T).ToString() + ".data";
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found in " + path);
            return default;
        }

        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Open);

        var data = (T)formatter.Deserialize(stream);
        stream.Close();

        return data;
    }
}