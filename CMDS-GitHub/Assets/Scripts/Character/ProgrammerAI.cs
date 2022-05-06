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

    //void Update()
    //{
    //    //UpdateUI();
    //    ////能量满后，立即触发。
    //    //if (energyPoint >= energyPointImageList.Count)
    //    //{
    //    //    TakeAction();
    //    //}
    //}

    //单体攻击伤害公式
    public int CalculateDMG()
    {
        int dmg = baseDmg + (energyPoint - baseDmg) + chargeLv * dmgPerCharge;
        return dmg;
    }

    //public void TakeAction()
    //{
    //    if (currentIntention == Intentions.Attack)
    //    {
    //        gM.enM.currentTarget.TakeDamage(CalculateDMG());
    //    }
    //    //else if (currentIntention == "Charge")
    //    //{
    //    //    chargeLv += 1;
    //    //}
    //    energyPoint = 0;

    //    GenerateIntention();
    //}

    //public void UpdateUI()
    //{
    //    //hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
    //    dmgText.text = dmgInt.ToString();
    //    chargeText.text = chargeLv.ToString();

    //    if (currentIntention == Intentions.Attack)
    //    {
    //        AttackUI.SetActive(true);
    //        chargeUI.SetActive(false);
    //    }
    //    else
    //    {
    //        chargeUI.SetActive(true);
    //        AttackUI.SetActive(false);
    //    }
    //}
}
