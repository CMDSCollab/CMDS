using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMate : BasicCharacter
{
    public int energyPoint = 0;
    public int energySlotAmount = 3;
    [HideInInspector]
    public List<Image> energyPointImageList = new List<Image>();
    public List<IntentionManager> intentions = new List<IntentionManager>();
    [HideInInspector]
    public Intentions currentIntention;
    private GameObject energy;
    [HideInInspector]
    public int intentionValue = 5;

    public override void Start()
    {
        base.Start();
        intentions = characterInfo.intentions;
        GenerateIntention();
        for (int i = 0; i < energySlotAmount; i++)
        {
            AddTheEnergySlot();
        }
    }

    public void Update()
    {
        
    }

    public void AddTheEnergySlot()
    {
        energy = Instantiate(gM.characterM.energyPrefab);
        energy.transform.SetParent(transform.Find("EnergyPos"));

        if (this is DesignerAI)
        {
            energy.GetComponent<Image>().sprite = gM.characterM.energyImages[0];
        }
        if (this is ProgrammerAI)
        {
            energy.GetComponent<Image>().sprite = gM.characterM.energyImages[1];
        }
        if (this is ArtistAI)
        {
            energy.GetComponent<Image>().sprite = gM.characterM.energyImages[2];
        }
        energy.transform.localScale = new Vector3(1, 1, 1);
        energyPointImageList.Add(energy.GetComponent<Image>());
        //÷ÿ÷√EnergySlotµƒŒª÷√
        int singleUnitWidth = (int)energy.GetComponent<RectTransform>().rect.width;
        float unitStartGenPos = -(singleUnitWidth * energyPointImageList.Count / 2) + singleUnitWidth / 2;
        for (int i = 0; i < energyPointImageList.Count; i++)
        {
            float unitXPos = unitStartGenPos + singleUnitWidth * i;
            energyPointImageList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(unitXPos, 0, 0);
        }
    }

    public void EnergyValueChange(int changeAmount)
    {
        energyPoint += changeAmount;

        for (int i = 0; i < energyPointImageList.Count; i++)
        {
            if (i < energyPoint)
            {
                energyPointImageList[i].color = Color.blue;
            }
            else
            {
                energyPointImageList[i].color = Color.white;
            }
        }
        IntentionValueChangeAndUISync();
    }

    #region Intention
    public void GenerateIntention()
    {
        int random = Random.Range(0, intentions.Count);
        IntentionManager intentionM = intentions[random];
        SyncIntention(intentionM);
        IntentionValueChangeAndUISync();
    }

    public void ChangeIntention()
    {
        int random = Random.Range(0, intentions.Count);
        IntentionManager intentionM = intentions[random];
        while (intentionM.intention == currentIntention)
        {
            random = Random.Range(0, intentions.Count);
            intentionM = intentions[random];
        }
        SyncIntention(intentionM);
    }

    public void SyncIntention(IntentionManager intentionM)
    {
        switch (intentionM.intention)
        {
            case Intentions.None:
                break;
            case Intentions.Attack:
                currentIntention = Intentions.Attack;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Attack";
                break;
            case Intentions.Heal:
                currentIntention = Intentions.Heal;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Heal";
                break;
            case Intentions.Shield:
                currentIntention = Intentions.Shield;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Shield";
                break;
            case Intentions.Buff:
                currentIntention = Intentions.Buff;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Buff";
                break;
            case Intentions.Debuff:
                currentIntention = Intentions.Debuff;
                transform.Find("IntentionPos").Find("Image").GetComponent<Image>().sprite = intentionM.image;
                transform.Find("IntentionPos").Find("Name").GetComponent<Text>().text = "Debuff";
                break;
        }
    }

    public void IntentionValueChangeAndUISync()
    {
        switch (energyPoint)
        {
            case 0:
                intentionValue = 5;
                break;
            case 1:
                intentionValue = 6;
                break;
            case 2:
                intentionValue = 7;
                break;
            case 3:
                intentionValue = 8;
                break;
            case 4:
                intentionValue = 9;
                break;
            case 5:
                intentionValue = 10;
                break;
            case 6:
                intentionValue = 15;
                break;
        }
        transform.Find("IntentionPos").Find("Value").GetComponent<Text>().text = intentionValue.ToString();
    }
    #endregion
    public virtual void TakeAction()
    {
        switch (currentIntention)
        {
            case Intentions.Attack:
                gM.enM.enemyTarget.TakeDamage(intentionValue);
                break;
            case Intentions.Heal:
                gM.characterM.mainCharacter.healthPoint += intentionValue;
                break;
            case Intentions.Shield:
                gM.buffM.SetCharacterBuff(CharacterBuff.Shield, true, intentionValue);
                break;
            case Intentions.Buff:
                break;
            case Intentions.Debuff:
                break;
        }
        EnergyValueChange(-energyPoint);
        GenerateIntention();
    }
}
