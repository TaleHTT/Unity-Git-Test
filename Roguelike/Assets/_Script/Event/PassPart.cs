using UnityEngine;
public class PassPart : MonoBehaviour
{
    private void Start()
    {
        EntityEventSystem.instance.OnTarget_SuccessPassPart += Target_PartSuccessPass;
        EntityEventSystem.instance.OnTarget_FailPassPart += Target_PartFailPass;
        EntityEventSystem.instance.OnTimer_SuccessPassPart += Timer_PartSuccessPass;
        EntityEventSystem.instance.OnTimer_FailPassPart += Timer_PartFailPass;
        EntityEventSystem.instance.OnTrigger_SuccessPassPart += Trigger_PartSuccessPass;
        EntityEventSystem.instance.OnTrigger_FailPassPart += Trigger_PartFailPass;
    }
    private void OnDisable()
    {
        EntityEventSystem.instance.OnTarget_SuccessPassPart -= Target_PartSuccessPass;
        EntityEventSystem.instance.OnTarget_FailPassPart -= Target_PartFailPass;
        EntityEventSystem.instance.OnTimer_SuccessPassPart -= Timer_PartSuccessPass;
        EntityEventSystem.instance.OnTimer_FailPassPart -= Timer_PartFailPass;
        EntityEventSystem.instance.OnTrigger_SuccessPassPart -= Trigger_PartSuccessPass;
        EntityEventSystem.instance.OnTrigger_FailPassPart -= Trigger_PartFailPass;
    }
    public void Target_PartSuccessPass()
    {
        //����ת��
        Debug.Log("Success");
    }
    public void Target_PartFailPass()
    {
        //����ת��
        Debug.Log("Fail");
    }
    public void Timer_PartSuccessPass()
    {
        //����ת��
        Debug.Log("Success");
    }
    public void Timer_PartFailPass()
    {
        //����ת��
        Debug.Log("Fail");
    }
    public void Trigger_PartSuccessPass()
    {
        //����ת��
        Debug.Log("Success");
    }
    public void Trigger_PartFailPass()
    {
        //����ת��
        Debug.Log("Fail");
    }
}