using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢UIԪ�صĻ�����Ϣ��UIԪ�ػ�����λΪPanel
/// </summary>
public class UIType
{
    private string path;
    public string Path { get => path; }

    private string name;
    public string Name { get => name; }

    /// <summary>
    /// ��ʼ��UIpanel��Ϣ
    /// </summary>
    /// <param name="path">��ӦPanel·��</param>
    /// <param name="name">��ӦPanel����</param>
    public UIType(string path, string name)
    {
        this.path = path;
        this.name = name;   
    }
}
