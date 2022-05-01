using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//被MapConfig（scriptable obj）所引用，游戏一关卡共16层，用于规划每一层出现的Node类型，概率，位置关系等
[System.Serializable]
public class MapLayer
{
    [Tooltip("该层主要出现的Node类型")]
    public NodeType nodeType;

    [Tooltip("该层可能会出现的Node类型")]
    public NodeType[] possibleNodeType;

    [Tooltip("该层可能会出现的Node数量")]
    public int possibleEmergeNodesCount;

    [Tooltip("该层出现其他类型Node的概率")]
    [Range(0f, 1f)]
    public float randomizeNodesType;
}
