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
            Debug.Log("��ȡGameRoot��̬����ʱΪ��");
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
            Debug.Log("ɾ�������GameRoot");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //UIManager.GetInstance().CanvasObj = UIMethods.GetInstance().FindMainCanvas();

        #region �������뵱ǰ���������ֵ䣬��Ϊ��ǰ����ΪĬ��һ��ʼ�ͻ���ʾ�ĳ���
        // ���ݳ������ƻ�ȡ����
        string sceneName = SceneManager.GetActiveScene().name;
        System.Type classType = System.Type.GetType(sceneName);

        if (classType == null)
        {
            Debug.LogWarning($"δ�ҵ���ǰ������Ӧ����");
        }
        else
        {
            // ʹ�� Activator.CreateInstance �������ʵ��
            object value = System.Activator.CreateInstance(classType);

            // ��鴴����ʵ���Ƿ����ת��Ϊ SceneBase ����
            SceneBase nowScene = value as SceneBase;

            if (nowScene != null)
            {
                // �ɹ�������ת��Ϊ SceneBase�����Խ��к�������
                SceneControl.GetInstance().dict_scene.Add(nowScene.SceneName, nowScene);

                //ֻ��Ҫ���ý���ľͺã�����ҪLoadScene()
                nowScene.EnterScene();
            }
            else
            {
                Debug.LogWarning($"�޷������� {classType} ת��Ϊ SceneBase");
            }
        }
       
        #endregion
        
    }
}
