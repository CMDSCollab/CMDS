using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMate : BasicCharacter
{
    public int energyPoint;
    public int energySlotAmount;

    public List<Image> energyPointImageList;
    public bool isReadyAction = false;

    public EnergySlot energySlotUI;
    public GameObject energySlots;

    public string currentIntention;



    public void AddTheEnergySlot()
    {
        energySlotAmount += 1;
        EnergySlot energySlot = Instantiate(energySlotUI);
        energySlot.transform.SetParent(energySlots.transform);
        energySlot.transform.localScale = new Vector3(1, 1, 1);
        energySlot.transform.localPosition = new Vector3(-100 + (energySlotAmount - 1) * 80, 0, 0);
        energyPointImageList.Add(energySlot.pointImage);
    }

    public void FillTheEnergyUI()
    {
        for (int i = 1; i < energyPointImageList.Count + 1; i++)
        {
            if (i > energyPoint)
            {
                energyPointImageList[i - 1].color = Color.white;
            }
            else
            {
                energyPointImageList[i - 1].color = Color.blue;
            }
        }
    }

}
