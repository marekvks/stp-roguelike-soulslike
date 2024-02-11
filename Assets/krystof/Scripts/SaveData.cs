using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private DataToSave _data = new DataToSave();
    private string _className = "Class 1";
    DataToSave SetAtributes(string className)
    {
        _data.ClassName = className;
        return _data;
    }

    public void SetClass(string className)
    {
        _className = className;
    }
    public void SaveToJSON()
    {
        string playerData = JsonUtility.ToJson(SetAtributes(_className));
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        
        System.IO.File.WriteAllText(filePath, playerData);
    }

    public DataToSave LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        string playerData = System.IO.File.ReadAllText(filePath);

        _data = JsonUtility.FromJson<DataToSave>(playerData);
        
        return _data;
    }
}

[System.Serializable]
public class DataToSave
{
    public string ClassName;
}
