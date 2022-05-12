using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Designer : CharacterMate
{
    public int challengeLv;

    public List<bool> statusPerTurn;

    public bool isTeamWork;//���ƣ��Ŷ�Э��
    public bool isSycn;//���ƣ�����ͬ��
    public bool isPMH;//���ƣ�ƽ�滯�Ŷ�

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
        int difference = challengeLv - gM.enM.enemyTarget.skillLv;
        if (challengeLv > gM.enM.enemyTarget.skillLv && difference < 10)
        {
            gM.enM.enemyTarget.TakeDamage(difference);
        }
    }

    public void GoTeamWork(int times)
    {
        for (int i = 0; i < times; i++)
        {
            int index = Random.Range(0, 2);
            if (index == 0)
            {
                gM.aiM.proAI.EnergyValueChange(1);
            }
            else
            {
                gM.aiM.artAI.EnergyValueChange(1);
            }
        }
    }
}
