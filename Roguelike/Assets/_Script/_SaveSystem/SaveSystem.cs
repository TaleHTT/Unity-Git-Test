using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <param name="data">Ҫ���������</param>
    public static void SaveByJson(string saveFileName, object data)
    {
        //������ת����Json��ʽ
        var json = JsonUtility.ToJson(data);
        //Debug.Log($"{saveFileName}    "+json);
        //�����ļ�·�������־������ļ�·�����ļ����ϲ�
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            //��json��ʽ����д���ı��ļ���
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            //Debug.Log($"{saveFileName}���ݴ洢�ɹ����洢·��Ϊ��{path}");
#endif
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            //Debug.LogError($"�洢����ʧ�ܣ�{path}��\n{exception}");
#endif

        }
    }

    //ͨ��Json��ȡ����
    public static T LoadFromJson<T>(string saveFileName)
    {
        //�����ļ�·�������־������ļ�·�����ļ����ϲ�
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
            Debug.LogError($"��ȡ����ʧ�ܣ�{path}.\n{exception}\n�´���һ��Ϊ�յ��ļ�");
#endif
            //SaveByJson(saveFileName);
            return default;
        }
    }

    //ɾ���洢������
    public static void DeleteSaveFile(string saveFileName)
    {
        //�����ļ�·�������־������ļ�·�����ļ����ϲ�
        var path = Path.Combine(Application.persistentDataPath, saveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
#if UNITY_EDITOR
            Debug.LogError($"ɾ������ʧ�ܣ�{path}.\n{exception}");
#endif

        }
    }
}

