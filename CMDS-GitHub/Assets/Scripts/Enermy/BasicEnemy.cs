using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#region �������˽�ɫ˵��
// 1. ���ȣ��½�һ���ű�������EM��EE��EBΪǰ׺����׺Ϊenemy���֣����»��� _ ���ӣ�eg��EB_JosefFames���ýű��̳�BasicEnemy
// 2. ���ݸ�enemy��Ϣ��ʵBuffManager��EnemyBuff�⣨���Enemyû����Ҫ����Buff������һ����
//    2.1 �����ڽű��е�EnemyBuffö�����Buff����
//    2.2 Ȼ���ڳ�����BuffManager��Inspector�ϵ�EnemyBuffs���list�У���Ӹ�buff�ļ�¼�Ͷ�Ӧ��BuffImage��icon�زĿ���ȥhttps://game-icons.net/��
// 3. ����EnemyInfo�ű��е�MinionType��EliteType��BossType������Ӷ�Ӧ���½�ɫ
// 4. ���ɫ��������ͼ�����ڴ�EnemyMaster��EnemyIntention��ӣ�����Ҫȥ�����ڵ�EnemyMaster�������Intention������Image
// 4. �����EnemyMaster�е�EnemyObjAddComponent��������Ӿ����SwitchCase����addComponent�ı�д
// 5. �½�Enemy��Scriptable obj���趨��ز�����������scriptable obj���ص�������EnemyManager��TestEnemy��
//    5.1 ͼƬ��������ң����ǵ���������
//    5.2 Intention��Tendency��ֵ����������100
// 6. �����½���Enemy�ű������ݽ��и��ģ�����ο�EM_ESPlayerMature
#endregion

public enum MagicCircleState { In, Out }

public class BasicEnemy : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;

    private int maxHp;
    public int healthPoint;
    public EnemyInfo enemyInfo;
    public EnemyIntention currentIntention;

    private Text enemyName;
    private Image portrait;
    private Slider hpBar;
    private Text hpRatio;
    private MagicCircleState magicCircleState = MagicCircleState.In;

    #region ���ʦ��ر���
    public int skillLv;
    #endregion

    public void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void Update()
    {
        UpdateUI();
        //��������MCÿ֡��һ�μ��
        //MainChaMCChange();
    }

    public void InitializeEnemyUI()
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
        Debug.Log("YouWin");
    }

    public virtual void TakeAction()
    {

    }

    public virtual void TakeDamage(int dmgValue)
    {
        healthPoint -= DmgValueCalculation(gM.buffM.EnemyTakeDamage(dmgValue));
    }

    public virtual int DmgValueCalculation(int dmgValue)
    {
        return dmgValue;
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
        }
    }
    #endregion

    #region Magic Circle
    //���ʦMC�߼������� ���ʦ��cha �� ����skillLv ���߽��бȽ� diff = cha - skillLv
    //�� diff > 10 ʱ�����˻��anxiety ������MC�ж� 30%����
    //��10 > diff > 0ʱ�����˻��MC ����ÿ�غϳ����������ʦ��cha�˺�
    //��diff < 0 ʱ�����˻��bored ������MC�ж� 60%����
    //�����˴���MCʱ��inflow����������������ˡ�
    public void MainChaMCChange()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                int chaLv = gM.aiM.des.challengeLv;
                int difference = chaLv - skillLv;
                if (difference > 10)
                {
                    gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 0);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 1);
                    MagicCirleStateControl(30);
                }
                if (10 > difference && difference >= 0)
                {
                    gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 1);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 0);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 0);
                    MagicCirleStateControl(0);
                }
                if (difference < 0)
                {
                    gM.buffM.SetEnemyBuff(EnemyBuff.InFlow, false, 0);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Bored, false, 1);
                    gM.buffM.SetEnemyBuff(EnemyBuff.Anxiety, false, 0);
                    MagicCirleStateControl(60);
                }
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
        }
    }

    public void MagicCirleStateControl(int weight)
    {
        int dice = Random.Range(0, 100);
        if (dice < weight)
        {
            magicCircleState = MagicCircleState.Out;
            transform.Find("MagicCircle").gameObject.SetActive(false);
            gM.buffM.SetEnemyBuff(EnemyBuff.Vulnerable, false, 0);
            gM.buffM.SetEnemyBuff(EnemyBuff.Weak, false, 0);
        }
        else
        {
            magicCircleState = MagicCircleState.In;
            transform.Find("MagicCircle").gameObject.SetActive(true);
            gM.buffM.SetEnemyBuff(EnemyBuff.Vulnerable, false, 1);
            gM.buffM.SetEnemyBuff(EnemyBuff.Weak, false, 1);
        }
    }

    public void MagicCirleStateControl(string pro)
    {
       
    }

    public void MagicCirleStateControl(float art)
    {

    }
    #endregion
}
