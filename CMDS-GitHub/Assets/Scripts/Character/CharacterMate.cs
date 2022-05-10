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
    private Slider hpBar;
    private Text hpRatio;

    public override void Start()
    {
        base.Start();
        InitializeCharacter();
    }

    public void Update()
    {
        SyncCharacterUI();
    }

    public virtual void SyncCharacterUI()
    {
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
    }

    private void InitializeCharacter()
    {
        maxHp = characterInfo.maxHp;
        healthPoint = maxHp;
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = hpBar.transform.Find("HpRatio").GetComponent<Text>();
        hpBar.maxValue = maxHp;
    }

    public virtual void TakeDamage(int dmg)
    {
        if (shieldPoint > 0)
        {
            shieldPoint -= dmg;
            if (shieldPoint > 0)
            {
                gM.buffM.SetCharacterBuff(CharacterBuff.Shield, true, shieldPoint);
            }
            if (shieldPoint < 0)
            {
                healthPoint += shieldPoint;
                gM.buffM.SetCharacterBuff(CharacterBuff.Shield, true, 0);
            }
            if (shieldPoint == 0)
            {
                gM.buffM.SetCharacterBuff(CharacterBuff.Shield, true, 0);
            }
        }
        else
        {
            healthPoint -= dmg;
        }
    }

    public virtual void HealSelf(int healAmount)
    {
        healthPoint += healAmount;
        if (healthPoint >= maxHp)
        {
            healthPoint = maxHp;
        }
    }
}
