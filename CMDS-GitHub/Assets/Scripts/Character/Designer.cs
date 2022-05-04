using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Designer : BasicCharacter
{
    public int challengeInt;

    public List<bool> statusPerTurn;

    public bool isTeamWork;//卡牌：团队协作
    public bool isSycn;//卡牌：需求同步
    public bool isPMH;//卡牌：平面化团队

    public Text challengeIntText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        shieldPText.text = shieldPoint.ToString();
        challengeIntText.text = challengeInt.ToString();

    }
}
