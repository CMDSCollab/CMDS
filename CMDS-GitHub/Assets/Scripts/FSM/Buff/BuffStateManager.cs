using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffUsage
{
    AddNew,
    Adjust,
    EffectApply
}

public class BuffStateManager : MonoBehaviour
{
    public GameMaster gM;
    public BuffBaseState currentState;
    public RectTransform buffTrans;
    public List<EnemyBuff> enemyBuffs = new List<EnemyBuff>();
    public List<CharacterBuff> characterBuffs = new List<CharacterBuff>();
    public bool isUpdate = false;
    public int valueToCalculate = 0;
    public bool isSequenceEnd = false;
    public BuffUsage buffUsage = BuffUsage.EffectApply;

    public BS_Weak weakState = new BS_Weak();
    public BS_Vulnerable vulnerableState = new BS_Vulnerable();
    public BS_Bored boredState = new BS_Bored();
    public BS_Defence defenceState = new BS_Defence();

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void BuffEffectsApply()
    {
        buffUsage = BuffUsage.EffectApply;
        if (enemyBuffs.Count != 0)
        {
            if (gM.buffM.FindBuff(enemyBuffs[0]) != null)
            {
                buffTrans = GetBuffRectTrans(enemyBuffs[0]);
                currentState = GetBuffState(enemyBuffs[0]);
                //SetBuffStateFindTrans(enemyBuffs[0]);
                currentState.EnterState(gM);
                enemyBuffs.RemoveAt(0);
            }
            else
            {
                enemyBuffs.RemoveAt(0);
                BuffEffectsApply();
            }
        }
        else if (characterBuffs.Count != 0)
        {
            if (gM.buffM.FindBuff(characterBuffs[0]) != null)
            {
                buffTrans = GetBuffRectTrans(characterBuffs[0]);
                currentState = GetBuffState(characterBuffs[0]);
                //SetBuffStateFindTrans(characterBuffs[0]);
                currentState.EnterState(gM);
                characterBuffs.RemoveAt(0);
            }
            else
            {
                characterBuffs.RemoveAt(0);
                BuffEffectsApply();
            }
        }
        else
        {
            characterBuffs.Clear();
            enemyBuffs.Clear();
            gM.actionSM.currentState.BeforeUpdate(gM, valueToCalculate);
        }
    }

    public void AddOrAdjustBuff(EnemyBuff enemyBuff)
    {
        if (gM.buffM.FindBuff(enemyBuff) != null)
        {
            buffUsage = BuffUsage.Adjust;
            buffTrans = GetBuffRectTrans(enemyBuff);
            currentState = GetBuffState(enemyBuff);
        }
        else
        {
            buffUsage = BuffUsage.AddNew;
            currentState = GetBuffState(enemyBuff);
        }
        currentState.EnterState(gM);
    }

    public void AddOrAdjustBuff(CharacterBuff characterBuff)
    {
        if (gM.buffM.FindBuff(characterBuff) != null)
        {
            buffUsage = BuffUsage.Adjust;
            buffTrans = GetBuffRectTrans(characterBuff);
            currentState = GetBuffState(characterBuff);
        }
        else
        {
            buffUsage = BuffUsage.AddNew;
            currentState = GetBuffState(characterBuff);
        }
        currentState.EnterState(gM);
    }

    public void AddNewBuff(CharacterBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value, BuffSource buffSource)
    {
        BuffInfo buffInfo = new BuffInfo();
        buffInfo.characterBuffType = buffType;
        buffInfo.timeType = timeType;
        buffInfo.lastTime = lastTime;
        buffInfo.valueType = valueType;
        buffInfo.value = value;
        buffInfo.source = buffSource;
        buffInfo.uiObj = Instantiate(gM.buffM.buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false);
        //buffInfo.isReadyToRemove = gM.buffM.CheckBuffIsReadyForRemove(buffInfo);
        foreach (CharacterBuffImage buff in gM.buffM.characterBuffs)
        {
            if (buff.buffType == buffType)
            {
                buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
            }
        }
        gM.buffM.SyncBuffUI(buffInfo);
        gM.buffM.activeCBuffs.Add(buffInfo);
        //gM.buffM.CheckBuffAndRemove(buffInfo.characterBuffType);
    }

    public void AddNewBuff(EnemyBuff buffType, BuffTimeType timeType, int lastTime, BuffValueType valueType, int value, BuffSource buffSource)
    {
        BuffInfo buffInfo = new BuffInfo();
        buffInfo.enemyBuffType = buffType;
        buffInfo.timeType = timeType;
        buffInfo.lastTime = lastTime;
        buffInfo.valueType = valueType;
        buffInfo.value = value;
        buffInfo.source = buffSource;
        buffInfo.uiObj = Instantiate(gM.buffM.buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false);
        //buffInfo.isReadyToRemove = gM.buffM.CheckBuffIsReadyForRemove(buffInfo);
        foreach (EnemyBuffImage buff in gM.buffM.enemyBuffs)
        {
            if (buff.buffType == buffType)
            {
                buffInfo.uiObj.GetComponent<Image>().sprite = buff.buffImage;
            }
        }
        gM.buffM.SyncBuffUI(buffInfo);
        gM.buffM.activeEBuffs.Add(buffInfo);
        //gM.buffM.CheckBuffAndRemove(buffInfo.characterBuffType);
    }

    #region Set Array To List && Get BuffState && Get RectTransform
    public RectTransform GetBuffRectTrans(CharacterBuff characterBuff)
    {
        switch (characterBuff)
        {
            case CharacterBuff.Defence:
                return gM.buffM.FindBuff(CharacterBuff.Defence).uiObj.GetComponent<RectTransform>();
            //case CharacterBuff.Vengeance:
            //    break;
            //case CharacterBuff.PowerUp:
            //    break;
            case CharacterBuff.Weak:
                return gM.buffM.FindBuff(CharacterBuff.Weak).uiObj.GetComponent<RectTransform>();
            //case CharacterBuff.Inflammable:
            //    break;
            //case CharacterBuff.IsTeamWork:
            //    break;
            //case CharacterBuff.IsSycn:
            //    break;
            default:
                return gM.buffM.FindBuff(CharacterBuff.Defence).uiObj.GetComponent<RectTransform>();
        }
    }

    public RectTransform GetBuffRectTrans(EnemyBuff enemyBuff)
    {
        switch (enemyBuff)
        {
            case EnemyBuff.Bored:
                return gM.buffM.FindBuff(EnemyBuff.Bored).uiObj.GetComponent<RectTransform>();
            //case EnemyBuff.Anxiety:
            //    break;
            //case EnemyBuff.InFlow:
            //    break;
            case EnemyBuff.Vulnerable:
                return gM.buffM.FindBuff(EnemyBuff.Vulnerable).uiObj.GetComponent<RectTransform>();
            case EnemyBuff.Weak:
                return gM.buffM.FindBuff(EnemyBuff.Weak).uiObj.GetComponent<RectTransform>();
            //case EnemyBuff.Instability:
            //    break;
            //case EnemyBuff.WeakMind:
            //    break;
            case EnemyBuff.Defence:
                return gM.buffM.FindBuff(EnemyBuff.Defence).uiObj.GetComponent<RectTransform>();
            //case EnemyBuff.Block:
            //    break;
            //case EnemyBuff.Charge:
            //    break;
            //case EnemyBuff.PartialInvincibility:
            //    break;
            //case EnemyBuff.Revive:
            //    break;
            default:
                return gM.buffM.FindBuff(EnemyBuff.Bored).uiObj.GetComponent<RectTransform>();
        }
    }

    public BuffBaseState GetBuffState(CharacterBuff characterBuff)
    {
        switch (characterBuff)
        {
            case CharacterBuff.Defence:
                return defenceState;
            //case CharacterBuff.Vengeance:
            //    return boredState;
            //case CharacterBuff.PowerUp:
            //    return boredState;
            case CharacterBuff.Weak:
                return weakState;
            //case CharacterBuff.Inflammable:
            //    return boredState;
            //case CharacterBuff.IsTeamWork:
            //    return boredState;
            //case CharacterBuff.IsSycn:
            //    return boredState;
            default:
                return boredState;
        }
    }

    public BuffBaseState GetBuffState(EnemyBuff enemyBuff)
    {
        switch (enemyBuff)
        {
            case EnemyBuff.Bored:
                return boredState;
            //case EnemyBuff.Anxiety:
            //    break;
            //case EnemyBuff.InFlow:
            //    break;
            case EnemyBuff.Vulnerable:
                return vulnerableState;
            case EnemyBuff.Weak:
                return weakState;
            //case EnemyBuff.Instability:
            //    break;
            //case EnemyBuff.WeakMind:
            //    break;
            case EnemyBuff.Defence:
                return defenceState;
            //case EnemyBuff.Block:
            //    break;
            //case EnemyBuff.Charge:
            //    break;
            //case EnemyBuff.PartialInvincibility:
            //    break;
            //case EnemyBuff.Revive:
            //    break;
            default:
                return boredState;
        }
    }

    public void SetBuffList(EnemyBuff[] enemyBuffArray)
    {
        for (int i = 0; i < enemyBuffArray.Length; i++)
        {
            enemyBuffs.Add(enemyBuffArray[i]);
        }
    }

    public void SetBuffList(CharacterBuff[] characterBuffArray)
    {
        for (int i = 0; i < characterBuffArray.Length; i++)
        {
            characterBuffs.Add(characterBuffArray[i]);
        }
    }

    //public void SetBuffStateFindTrans(EnemyBuff enemyBuff)
    //{
    //    switch (enemyBuff)
    //    {
    //        case EnemyBuff.Bored:
    //            currentState = boredState;
    //            buffTrans = gM.buffM.FindBuff(EnemyBuff.Bored).uiObj.GetComponent<RectTransform>();
    //                break;
    //        //case EnemyBuff.Anxiety:
    //        //    break;
    //        //case EnemyBuff.InFlow:
    //        //    break;
    //        case EnemyBuff.Vulnerable:
    //            currentState = vulnerableState;
    //            buffTrans = gM.buffM.FindBuff(EnemyBuff.Vulnerable).uiObj.GetComponent<RectTransform>();
    //            break;
    //        case EnemyBuff.Weak:
    //            currentState = weakState;
    //            buffTrans = gM.buffM.FindBuff(EnemyBuff.Weak).uiObj.GetComponent<RectTransform>();
    //            break;
    //        //case EnemyBuff.Instability:
    //        //    break;
    //        //case EnemyBuff.WeakMind:
    //        //    break;
    //        case EnemyBuff.Defence:
    //            currentState = defenceState;
    //            buffTrans = gM.buffM.FindBuff(EnemyBuff.Defence).uiObj.GetComponent<RectTransform>();
    //            break;
    //        //case EnemyBuff.Block:
    //        //    break;
    //        //case EnemyBuff.Charge:
    //        //    break;
    //        //case EnemyBuff.PartialInvincibility:
    //        //    break;
    //        //case EnemyBuff.Revive:
    //        //    break;
    //        default:
    //            currentState = boredState;
    //            buffTrans = gM.buffM.FindBuff(EnemyBuff.Bored).uiObj.GetComponent<RectTransform>();
    //            break;
    //    }
    //}

    //public void SetBuffStateFindTrans(CharacterBuff characterBuff)
    //{
    //    switch (characterBuff)
    //    {
    //        case CharacterBuff.Defence:
    //            currentState = defenceState;
    //            buffTrans = gM.buffM.FindBuff(CharacterBuff.Defence).uiObj.GetComponent<RectTransform>();
    //            break;
    //        case CharacterBuff.Vengeance:
    //            break;
    //        case CharacterBuff.PowerUp:
    //            break;
    //        case CharacterBuff.Weak:
    //            break;
    //        case CharacterBuff.Inflammable:
    //            break;
    //        case CharacterBuff.IsTeamWork:
    //            break;
    //        case CharacterBuff.IsSycn:
    //            break;
    //        default:
    //            break;
    //    }
    //}
#endregion
}
