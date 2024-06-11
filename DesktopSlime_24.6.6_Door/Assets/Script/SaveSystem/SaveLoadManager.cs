using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    private GameSave gameSave;
    private List<ISaveable> saveableList = new();

    private void Update()
    {
        // TEST___________________________________________________________________
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveDataToFile();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadDataFromFile();
        }
    }

    public void LoadDataFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/DesktopDungeon.dat"))
        {
            gameSave = new();

            FileStream file = File.Open(Application.persistentDataPath + "/DesktopDungeon.dat", FileMode.Open);

            gameSave = (GameSave)bf.Deserialize(file);

            foreach (ISaveable saveable in saveableList)
                saveable.Load(gameSave);

            file.Close();
        }
    }

    public void SaveDataToFile()
    {
        gameSave = new();

        foreach (ISaveable saveable in saveableList)
            saveable.Save(gameSave);

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/DesktopDungeon.dat", FileMode.Create);

        bf.Serialize(file, gameSave);

        file.Close();
    }

    public void RegisterSaveable(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }

    public void DeregisterSaveable(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }
}
