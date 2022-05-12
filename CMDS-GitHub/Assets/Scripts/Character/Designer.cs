using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Designer : CharacterMate
{
    public int challengeLv;

    public List<bool> statusPerTurn;

    public bool isTeamWork;//卡牌：团队协作
    public bool isSycn;//卡牌：需求同步
    public bool isPMH;//卡牌：平面化团队

    public Text challengeIntText;

    public GameObject flowChartPrefab;
    private Canvas UICanvas;
    private GameObject flowChart;

    private void Awake()
    {
        
    }

    public override void Start()
    {
        base.Start();
        PrepareFlowChart();
    }
    //void Update()
    //{
    //    UpdateUI();
    //}

    void PrepareFlowChart()
    {
        UICanvas = gM.uiCanvas;
        flowChart = Instantiate(gM.characterM.flowChartPrefab, UICanvas.transform, false);
    }

    public void ChallengeDMG()
    {
        gM.enM.enemyTarget.TakeDamage(challengeLv - gM.enM.enemyTarget.skillLv);
    }

    void UpdateUI()
    {
        //hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        //challengeIntText.text = challengeInt.ToString();
    }
}
