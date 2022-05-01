using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammerAI : AIMate
{
    public Text chargeText;

    public int dmgInt = 0;
    public int baseDmg = 10;
    public int chargeLv;
    public int dmgPerCharge;
    public Text dmgText;

    public GameObject AttackUI;
    public GameObject chargeUI;


    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        GenerateIntention();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        //能量满后，立即触发。
        if (energyPoint >= energyPointImageList.Count)
        {
            TakeAction();
        }
    }

    //单体攻击伤害公式
    public int CalculateDMG()
    {
        int dmg = baseDmg + (energyPoint - baseDmg) + chargeLv * dmgPerCharge;
        return dmg;
    }

    public void GenerateIntention()
    {
        int index = Random.Range(0, 2);
        if (index == 0)
        {
            currentIntention = "Attack";
        }
        else
        {
            currentIntention = "Charge";
        }
    }


    public void ChangeIntention()
    {
        if (currentIntention == "Attack")
        {
            currentIntention = "Charge";
        }
        else
        {
            currentIntention = "Attack";
        }
    }

    public void TakeAction()
    {
        if (currentIntention == "Attack")
        {
            gM.enM.currentTarget.TakeDamage(CalculateDMG());
        }
        else if (currentIntention == "Charge")
        {
            chargeLv += 1;
        }
        energyPoint = 0;
        isReadyAction = false;

        GenerateIntention();
    }

    public void UpdateUI()
    {
        hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        dmgText.text = dmgInt.ToString();
        shieldPText.text = shieldPoint.ToString();
        chargeText.text = chargeLv.ToString();

        if (currentIntention == "Attack")
        {
            AttackUI.SetActive(true);
            chargeUI.SetActive(false);
        }
        else
        {
            chargeUI.SetActive(true);
            AttackUI.SetActive(false);
        }
    }
}
