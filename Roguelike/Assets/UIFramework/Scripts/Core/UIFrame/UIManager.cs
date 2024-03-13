using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// UI管理器
/// </summary>
public class UIManager
{
    /// <summary>
    /// 单例结构
    /// </summary>
    private static UIManager instance;

    public Stack<BasePanel> stack_ui;
    
    /// <summary>
    /// 当前场景的画布
    /// </summary>
    public GameObject CanvasObj;

    /// <summary>
    /// Panel名称， 对应Panel GameObject
    /// </summary>
    public Dictionary<string, GameObject> dict_uiObject;

    public UIManager() 
    {
        instance = this;
        stack_ui = new Stack<BasePanel>();
        dict_uiObject = new Dictionary<string, GameObject>();
    }

    public static UIManager GetInstance()
    {
        if(instance == null)
        {
            Debug.Log("UIManager instance 为空");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    /// <summary>
    /// Panel入栈
    /// </summary>
    /// <param name="basePanel_push">要入栈的Panel</param>
    public void Push(BasePanel basePanel_push)
    {
        Debug.Log($"{basePanel_push.uiType.Name}入栈");

        //栈顶禁用
        if(stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisabled();
        }

        //加载本地Panel对应的GameObject物体
        GameObject BasePanel_pushObj = GetSingleObject(basePanel_push.uiType);
        Debug.Log(BasePanel_pushObj.name);
        dict_uiObject.Add(basePanel_push.uiType.Name, BasePanel_pushObj);

        //对应的实际物体
        basePanel_push.ActiveObj = BasePanel_pushObj;

        //实际入栈操作,防止多次误触
        if(stack_ui.Count == 0)
        {
            stack_ui.Push(basePanel_push);
        }
        else
        {
            if(basePanel_push.uiType.Name != stack_ui.Peek().uiType.Name)
            {
                stack_ui.Push(basePanel_push);
            }
        }
        basePanel_push.OnStart();
        //不考虑误触
        /*stack_ui.Push(basePanel_push);
        basePanel_push.OnStart();*/

    }

    /// <summary>
    /// 推出栈顶Panel
    /// </summary>
    public void Pop()
    {
        if(stack_ui.Count == 0)
        {
            Debug.Log("stack_ui已为空，出栈无效");
        }
        else
        {
            stack_ui.Peek().OnDisabled();
            stack_ui.Peek().OnDestory();
            GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
            Debug.Log($"摧毁{stack_ui.Peek().uiType.Name}");
            dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
            stack_ui.Pop();
            if(stack_ui.Count > 0)
            {
                stack_ui.Peek().OnEable();
            }
        }
    }

    /// <summary>
    /// 清空栈
    /// </summary>
    public void PopAll()
    {
        while(stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisabled();
            stack_ui.Peek().OnDestory();
            GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
            Debug.Log($"摧毁{stack_ui.Peek().uiType.Name}");
            dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
            stack_ui.Pop();
        }
    }

    public GameObject GetSingleObject(UIType uitype)
    {
        //字典里有
        if (dict_uiObject.ContainsKey(uitype.Name))
        {
            return dict_uiObject[uitype.Name];
        }

        if(CanvasObj == null)
        {
            CanvasObj = UIMethods.GetInstance().FindMainCanvas();
            if (CanvasObj == null) 
            {
                GameObject mainCanvas = new GameObject("MainCanvas");
                Canvas canvas = mainCanvas.AddComponent<Canvas>();
                mainCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                mainCanvas.AddComponent<CanvasScaler>();
                mainCanvas.AddComponent<GraphicRaycaster>();
                CanvasObj = mainCanvas;
                Debug.Log("UIManager未能成功获得Canvas，已重新创建MainCanvas");
            }
        }


        GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uitype.Path));
        go.transform.parent = CanvasObj.transform;
        return go;
    }

}
