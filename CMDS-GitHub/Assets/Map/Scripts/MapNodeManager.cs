using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������BaseMapNode���prefab�ϣ����������ǵ����Node����������Ч�����ͼ�¼��ǰNode����Ϣ
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
