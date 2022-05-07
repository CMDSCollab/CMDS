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

    //void Update()
    //{
    //    UpdateUI();
    //}

    void UpdateUI()
    {
        //hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        //challengeIntText.text = challengeInt.ToString();

    }
}
