using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameMaster gM;
    public EnemyIntentionUI enInUI;
    public EnemyAI enAI;

    public int healthPoint;
    public int maxHp;
    public int shieldPoint;
    public int shieldAmount;
    public int healAmount;
    public int chargeLv;
    public int skillLv;

    public int singleDmg;
    public int aoeDmg;
    public int dmgPerCharge;
    public int currentDmg;

    public Text hpText;
    public Text shieldText;
    public Text chargeText;
    public Text skillText;

    //��ͼboolֵ
    public bool isSingleAttack;
    //public bool isAoeAttack;
    public bool isShielding;
    public bool isCharging;
    public bool isHealing;
    public bool isImproving;

    //�����ʨ��������boolֵ
    public bool isPlDesigner; // �����ѡ�����ʨ��Ϊְҵʱ��
    public bool isAnexity;//����
    public bool isBored;//����
    public bool isWeak;//����
    public bool isSoft;//����
    public bool isMagicCircle;//��MC��Χ


    //�����ʨ����UI

    public Image softImg;
    public Image magicCircleImg;
    public Image anexityImg;
    public Image boringImg;

    //�����ʨ�������Ƶ���ֵ

    public float anexityDmgWeight;//ԭ�˺����Ը�ϵ����ø����˺�
    public float weakweight;//����˺����Ը�ϵ����ɸ����˺�
    public float softweight;//ԭ�˺����Ը�ϵ����ø����˺�

    public List<string> intentionList;
    public string currentIntention;
    public GameObject currentIntentionUI;


    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        enInUI = GetComponent<EnemyIntentionUI>();
        GenerateInitialIntentionList();
        enAI.GenerateIntention();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        shieldText.text = shieldPoint.ToString();
        chargeText.text = chargeLv.ToString();
        skillText.text = skillLv.ToString();
        CheckStatus();
    }

    public void CheckStatus()
    {
        anexityImg.enabled = isAnexity;
        softImg.enabled = isSoft;
        magicCircleImg.enabled = isMagicCircle;
        boringImg.enabled = isBored;
    }

    public void GenerateInitialIntentionList()
    {
        intentionList = new List<string>();
        if(isSingleAttack)
        {
            intentionList.Add("SingleA");
        }
/*        if(isAoeAttack)
        {
            intentionList.Add("AOE");
        }*/
        if (isCharging)
        {
            intentionList.Add("Charging");
        }
        if (isShielding)
        {
            intentionList.Add("Shielding");
        }
        if (isHealing)
        {
            intentionList.Add("Healing");
        }
        if (isPlDesigner && isImproving)
        {
            intentionList.Add("Improving");
        }
    }

    public void ShowIntention()
    {
        if(currentIntention == "SingleA")
        {
            currentDmg = GetCurrentDmg();
            enInUI.singeAIntText.text = currentDmg.ToString();
            currentIntentionUI = enInUI.singleAUI;
            enInUI.singleAUI.SetActive(true);
        }
/*        else if (currentIntention == "AOE")
        {
            currentDmg = GetCurrentDmg();
            enInUI.aoeIntText.text = currentDmg.ToString();
            currentIntentionUI = enInUI.aoeUI;
            enInUI.aoeUI.SetActive(true);
        }*/
        else if (currentIntention == "Charging")
        {
            currentIntentionUI = enInUI.chargeUI;
            enInUI.chargeUI.SetActive(true);
        }
        else if (currentIntention == "Shielding")
        {
            enInUI.shieldIntText.text = shieldAmount.ToString();
            currentIntentionUI = enInUI.shieldUI;
            enInUI.shieldUI.SetActive(true);
        }
        else if (currentIntention == "Healing")
        {
            enInUI.healIntText.text = healAmount.ToString();
            currentIntentionUI = enInUI.healUI;
            enInUI.healUI.SetActive(true);
        }
        else if(currentIntention == "Improving")
        {
            currentIntentionUI = enInUI.skillUI;
            enInUI.skillUI.SetActive(true);
        }
    }

    public int DmgCalculationWithWeight(int originalDmg,float weight)
    {
        float currentDmg = originalDmg;
        currentDmg *= 1 + weight;
        return (int)currentDmg;
    }

    public int GetCurrentDmg()
    {
        int dmg = 0;

        dmg = singleDmg + chargeLv * dmgPerCharge;

        if (isSoft)
        {
            dmg = DmgCalculationWithWeight(dmg, -softweight);
        }
        else if (isAnexity)
        {
            dmg = DmgCalculationWithWeight(dmg, anexityDmgWeight);
        }

        return dmg;
    }

    public void SingleAttack()
    {
        gM.characterM.mainCharacter.TakeDamage(currentDmg);
    }

/*    public void AOEAttack()
    {
        gM.aiM.proAI.TakeDamage(currentDmg);
        gM.aiM.desAI.TakeDamage(currentDmg);
        gM.aiM.artAI.TakeDamage(currentDmg);
    }*/

    public void HealSelf(int healAmount)
    {
        if(healAmount + healthPoint >= maxHp)
        {
            healthPoint = maxHp;
        }
        else
        {
            healthPoint += healAmount;
        }
    }

    public void ShieldSelf(int shieldAmount)
    {
        shieldPoint += shieldAmount;
    }

    public void ChargeSelf()
    {
        chargeLv += 1;
    }

    public void ImproveSelf()
    {
        skillLv += 1;
    }

    public void TakeAction()
    {
        if (currentIntention == "SingleA")
        {
            SingleAttack();
        }
/*        else if (currentIntention == "AOE")
        {
            AOEAttack();
        }*/
        else if (currentIntention == "Charging")
        {
            ChargeSelf();
        }
        else if (currentIntention == "Shielding")
        {
            ShieldSelf(shieldAmount);
        }
        else if (currentIntention == "Healing")
        {
            HealSelf(healAmount);
        }
        else if(currentIntention == "Improving")
        {
            ImproveSelf();
        }

        currentIntentionUI.SetActive(false);

        enAI.GenerateIntention();
    }

    public void TakeDamage(int dmg)
    {
        if (isSoft)
        {
            float adjustDmg = dmg * (1 + weakweight);
            dmg = (int)adjustDmg;
        }
        if (shieldPoint > 0)
        {
            if (shieldPoint >= dmg)
            {
                shieldPoint -= dmg;
            }
            else
            {
                int overdmg = dmg - shieldPoint;
                shieldPoint = 0;

                healthPoint -= overdmg;
            }
        }
        else
        {
            healthPoint -= dmg;
        }
    }

    public void ChallengeVsSkill()
    {
        int chaInt = gM.characterM.designerPl.challengeInt;
        if (chaInt >= skillLv)
        {
            isBored = false;

            if(chaInt - skillLv > 10)
            {
                isAnexity = true;
            }
            else
            {
                isAnexity = false;
                MagicCircle(true);
                TakeChallengeDamage(chaInt);
            }
        }
        else
        {
            isBored = true;
        }
    }

    public void TakeChallengeDamage(int chaInt)
    {

    }

    public void MagicCircle(bool isMC)
    {
        isMagicCircle = isMC;
        isWeak = isMC;
        isSoft = isMC;
    }

    //�ж��Ƿ����MagicCircle������weightȨ�أ�һ��0��100������ֵ������������бȽϣ�weightԽ�󣬵����Ŀ�����Խ�ߡ�

    public void RollMagicCircleDice(int weight)
    {
        int dice = Random.Range(0, 100);

        if (dice < weight)
        {
            MagicCircle(false);
        }
        else
        {
            MagicCircle(true); ;
        }
    }

    public void MagicCircleDetection(int boredMCRate)
    {
        ChallengeVsSkill();
        if(skillLv < gM.characterM.designerPl.challengeInt)
        {
            MagicCircle(true);

            if(skillLv + 10 < gM.characterM.designerPl.challengeInt)
            {
                RollMagicCircleDice(30);
            }
        }
        else
        {
            RollMagicCircleDice(boredMCRate);
        }
    }
}
