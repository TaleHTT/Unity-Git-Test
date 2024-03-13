using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储UI元素的基本信息，UI元素基本单位为Panel
/// </summary>
public class UIType
{
    private string path;
    public string Path { get => path; }

    private string name;
    public string Name { get => name; }

    /// <summary>
    /// 初始化UIpanel信息
    /// </summary>
    /// <param name="path">对应Panel路径</param>
    /// <param name="name">对应Panel名称</param>
    public UIType(string path, string name)
    {
        this.path = path;
        this.name = name;   
    }
}
