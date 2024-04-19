using UnityEngine;

public class CenterMoveManager : MonoBehaviour
{
    public static CenterMoveManager instance;
    public CenterPointMoveLogic centerPointMoveLogic {  get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        centerPointMoveLogic = GetComponent<CenterPointMoveLogic>();
    }
}
