using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtistAI : AIMate
{

    public int healInt;
    public int shieldingInt;
    public Text healText;
    public Text shieldingText;
    public GameObject healUI;
    public GameObject shieldingUI;

    private void Start()
    {
        GenerateIntention();
    }

    private void Update()
    {
        UpdateUI();
        if (energyPoint >= energyPointImageList.Count)
        {
            TakeAction();
        }
    }

    public void GenerateIntention()
    {
        int index = Random.Range(0, 2);
        if (index == 0)
        {
            currentIntention = "Heal";
        }
        else
        {
            currentIntention = "Shield";
        }
    }

    public void ChangeIntention()
    {
        if (currentIntention == "Heal")
        {
            currentIntention = "Shield";
        }
        else
        {
            currentIntention = "Heal";
        }
    }

    public void UpdateUI()
    {
        hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        healText.text = healInt.ToString();
        shieldingText.text = shieldingInt.ToString();
        shieldPText.text = shieldPoint.ToString();


        if (currentIntention == "Heal")
        {
            healUI.SetActive(true);
            shieldingUI.SetActive(false);
        }
        else
        {
            healUI.SetActive(false);
            shieldingUI.SetActive(true);
        }
    }

    public void TakeAction()
    {
        if (isReadyAction)
        {
            if (currentIntention == "Heal")
            {
                gM.aiM.artAI.healthPoint += gM.aiM.artAI.healInt;
                gM.aiM.proAI.healthPoint += gM.aiM.artAI.healInt;
                gM.aiM.desAI.healthPoint += gM.aiM.artAI.healInt;
                gM.aiM.artAI.healInt = 0;
            }
            else if (currentIntention == "Shield")
            {
                gM.aiM.artAI.shieldPoint += gM.aiM.artAI.shieldingInt;
                gM.aiM.proAI.shieldPoint += gM.aiM.artAI.shieldingInt;
                gM.aiM.desAI.shieldPoint += gM.aiM.artAI.shieldingInt;
            }
            energyPoint = 0;
            isReadyAction = false;

        }

        GenerateIntention();
    }
}
