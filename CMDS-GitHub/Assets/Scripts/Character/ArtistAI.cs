using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtistAI : AIMate
{
    public int baseHeal;
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
            energyPoint -= energyPointImageList.Count;
            FillTheEnergyUI();
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
        healText.text = CalculateHeal().ToString();
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
        gM = FindObjectOfType<GameMaster>();
        if (currentIntention == "Heal")
        {
            Heal(CalculateHeal());    
        }
        else if (currentIntention == "Shield")
        {
            gM.characterM.mainCharacter.shieldPoint += gM.aiM.artAI.shieldingInt;
        }
        isReadyAction = false;

        

        GenerateIntention();
    }

    public void Heal(int healValue)
    {
        BasicCharacter baseM = gM.characterM.mainCharacter; 
        if(baseM.healthPoint + healValue >= baseM.maxHp)
        {
            baseM.healthPoint = baseM.maxHp;
        }
        else
        {
            baseM.healthPoint += healValue;
        }
    }


    public int CalculateHeal()
    {
        int heal = baseHeal + (energyPointImageList.Count - 3);
        return heal;
    }
}
