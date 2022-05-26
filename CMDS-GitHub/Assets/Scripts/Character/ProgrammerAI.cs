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
}
