using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class SaveSystem
{
    #region PlayerPrefs

    public static void SaveByPlayerPrefs(string key, object data)
    {
        var json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

#if UNITY_EDITOR
        Debug.Log("Successfully saved data to PlayerPrefs.");
#endif
    }

    public static string LoadFromPlayerPrefs(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    #endregion

    #region JSON

    public static void SaveByJson(string saveFileName, object data)
    {
        var json = JsonUtility.ToJson(data);
        //不同平台路径不一样，所以path
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            Debug.Log($"Susscessfully saved data to {path}.");
#endif
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            Debug.Log($"Failed to save data to {path}. \n{exception}");
#endif
        }




    }

    public static T LoadFrowJson<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            return data;
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            Debug.Log($"Failed to Load data from {path}. \n{exception}");
#endif
            return default;
        }
    }

    #endregion

    #region Deleting

    public static void DeleteSaveFile(string deleteFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, deleteFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            Debug.Log($"Failed to  Delete {path}. \n{exception}");
#endif
        }
    }

    #endregion

}

