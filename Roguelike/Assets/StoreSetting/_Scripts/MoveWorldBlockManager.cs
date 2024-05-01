using System.Collections;

using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// 放置在需要移动的物体上就可以实现拖动效果
/// </summary>
public class MoveWorldBlockManager : MonoBehaviour

{



    private IEnumerator OnMouseDown()

    {

        //将三维物体坐标转换成屏幕坐标

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //鼠标屏幕坐标

        Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

        //将鼠标屏幕坐标转换成三维坐标

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);

        //计算物体位置与鼠标之间的距离

        Vector3 offset = transform.position - mouseWorldPosition;

        //提前定义好返回值

        var cs = new WaitForFixedUpdate();

        //当按下鼠标左键时

        while (Input.GetMouseButton(0))

        {

            //更新鼠标屏幕坐标

            currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //将鼠标屏幕坐标转换成三维坐标

            mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace);

            //移动物体坐标

            Vector3 currentPosition = mouseWorldPosition + offset;

            //将物体坐标设置成移动后的坐标

            transform.position = currentPosition;

            //返回 (只有当下一次fixedUpdate开始时再执行后续代码)

            yield return cs;

        }



    }



}