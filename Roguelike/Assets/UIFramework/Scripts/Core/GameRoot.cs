using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public static GameRoot instance;

    public static GameRoot GetInstance()
    {
        if(instance == null)
        {
            Debug.Log("获取GameRoot静态单例时为空");
        }
        return instance;
    }

    private UIManager uiManager;

    public UIManager uiManager_root { get => uiManager; }

    private SceneControl sceneControl;

    public SceneControl sceneControl_root { get => sceneControl; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            uiManager = new UIManager();
            sceneControl = new SceneControl();
        }
        else
        {
            Debug.Log("删除多出的GameRoot");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //UIManager.GetInstance().CanvasObj = UIMethods.GetInstance().FindMainCanvas();

        #region 单独加入当前场景进入字典，因为当前场景为默认一开始就会显示的场景
        // 根据场景名称获取类型
        string sceneName = SceneManager.GetActiveScene().name;
        System.Type classType = System.Type.GetType(sceneName);

        if (classType == null)
        {
            Debug.LogWarning($"未找到当前场景对应的类");
        }
        else
        {
            // 使用 Activator.CreateInstance 创建类的实例
            object value = System.Activator.CreateInstance(classType);

            // 检查创建的实例是否可以转换为 SceneBase 类型
            SceneBase nowScene = value as SceneBase;

            if (nowScene != null)
            {
                // 成功将类型转换为 SceneBase，可以进行后续操作
                SceneControl.GetInstance().dict_scene.Add(nowScene.SceneName, nowScene);

                //只需要调用进入的就好，不需要LoadScene()
                nowScene.EnterScene();
            }
            else
            {
                Debug.LogWarning($"无法将类型 {classType} 转换为 SceneBase");
            }
        }
       
        #endregion
        
    }
}
