using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string DirectoryPath;
    public static bool Initialized;
    
    public static void Init()
    {
        DirectoryPath = $"{Application.dataPath}/Saves/";

        // Check the directory and if it does not exist, create it
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }

        Initialized = true;
    }

    public static void Save(object DataToSave, string FileName = "Save")
    {
        if (!Initialized)
        {
            Init();
        }

        string JSONSave = JsonUtility.ToJson(DataToSave);

        if (File.Exists($"{DirectoryPath}{FileName}.json")) 
        {
            File.Delete($"{DirectoryPath}{FileName}.json");
        }

        StreamWriter SaveFileWriter = new StreamWriter($"{DirectoryPath}{FileName}.json");
        SaveFileWriter.WriteLine(JSONSave);
        SaveFileWriter.Close();
    }

    public static void Load<T>(out T LoadedData, string FileName = "Save")
    {
        if (!Initialized)
        {
            Init();
        }

        StreamReader SaveFileReader = new StreamReader($"{DirectoryPath}{FileName}.json");

        string JSONSave = SaveFileReader.ReadLine();

        LoadedData = JsonUtility.FromJson<T>(JSONSave);
    }
}