using UnityEngine;
using UnityEngine.Pool;

public class MultipleArrow_Controller : MonoBehaviour
{
    [HideInInspector] public bool isFaceLeft = true;
    public float moveSpeed;
    public float damage {  get; set; }
    public Vector2 moveDir { get; set; }
    public ObjectPool<GameObject> multipleArrowPool {  get; set; }
    protected virtual void Update()
    {
        if (transform.position.x < 0 && !isFaceLeft)
        {
            Filp();
        }
        else if (transform.position.x > 0 && isFaceLeft)
        {
            Filp();
        }
    }

    public void Move()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
    public void Filp()
    {
        isFaceLeft = !isFaceLeft;
        transform.Rotate(0, 180, 0);
    }
}