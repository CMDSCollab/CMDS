using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//最基本的单个Node信息记录，包括sprite和type，被MapConfig引用
public enum NodeType
{
    None,
    Minion,
    Elite,
    Boss,
    Chest,
    Rest,
    Merchant,
    Uncertainty
}

[CreateAssetMenu(fileName = "Map Node",menuName ="Scriptable Object/Map Node")]
public class MapNode : ScriptableObject
{
    public Sprite iconImage;
    public NodeType nodeType;
}
