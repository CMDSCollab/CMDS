using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignerAI : AIMate
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = healthPoint.ToString() + "/" + maxHp.ToString();
        shieldPText.text = shieldPoint.ToString();
    }

    public void ChangeIntention()
    {
        if (currentIntention == "a")
        {
            currentIntention = "b";
        }
        else
        {
            currentIntention = "a";
        }
    }
}
