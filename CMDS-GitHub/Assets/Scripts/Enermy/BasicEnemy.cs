using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#region 新增敌人角色说明
// 1. 首先，新建一个脚本名字以EM、EE、EB为前缀，后缀为enemy名字，以下划线 _ 连接，eg：EB_JosefFames，该脚本继承BasicEnemy
// 2. 根据该enemy信息充实BuffManager的EnemyBuff库（如果Enemy没有需要的新Buff忽略这一步）
//    2.1 首先在脚本中的EnemyBuff枚举添加Buff名字
//    2.2 然后在场景中BuffManager的Inspector上的EnemyBuffs这个list中，添加该buff的记录和对应的BuffImage，icon素材可以去https://game-icons.net/找
// 3. 对于EnemyInfo脚本中的MinionType、EliteType、BossType进行添加对应的新角色
// 4. 如角色有新增意图，可在此EnemyMaster的EnemyIntention添加，并需要去场景内的EnemyMaster挂载与该Intention关联的Image
// 4. 随后在EnemyMaster中的EnemyObjAddComponent方法中添加具体的SwitchCase，和addComponent的编写
// 5. 新建Enemy的Scriptable obj，设定相关参数，随后将这个scriptable obj挂载到场景中EnemyManager的TestEnemy上
//    5.1 图片网上随便找，但是得是正方形
//    5.2 Intention的Tendency总值加起来得是100
// 6. 对于新建的Enemy脚本的内容进行更改，具体参考EM_ESPlayerMature
#endregion

public enum MagicCircleState { In, Out }

public class BasicEnemy : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;

    public int maxHp;
    public int healthPoint;
    public EnemyInfo enemyInfo;
    public EnemyIntention currentIntention;

    public Text enemyName;
    public Image portrait;
    public Slider hpBar;
    public Text hpRatio;
    public MagicCircleState magicCircleState = MagicCircleState.In;

    #region 设计师相关变量
    public int skillLv;
    #endregion

    public virtual void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void Update()
    {
        UpdateUI();
        if (healthPoint<=0)
        {
            EnemyDefeated();
        }
        //不建议让MC每帧做一次检测
        //MainChaMCChange();
    }

    public virtual void InitializeEnemyUI()
    {
        enemyName = transform.Find("Name").GetComponent<Text>(); 
        portrait = transform.Find("Portrait").GetComponent<Image>();
        hpBar = transform.Find("HpBar").GetComponent<Slider>();
        hpRatio = transform.Find("HpBar").Find("HpRatio").GetComponent<Text>();

        portrait.sprite = enemyInfo.images[0];
        enemyName.text = enemyInfo.enemyName;
        maxHp = enemyInfo.maxHealth;
        healthPoint = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
        skillLv = enemyInfo.defaultSkill;

        GenerateEnemyIntention();
        MainChaMCChange();
    }

    public void UpdateUI()
    {
        hpBar.value = healthPoint;
        hpRatio.text = healthPoint.ToString() + "/" + maxHp.ToString();
    }

    public virtual void EnemyDefeated()
    {
        gM.uiCanvas.transform.Find("Buttons").Find("FightEnd").gameObject.SetActive(true);
    }

    public virtual void TakeAction()
    {

    }

    public virtual void TakeDamage(int dmgValue)
    {
        healthPoint -= gM.buffM.EnemyTakeDamage(dmgValue);
    }


    //承受真实伤害：无视所有减益buff
    public void TakeTrueDamage(int dmg)
    {
        healthPoint -= dmg;
    }

    #region Intention
    public virtual void GenerateEnemyIntention()
    {
        List<int> tendencyValues = new List<int>();
        foreach (EnemyIntentionRatio ratio in enemyInfo.basicIntentions)
        {
            tendencyValues.Add(ratio.tendency);
        }
        int random = Random.Range(0, 100);
        for (int i = 0; i < tendencyValues.Count; i++)
        {
            if (random < tendencyValues[i])
            {
                currentIntention = enemyInfo.basicIntentions[i].intention;
                break;
            }
            else
            {
                random -= tendencyValues[i];
            }
        }
        SetIntentionUI();
    }

    public void SetIntentionUI()
    {
        Sprite imageToSet = null;
        foreach (EnemyIntentionImages intentionImage in gM.enM.intentionImages)
        {
            if (intentionImage.enemyIntention == currentIntention)
            {
                imageToSet = intentionImage.image;
            }
        }
        switch (currentIntention)
        {
            case EnemyIntention.Attack:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Attack";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Defence:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Defence";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Heal:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Heal";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Taunt:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Taunt";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Skill:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Skill";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.MultiAttack:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "MultiAttack";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Charge:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Charge";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Block:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Block";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.HoneyShoot:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "HoneyShoot";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.FireShoot:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "FireShoot";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.Comment:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "Comment";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
            case EnemyIntention.ToComment:
                transform.Find("Intention").Find("Name").GetComponent<Text>().text = "ToComment";
                transform.Find("Intention").Find("Image").GetComponent<Image>().sprite = imageToSet;
                break;
        }
    }
    #endregion

    #region Magic Circle
    //设计师MC逻辑：输入 设计师的cha 与 敌人skillLv 两者进行比较 diff = cha - skillLv
    //当 diff > 10 时，敌人获得anxiety 并进行MC判定 30%掉出
    //当10 > diff > 0时，敌人获得MC 并于每回合承受来自设计师的cha伤害
    //当diff < 0 时，敌人获得bored 并进行MC判定 60%掉出
    //当敌人处于MC时（inflow），获得虚弱和易伤。

    //程序猿MC逻辑：当currentErrors.Count >= 2时，开始掉出MC的判定
    //当 Count = 2时，MC判定每回合10%掉出 
    //在此之上，Count每多1，MC掉出概率高10%（最高6个BUG，掉出概率50%），并记录最高值（highestDropWeight）
    //只有消减Count到0时，掉出概率才会重置，否则取最高值（例：将6个BUG消至1个BUG，掉出概率依然为50%）
    private int highestDropWeight = 0;

    public void MainChaMCChange()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                int chaLv = gM.aiM.des.challengeLv;
                ////int difference = chaLv - skillLv;
                //int difference = skillLv - chaLv;
                int chaSubtractSkill = chaLv - skillLv;
                int skillSubtractCha = skillLv - chaLv;
                if (skillSubtractCha > 10)
                {
                    //gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 1);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 0);
                    //MagicCirleStateControl(30);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 1);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 0);
                    //gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0);
                    //gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1);
                    //gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0);
                    //MagicCircleDropOut(30);
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    MagicCircleDropOut(30);
                }
                if (chaSubtractSkill <= 10  && skillSubtractCha <= 10)
                {
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    MagicCirleRecapture();
                }
                if (chaSubtractSkill > 10)
                {
                    gM.buffM.SetBuff(EnemyBuff.InFlow, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Bored, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
                    gM.buffM.SetBuff(EnemyBuff.Anxiety, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
                    MagicCircleDropOut(60);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 0);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 1);
                    //MagicCirleStateControl(60);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 1);
                    //gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 0);
                    //MagicCirleStateControl(60);
                }
                break;
            case CharacterType.Programmmer:
                Programmer programmer = gM.aiM.pro;
                int dropWeight = 0;
                // 当highestDropWeight == 0，说明刚刚重置，此时只要看是否Count>=2，并记录最高值
                if (highestDropWeight == 0)
                {
                    if (programmer.currentErrors.Count >= 2)
                    {
                        dropWeight = 10 * programmer.currentErrors.Count;
                        highestDropWeight = dropWeight;
                    }
                }
                else
                {
                    // 首先判断Count是否已经等于0，如果是则进行重置
                    if (programmer.currentErrors.Count == 0)
                    {
                        dropWeight = 0;
                        highestDropWeight = 0;
                    }
                    // 如果Count>0，则比较一下当前值和最高值，记录最高值或将最高值赋给dropWeight
                    else
                    {
                        dropWeight = 10 * programmer.currentErrors.Count;
                        if (dropWeight >= highestDropWeight)
                        {
                            highestDropWeight = dropWeight;
                        }
                        else
                        {
                            dropWeight = highestDropWeight;
                        }
                    }
                    
                }
                // 进行MC掉出判定
                MagicCircleDropOut(dropWeight);
                Debug.Log("当前MC掉出概率：" + dropWeight);
                break;
            case CharacterType.Artist:
                break;
        }
    }

    public void MagicCircleDropOut(int weight)
    {
        int dice = Random.Range(0, 100);
        if (dice < weight)
        {
            magicCircleState = MagicCircleState.Out;
            transform.Find("MagicCircle").gameObject.SetActive(false);
            gM.buffM.SetBuff(EnemyBuff.Weak, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
            gM.buffM.SetBuff(EnemyBuff.Vulnerable, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 0, BuffSource.Enemy);
        }
    }

    public void MagicCirleRecapture()
    {
            magicCircleState = MagicCircleState.In;
            transform.Find("MagicCircle").gameObject.SetActive(true);
            gM.buffM.SetBuff(EnemyBuff.Weak, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
            gM.buffM.SetBuff(EnemyBuff.Vulnerable, BuffTimeType.Permanent, 999, BuffValueType.NoValue, 1, BuffSource.Enemy);
    }

    public void MagicCirleStateControl(string pro)
    {
       
    }

    public void MagicCirleStateControl(float art)
    {

    }
    #endregion
}
