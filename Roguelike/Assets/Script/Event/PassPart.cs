using UnityEngine;
public class PassPart : MonoBehaviour
{
    private void Start()
    {
        EventSystem.instance.OnTarget_SuccessPassPart += Target_PartSuccessPass;
        EventSystem.instance.OnTarget_FailPassPart += Target_PartFailPass;
        EventSystem.instance.OnTimer_SuccessPassPart += Timer_PartSuccessPass;
        EventSystem.instance.OnTimer_FailPassPart += Timer_PartFailPass;
        EventSystem.instance.OnTrigger_SuccessPassPart += Trigger_PartSuccessPass;
        EventSystem.instance.OnTrigger_FailPassPart += Trigger_PartFailPass;
    }
    private void OnDisable()
    {
        EventSystem.instance.OnTarget_SuccessPassPart -= Target_PartSuccessPass;
        EventSystem.instance.OnTarget_FailPassPart -= Target_PartFailPass;
        EventSystem.instance.OnTimer_SuccessPassPart -= Timer_PartSuccessPass;
        EventSystem.instance.OnTimer_FailPassPart -= Timer_PartFailPass;
        EventSystem.instance.OnTrigger_SuccessPassPart -= Trigger_PartSuccessPass;
        EventSystem.instance.OnTrigger_FailPassPart -= Trigger_PartFailPass;
    }
    public void Target_PartSuccessPass()
    {
        //场景转换
        Debug.Log("Success");
    }
    public void Target_PartFailPass()
    {
        //场景转换
        Debug.Log("Fail");
    }
    public void Timer_PartSuccessPass()
    {
        //场景转换
        Debug.Log("Success");
    }
    public void Timer_PartFailPass()
    {
        //场景转换
        Debug.Log("Fail");
    }
    public void Trigger_PartSuccessPass()
    {
        //场景转换
        Debug.Log("Success");
    }
    public void Trigger_PartFailPass()
    {
        //场景转换
        Debug.Log("Fail");
    }
}