using UnityEngine;

public class AnimationTrgger : MonoBehaviour
{
    private void Update()
    {
        transform.position = GetComponentInParent<Transform>().transform.position;
    }
    private void DestoryEffect()
    {
        Destroy(this.gameObject);
    }
}
