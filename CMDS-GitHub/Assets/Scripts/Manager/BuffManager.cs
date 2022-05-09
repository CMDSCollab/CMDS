using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region EnemyBuff说明
//Bored 
//Anxiety
//InFlow
//Vulnerable
//Weak
//Instability
//WeakMind
//Defence
//Skill 技能值 将与设计师的challenge比较 只存在于主要角色为设计师的情况下
//Block 该轮不会受到伤害
//Charge 下回合造成双倍伤害
#endregion
public enum EnemyBuff { Bored, Anxiety, InFlow, Vulnerable, Weak, Instability, WeakMind, Defence, Skill, Block, Charge}
public enum CharacterBuff { Shield, Vengeance, PowerUp, Weak,Challenge}

[System.Serializable]
public struct EnemyBuffInfo { public EnemyBuff buffType; public Sprite buffImage; }
[System.Serializable]
public struct CharacterBuffInfo { public CharacterBuff buffType; public Sprite buffImage; }

public class BuffManager : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    public List<CharacterBuffInfo> characterBuffs;
    public List<EnemyBuffInfo> enemyBuffs;
    public GameObject buffPrefab;

    public Dictionary<CharacterBuff, GameObject> activeCharacterBuffs = new Dictionary<CharacterBuff, GameObject>();
    public Dictionary<EnemyBuff, GameObject> activeEnemyBuffs = new Dictionary<EnemyBuff, GameObject>();

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void SetCharacterBuff(CharacterBuff buffType, bool isBuffWithValue, int buffValue)
    {
        if (isBuffWithValue == true)
        {
            if (activeCharacterBuffs.ContainsKey(buffType))
            {
                GameObject obj = activeCharacterBuffs[buffType];
                obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
                activeCharacterBuffs.Remove(buffType);
                activeCharacterBuffs.Add(buffType, obj);
            }
            else
            {
                GameObject obj;
                GameObject buffObj = Instantiate(buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false);
                foreach (CharacterBuffInfo recordBuff in characterBuffs)
                {
                    if (recordBuff.buffType == buffType)
                    {
                        buffObj.GetComponent<Image>().sprite = recordBuff.buffImage;
                    }
                }
                obj = buffObj;
                obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
                activeCharacterBuffs.Add(buffType, obj);
            }
            if (buffValue == 0)
            {
                Destroy(activeCharacterBuffs[buffType]);
                activeCharacterBuffs.Remove(buffType);
            }
        }
        else
        {
            if (activeCharacterBuffs.ContainsKey(buffType) == false)
            {
                GameObject obj;
                GameObject buffObj = Instantiate(buffPrefab, gM.characterM.mainCharacter.transform.Find("BuffArea"), false);
                foreach (CharacterBuffInfo recordBuff in characterBuffs)
                {
                    if (recordBuff.buffType == buffType)
                    {
                        buffObj.GetComponent<Image>().sprite = recordBuff.buffImage;
                    }
                }
                obj = buffObj;
                obj.transform.Find("Value").gameObject.SetActive(false);
                activeCharacterBuffs.Add(buffType, obj);
            }
            if (buffValue == 0)
            {
                Destroy(activeCharacterBuffs[buffType]);
                activeCharacterBuffs.Remove(buffType);
            }
        }
    }

    public void SetEnemyBuff(EnemyBuff buffType, bool isBuffWithValue, int buffValue)
    {
        if (isBuffWithValue == true)
        {
            if (activeEnemyBuffs.ContainsKey(buffType))
            {
                GameObject obj = activeEnemyBuffs[buffType];
                obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
                activeEnemyBuffs.Remove(buffType);
                activeEnemyBuffs.Add(buffType, obj);
            }
            else
            {
                GameObject obj;
                GameObject buffObj = Instantiate(buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false);
                foreach (EnemyBuffInfo recordBuff in enemyBuffs)
                {
                    if (recordBuff.buffType == buffType)
                    {
                        buffObj.GetComponent<Image>().sprite = recordBuff.buffImage;
                    }
                }
                obj = buffObj;
                obj.transform.Find("Value").GetComponent<Text>().text = buffValue.ToString();
                activeEnemyBuffs.Add(buffType, obj);
            }
            if (buffValue == 0)
            {
                Destroy(activeEnemyBuffs[buffType]);
                activeEnemyBuffs.Remove(buffType);
            }
        }
        else
        {
            if (activeEnemyBuffs.ContainsKey(buffType) == false)
            {
                GameObject obj;
                GameObject buffObj = Instantiate(buffPrefab, gM.enM.enemyTarget.transform.Find("BuffArea"), false);
                foreach (EnemyBuffInfo recordBuff in enemyBuffs)
                {
                    if (recordBuff.buffType == buffType)
                    {
                        buffObj.GetComponent<Image>().sprite = recordBuff.buffImage;
                    }
                }
                obj = buffObj;
                obj.transform.Find("Value").gameObject.SetActive(false);
                activeEnemyBuffs.Add(buffType, obj);
            }
            if (buffValue == 0)
            {
                Destroy(activeEnemyBuffs[buffType]);
                activeEnemyBuffs.Remove(buffType);
            }
        }
    }
}
