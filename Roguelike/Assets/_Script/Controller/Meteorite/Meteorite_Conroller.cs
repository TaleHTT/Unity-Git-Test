using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Meteorite_Conroller : MonoBehaviour
{
    public float moveSpeed;
    public ObjectPool<GameObject> meteoritePool;
    public Orb_Controller orb_Controller;
    private void Awake()
    {
        moveSpeed = orb_Controller.moveSpeed * 0.8f;
    }
    private void Update()
    {
        StartCoroutine(DestoryGameObject());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

        }
    }
    public IEnumerator DestoryGameObject()
    {
        yield return new WaitForSeconds(5);

        meteoritePool.Release(gameObject);
    }
}