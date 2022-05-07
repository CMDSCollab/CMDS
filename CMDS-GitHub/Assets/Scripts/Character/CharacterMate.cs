using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMate : BasicCharacter
{
    public int maxHp;
    public int healthPoint;
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
        healthPoint -= dmg;
    }
}
