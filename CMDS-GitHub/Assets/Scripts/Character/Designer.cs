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
