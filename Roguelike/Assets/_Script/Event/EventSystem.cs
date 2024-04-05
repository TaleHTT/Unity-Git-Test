using System;
using UnityEngine;
public class EntityEventSystem : MonoBehaviour
{
    public static EntityEventSystem instance;
    public event Action OnTarget_SuccessPassPart;
    public event Action OnTarget_FailPassPart;
    public event Action OnTimer_SuccessPassPart;
    public event Action OnTimer_FailPassPart;
    public event Action OnTrigger_SuccessPassPart;
    public event Action OnTrigger_FailPassPart;
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void Traget_SuccessPassPart()
    {
        if(OnTarget_SuccessPassPart != null)
            OnTarget_SuccessPassPart();
    }
    public void Target_FailPassPart()
    {
        if(OnTarget_FailPassPart != null)
            OnTarget_FailPassPart();
    }
    public void Timer_SuccessPassPart()
    {
        if (OnTimer_SuccessPassPart != null)
            OnTimer_SuccessPassPart();
    }
    public void Timer_FailPassPart()
    {
        if (OnTimer_FailPassPart != null)
            OnTimer_FailPassPart();
    }
    public void Trigger_SuccessPassPart()
    {
        if (OnTrigger_SuccessPassPart != null)
            OnTrigger_SuccessPassPart();
    }
    public void Trigger_FailPassPart()
    {
        if (OnTrigger_FailPassPart != null)
            OnTrigger_FailPassPart();
    }
}
