using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffType
{
    Shield,
    Vengeance,
    PowerUp,
    Challenge
}

public class CharacterMate : BasicCharacter
{
    public int maxHp;
    public int healthPoint;
    public int shieldPoint;
    //public int gold;
    public bool isDefeated;
    public Slider hpBar;
    public Text hpRatio;

    public override void Start()
    {
        base.Start();
        InitializeCharacter();
    }

    public void CharacterDefeated()
    {
        gM.uiCanvas.transform.Find("StatisticPanel").gameObject.SetActive(true);
    }

    public void Update()
    {
        SyncCharacterUI();

        if (healthPoint <= 0 && !isDefeated)
        {
            isDefeated = true;
            CharacterDefeated();
    }
    }

    public virtual void SyncCharacterUI()
    {
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
/*        CommonStatusBar statusBar = FindObjectOfType<CommonStatusBar>();
        statusBar.goldIntText.text = gold.ToString();*/
    }

    private void InitializeCharacter()
    {
        maxHp = characterInfo.maxHp;
        //gold = characterInfo.initialGold;
        healthPoint = maxHp;
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = hpBar.transform.Find("HpRatio").GetComponent<Text>();
        hpBar.maxValue = maxHp;
    }

    public virtual void TakeDamage(int dmg)
    {
        //Debug.Log("tdDmg:" + dmg);
        //Debug.Log("tdDmgddadsas£º" + gM.buffM.CharacterTakeDamage(dmg));
        healthPoint -= gM.buffM.CharacterTakeDamage(dmg);
    }

    public virtual void HealSelf(int healAmount)
    {
        //Debug.Log("Hp:"+healthPoint);
        //Debug.Log("Ha:"+healAmount);
        healthPoint += healAmount;
        if (healthPoint >= maxHp)
        {
            healthPoint = maxHp;
        }
    }
}
