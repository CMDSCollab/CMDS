using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteraction : MonoBehaviour
{
    public Transform hitObj;

    void Update()
    {
        MapScroll();
    }

    public void MapScroll()
    {
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //鼠标滚动滑轮 值就会变化
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Translate(Vector3.up * 1f);
                transform.GetComponent<MapManager>().LinePosReset(1f);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Translate(Vector3.up * -1f);
                transform.GetComponent<MapManager>().LinePosReset(-1f);
            }
           
        }
    }
}
