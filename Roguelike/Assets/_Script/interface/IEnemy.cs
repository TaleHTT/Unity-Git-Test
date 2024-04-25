using UnityEngine;

public interface IEnemy
{
    public void Update();

    public void Enter();

    public void Exit();

    public void AnimationFinishTrigger();
}