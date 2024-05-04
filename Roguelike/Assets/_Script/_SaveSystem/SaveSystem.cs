using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <param name="data">要保存的数据</param>
    public static void SaveByJson(string saveFileName, object data)
    {
        //将数据转换成Json形式
        var json = JsonUtility.ToJson(data);
        //Debug.Log($"{saveFileName}    "+json);
        //定义文件路径，将持久数据文件路径和文件名合并
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            //将json形式数据写入文本文件内
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            //Debug.Log($"{saveFileName}数据存储成功，存储路径为：{path}");
#endif
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            //Debug.LogError($"存储数据失败：{path}。\n{exception}");
#endif

        }
    }

    //通过Json读取数据
    public static T LoadFromJson<T>(string saveFileName)
    {
        //定义文件路径，将持久数据文件路径和文件名合并
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
            Debug.LogError($"读取数据失败：{path}.\n{exception}\n新创建一个为空的文件");
#endif
            //SaveByJson(saveFileName);
            return default;
        }
    }

    //删除存储的数据
    public static void DeleteSaveFile(string saveFileName)
    {
        //定义文件路径，将持久数据文件路径和文件名合并
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            Debug.LogError($"删除数据失败：{path}.\n{exception}");
#endif

        }
    }
}

