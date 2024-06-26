using System;
using System.IO;
using UnityEngine;
using static SaveSystem;

public class SaveSystem
{

    public class SaveDataExample : SaveData<SaveDataExample>
    {
        protected override string FileName() => "ExampleData";

        public bool data1;


    }

    public static SaveDataExample Data() => SaveDataExample.Get(ref _saveData);
    public static SaveDataExample Data2() => SaveDataExample.Get(ref _saveData2);
    public static void Save()
    {
        Data().Save();
        Data2().Save(); 
    }



    private static SaveDataExample _saveData;
    private static SaveDataExample _saveData2;


}

[Serializable]
public abstract class SaveData<T> where T : SaveData<T>
{

    protected virtual string DataPath() => Application.persistentDataPath + "Saves";
    protected abstract string FileName();

    public void Save()
    {
        DirectoryCheck();
        using StreamWriter save = File.CreateText(DataPath() + "/" + FileName() + ".json");
        save.WriteLine(JsonUtility.ToJson(this, true));
    }

    public void Load()
    {
        DirectoryCheck();
        if (!File.Exists(DataPath() + FileName())) Save();
        using StreamReader load = File.OpenText(DataPath() + "/" + FileName() + ".json");
        JsonUtility.FromJsonOverwrite(load.ReadToEnd(), this);
    }

    protected void DirectoryCheck() { if (Directory.Exists(DataPath())) Directory.CreateDirectory(DataPath()); }

    public static T Get(ref T data)
    {
        if (data == null)
        {
            data = Activator.CreateInstance<T>();
            data.Load();
        }
        return data;
    }
}

/*

public interface ISaveSystem<T> where T : SaveData
{
    public abstract void Save();
    public abstract void Load();


    protected static T INew(ref T data)
    {
        data = Activator.CreateInstance<T>();
        return data;
    }
    protected static void ISave(ref T data, T newData = null)
    {
        if (newData == null) data = Activator.CreateInstance<T>();
        else data = newData;
        data.Save();
    }
    protected static T ILoad(ref T data)
    {
        if (data == null) Activator.CreateInstance<T>();
        data.Load();
        return data;
    }





}
 */