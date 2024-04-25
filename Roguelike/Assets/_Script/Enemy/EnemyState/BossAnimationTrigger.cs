using UnityEngine;

public class BossAnimationTrigger : MonoBehaviour
{
    BossBase boss => GetComponentInParent<BossBase>();
    private void AnimationFinishTrigger()
    {
        boss.AnimationFinishTrigger();
    }
}