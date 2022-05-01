using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于管理所有的Node和关卡信息，被MapManager引用
[CreateAssetMenu(fileName ="New MapConfig",menuName ="Scriptable Object/Map Config")]
public class MapConfig : ScriptableObject
{
    public List<MapNode> mapNodes;
    public List<MapLayer> mapLayers;
}
