using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffType
{
    Shield,
    Vengeance,
    PowerUp
}

public class CharacterMate : BasicCharacter
{
    public int maxHp;
    public int healthPoint;
    private Slider hpBar;
    private Text hpRatio;
    public Dictionary<BuffType, GameObject> buffs = new Dictionary<BuffType, GameObject>();

    public override void Start()
    {
        base.Start();
        InitializeCharacter();

        //for (int i = 0; i < 3; i++)
        //{
        //    SetBuff(gM.characterM.buffRecord[i].buffType, 0);
        //}
    }

    public void Update()
    {
        SyncCharacterUI();
    }

    public void SetBuff(BuffType buffType,int buffValue)
    {
        if (buffs.ContainsKey(buffType))
        {
            GameObject obj = buffs[buffType];
            obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
            buffs.Remove(buffType);
            buffs.Add(buffType, obj);
        }
        else
        {
            GameObject obj;
            GameObject buffObj = Instantiate(gM.characterM.buffPrefab, transform.Find("BuffArea"), false);
            foreach (BuffRecord recordBuff in gM.characterM.buffRecord)
            {
                if (recordBuff.buffType == buffType)
                {
                    buffObj.GetComponent<Image>().sprite = recordBuff.buffImage;
                }
            }
            obj = buffObj;
            obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
            buffs.Add(buffType, obj);
        }

        if (buffValue == 0)
        {
            Destroy(buffs[buffType]);
            buffs.Remove(buffType);
        }
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
