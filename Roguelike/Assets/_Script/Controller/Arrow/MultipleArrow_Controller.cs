using UnityEngine;
using UnityEngine.Pool;

public class MultipleArrow_Controller : MonoBehaviour
{
    public float moveSpeed;
    public float damage {  get; set; }
    public Vector2 moveDir { get; set; }
    public ObjectPool<GameObject> multipleArrowPool {  get; set; }
    protected virtual void Update()
    {

    }

    public void Move()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}