using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//放置在BaseMapNode这个prefab上，后续功能是当这个Node点击后产生的效果，和记录当前Node的信息
public class MapNodeManager : MonoBehaviour
{
    public MapNode mapNode;
    public int linkTargetCount;
    public List<int> linkTargetIndexList;

    public void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnMouseDown()
    {
        Debug.Log(mapNode.nodeType);
    }
}
