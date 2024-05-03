using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
public class CenterPointMoveLogic : MonoBehaviour
{
    [Tooltip("中心点移动速度")]
    public float moveSpeed;
    private List<Vector3> pathPointList;
    private int currentIndex;
    private Vector3 CenterPointAutoPathTarget;
    private Vector3 target;
    private Seeker seeker;
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }
    private void Update()
    {
        MoveDir();
        CenterPointAutoPathTarget = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        if (transform.position != CenterPointAutoPathTarget)
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
        }
    }
    public void AutoPath()
    {
        CenterPointAutoPathTarget = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        GeneratePath(CenterPointAutoPathTarget);
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(CenterPointAutoPathTarget);
        }
        else if (Vector2.Distance(transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(CenterPointAutoPathTarget);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }

    public float stepInRotation;
    public virtual void MoveDir()
    {
        Vector2 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 arrowheadposition = new Vector2(transform.position.x, transform.position.y);
        float angle = WhatAngle(mouseposition, arrowheadposition);

        //transform.rotation = Quaternion.Euler(0, 0, angle);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, angle);

        Quaternion newRotation = Quaternion.Lerp(startRotation, endRotation, stepInRotation);
        transform.rotation = newRotation;
    }
    public float WhatAngle(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
