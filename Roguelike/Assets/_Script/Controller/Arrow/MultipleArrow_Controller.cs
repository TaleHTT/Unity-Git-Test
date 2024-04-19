using UnityEngine;
using UnityEngine.Pool;

public class MultipleArrow_Controller : MonoBehaviour
{
    public Vector2 moveDir { get; set; }
    public float damage { get; set; }
    public ObjectPool<GameObject> multipleArrowPool {  get; set; }
}