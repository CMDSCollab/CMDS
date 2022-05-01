using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Designer : BasicCharacter
{
    public int challengeInt;

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
