using UnityEngine;
using UnityEngine.Pool;

public class MultipleArrow_Controller : MonoBehaviour
{
    public float moveSpeed;
    public float damage {  get; set; }
    public Vector2 moveDir { get; set; }
    public ObjectPool<GameObject> multipleArrowPool {  get; set; }
    private void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("Enter");
            collision.GetComponent<EnemyStats>()?.TakeDamage(damage);
            multipleArrowPool.Release(gameObject);
        }
    }
}