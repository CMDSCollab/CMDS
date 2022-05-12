using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPoint : MonoBehaviour
{
    public Vector2 initialPos;
    public int chaP;
    public int skillP;
    public GameMaster gM;
    public Designer des;
    public float xGap;
    public float yGap;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPos;
        gM = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
        des = gM.aiM.des;
        transform.localPosition = new Vector2(initialPos.x + xGap * des.challengeLv, initialPos.y + yGap * gM.enM.enemyTarget.skillLv);
    }
}
