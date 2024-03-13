using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// UI������
/// </summary>
public class UIManager
{
    /// <summary>
    /// �����ṹ
    /// </summary>
    private static UIManager instance;

    public Stack<BasePanel> stack_ui;
    
    /// <summary>
    /// ��ǰ�����Ļ���
    /// </summary>
    public GameObject CanvasObj;

    /// <summary>
    /// Panel���ƣ� ��ӦPanel GameObject
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
            Debug.Log("UIManager instance Ϊ��");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    /// <summary>
    /// Panel��ջ
    /// </summary>
    /// <param name="basePanel_push">Ҫ��ջ��Panel</param>
    public void Push(BasePanel basePanel_push)
    {
        Debug.Log($"{basePanel_push.uiType.Name}��ջ");

        //ջ������
        if(stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisabled();
        }

        //���ر���Panel��Ӧ��GameObject����
        GameObject BasePanel_pushObj = GetSingleObject(basePanel_push.uiType);
        Debug.Log(BasePanel_pushObj.name);
        dict_uiObject.Add(basePanel_push.uiType.Name, BasePanel_pushObj);

        //��Ӧ��ʵ������
        basePanel_push.ActiveObj = BasePanel_pushObj;

        //ʵ����ջ����,��ֹ�����
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
        //��������
        /*stack_ui.Push(basePanel_push);
        basePanel_push.OnStart();*/

    }

    /// <summary>
    /// �Ƴ�ջ��Panel
    /// </summary>
    public void Pop()
    {
        if(stack_ui.Count == 0)
        {
            Debug.Log("stack_ui��Ϊ�գ���ջ��Ч");
        }
        else
        {
            stack_ui.Peek().OnDisabled();
            stack_ui.Peek().OnDestory();
            GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
            Debug.Log($"�ݻ�{stack_ui.Peek().uiType.Name}");
            dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
            stack_ui.Pop();
            if(stack_ui.Count > 0)
            {
                stack_ui.Peek().OnEable();
            }
        }
    }

    /// <summary>
    /// ���ջ
    /// </summary>
    public void PopAll()
    {
        while(stack_ui.Count > 0)
        {
            stack_ui.Peek().OnDisabled();
            stack_ui.Peek().OnDestory();
            GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
            Debug.Log($"�ݻ�{stack_ui.Peek().uiType.Name}");
            dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
            stack_ui.Pop();
        }
    }

    public GameObject GetSingleObject(UIType uitype)
    {
        //�ֵ�����
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
                Debug.Log("UIManagerδ�ܳɹ����Canvas�������´���MainCanvas");
            }
        }


        GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uitype.Path));
        go.transform.parent = CanvasObj.transform;
        return go;
    }

}
