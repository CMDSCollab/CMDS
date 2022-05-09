using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//放置在Canvas的Map上，用于管理具体Node的生成，并将其分成16层layer包装记录
public class MapManager : MonoBehaviour
{
    public GameMaster gM;
    public MapConfig mapConfig;
    public GameObject mapNodePrefab;
    public GameObject linePrefab;
    public Dictionary<int, GameObject> singleLayerNodes = new Dictionary<int, GameObject>();
    public Dictionary<int, Dictionary<int, GameObject>> allLayerNodes = new Dictionary<int, Dictionary<int, GameObject>>();

    private int singleLayerNodesCount;
    public float nodePosXStartValue;
    public float nodeHoriMinApartDistance;
    public float nodePosYStartValue;
    public float nodeVerMinApartDistance;

    public List<EnemyInfo> levelEnemyPool;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        GenerateAllLayerNodes();
        LineConnectionSet();
        DrawLine();
    }

    #region MapGeneration
    public void GenerateAllLayerNodes() //生成所有Node
    {
        //float nodePosYRecord = nodePosYStartValue;
        for (int i = 0; i < mapConfig.mapLayers.Count; i++)
        {
            //创建一层Nodes
            GenerateSingleLayerNodes(mapConfig.mapLayers[i]);
            //调整该层Nodes在Y轴的坐标
            for (int j = 0; j < singleLayerNodes.Count; j++)
            {
                singleLayerNodes[j].transform.position = new Vector3(singleLayerNodes[j].transform.position.x, nodePosYStartValue + i * nodeVerMinApartDistance+ Random.Range(0f,0.5f), -2);
                if (i == mapConfig.mapLayers.Count - 1)
                {
                    singleLayerNodes[j].transform.position = new Vector3(0, nodePosYStartValue + i * nodeVerMinApartDistance + Random.Range(0f, 0.5f), -2);
                }
            }
            //用allLayerNodes(Dic)记录该层nodes信息，因为引用类型所以得重新包装，然后清除singleLayerNodes用于包装下层nodes信息
            Dictionary<int, GameObject> newDic = new Dictionary<int, GameObject>(singleLayerNodes);
            allLayerNodes.Add(i, newDic);
            singleLayerNodes.Clear();
        }
    }

    public void GenerateSingleLayerNodes(MapLayer targetMapLayer) //生成单层Node
    {
        //随机生成可能的Node数量，3-5个
        if (allLayerNodes.Count < mapConfig.mapLayers.Count -1) //Boss层只会有一个Node
        {
            singleLayerNodesCount = Random.Range(targetMapLayer.possibleEmergeNodesCount, targetMapLayer.possibleEmergeNodesCount + 2);
        }
        else
        {
            singleLayerNodesCount = 1;
        }
        float nodePosXRecord = nodePosXStartValue;
        for (int i = 0; i < singleLayerNodesCount; i++)
        {
            //判定生成的Node的Type是常规设定的该层Node，还是触发随机变量生成
            NodeType determinedNodeType;
            float judgeAmount = Random.Range(0f, 1f);
            if (judgeAmount <= targetMapLayer.randomizeNodesType)
            {
                int randomSequence = Random.Range(0, targetMapLayer.possibleNodeType.Length);
                determinedNodeType = targetMapLayer.possibleNodeType[randomSequence];
            }
            else
            {
                determinedNodeType = targetMapLayer.nodeType;
            }
            //生成Node
            GameObject mapNodeObj = Instantiate(mapNodePrefab);
            //从mapConfig获取和该Type同类型的TypeRef，从而获得对应mapNode的信息赋值给该Node上MapNodeManager里的mapNode进行记录
            for (int j = 0; j < mapConfig.mapNodes.Count; j++)
            {
                if (mapConfig.mapNodes[j].nodeType == determinedNodeType)
                {
                    mapNodeObj.GetComponent<MapNodeManager>().mapNode = mapConfig.mapNodes[j];
                }
            }
            //调用该mapNodeObj的组件中的mapNode中的图片信息赋值给Image的Sprite
            mapNodeObj.GetComponent<SpriteRenderer>().sprite = mapNodeObj.GetComponent<MapNodeManager>().mapNode.iconImage;
            //设定父物体
            mapNodeObj.transform.SetParent(transform.Find("NodeCollection"));
            //调整水平位置关系
            float maxNodeDistance = 2 * (-nodePosXStartValue) / singleLayerNodesCount;
            float randomXDistance = Random.Range(nodeHoriMinApartDistance, maxNodeDistance);
            nodePosXRecord += randomXDistance;
            mapNodeObj.transform.position = new Vector3(nodePosXRecord, 0, 0);
            //将循环出来的Node赋值给singleLayerNodes这个dic
            singleLayerNodes.Add(i, mapNodeObj);
        }
 
    }

    public void LineConnectionSet() //确定每个点之间的连接关系
    {
        //确定每个Node的目标连接点数量
        for (int i = 0; i < allLayerNodes.Count-1; i++)
        {
            int currentLayerNodesCount = allLayerNodes[i].Count;
            int nextLayerNodesCount = allLayerNodes[i + 1].Count;
            if (currentLayerNodesCount < nextLayerNodesCount)
            {
                int extraNodeToLink = nextLayerNodesCount - currentLayerNodesCount;
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetCount = 1;
                }
                do
                {
                    int whichNodeToAddExtra = Random.Range(0, currentLayerNodesCount);
                    allLayerNodes[i][whichNodeToAddExtra].GetComponent<MapNodeManager>().linkTargetCount += 1;
                    extraNodeToLink -= 1;
                } while (extraNodeToLink > 0);
            }
            else
            {
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetCount = 1;
                }
            }
        }
        //确定需要连接的点信息
        for (int i = 0; i < allLayerNodes.Count-1; i++)
        {
            int targetNodeIndex = 0;
            int remainder = allLayerNodes[i].Count - allLayerNodes[i + 1].Count;
            List<int> nodesToEmerge = new List<int>();
            bool isNodeToEmerge = false;
            if (remainder > 0)
            {
                for (int j = 0; j < remainder; j++)
                {
                    int random = Random.Range(1, allLayerNodes[i].Count);
                    while (nodesToEmerge.Contains(random) == true)
                    {
                        random = Random.Range(1, allLayerNodes[i].Count);
                    }
                    nodesToEmerge.Add(random);
                }
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                    if (nodesToEmerge.Contains(j) == true)
                    {
                        isNodeToEmerge = true;
                    }
                    else
                    {
                        isNodeToEmerge = false;
                    }
                    if (isNodeToEmerge == true)
                    {
                        mapTemp.linkTargetIndexList.Add(targetNodeIndex - 1);
                    }
                    else
                    {
                        mapTemp.linkTargetIndexList.Add(targetNodeIndex);
                        targetNodeIndex++;
                    }
                }
            }
            else
            {
                for (int j = 0; j < allLayerNodes[i].Count; j++)
                {
                    MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                    for (int n = 0; n < mapTemp.linkTargetCount; n++)
                    {
                            mapTemp.linkTargetIndexList.Add(targetNodeIndex);
                            targetNodeIndex++;
                    }
                }
            }
            if (remainder == 0)
            {
               
                    int random = Random.Range(0, allLayerNodes[i].Count);
                    MapNodeManager mapTemp = allLayerNodes[i][random].GetComponent<MapNodeManager>();
                    mapTemp.linkTargetCount++;
                    if (mapTemp.linkTargetIndexList[0] == 0)
                    {
                        mapTemp.linkTargetIndexList.Add(1);
                    }
                    else
                    {
                        mapTemp.linkTargetIndexList.Add(mapTemp.linkTargetIndexList[0]--);
                    }
                
            }
        }
    }

    public void DrawLine() //在地图生成时，生成Node连接线
    {
        for (int i = 0; i < allLayerNodes.Count - 1; i++)
        {
            for (int j = 0; j < allLayerNodes[i].Count; j++)
            {
                MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                for (int n = 0; n < mapTemp.linkTargetCount; n++)
                {
                    GameObject newObj = Instantiate(linePrefab, allLayerNodes[i][j].transform);
        
                    LineRenderer line = newObj.GetComponent<LineRenderer>();
                    line.SetPosition(0, newObj.transform.position);
                    line.SetPosition(1, allLayerNodes[i + 1][allLayerNodes[i][j].GetComponent<MapNodeManager>().linkTargetIndexList[n]].transform.position);
                    line.startWidth = 0.01f;
                    line.endWidth = 0.01f;
                }
            }
        }
    }

    public void LinePosReset(float changeAmount) //当滑动鼠标滚轮时，重新同步屏幕内Node连接线的位置信息
    {
        for (int i = 0; i < allLayerNodes.Count - 1; i++)
        {
            for (int j = 0; j < allLayerNodes[i].Count; j++)
            {
                //MapNodeManager mapTemp = allLayerNodes[i][j].GetComponent<MapNodeManager>();
                //List<LineRenderer> childLinePrefabList = new List<LineRenderer>();
                foreach (LineRenderer lineRenderer in allLayerNodes[i][j].GetComponentsInChildren<LineRenderer>())
                {
                    Vector3 startPos = lineRenderer.GetPosition(0);
                    Vector3 endPos = lineRenderer.GetPosition(1);
                    lineRenderer.SetPosition(0,new Vector3( startPos.x, startPos.y + changeAmount, startPos.z));
                    lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y + changeAmount, endPos.z));
                }
                
            }
        }
    }
    #endregion
}
