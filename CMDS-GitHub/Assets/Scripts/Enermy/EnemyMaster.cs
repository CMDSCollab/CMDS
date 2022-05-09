using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyIntention { Attack, Defence, Heal, Taunt, Skill, MultiAttack, Charge, Block}
[System.Serializable]
public struct EnemyIntentionImages
{
    public EnemyIntention enemyIntention;
    public Sprite image;
}

public class EnemyMaster : MonoBehaviour
{
    [HideInInspector]
    public GameMaster gM;
    public BasicEnemy enemyTarget;
    public GameObject enemyPrefab;
    public List<EnemyIntentionImages> intentionImages;

    private EnemyInfo enemyInfo; //�����info��basicE�����info��;�Ĳ������ڣ�����info��Ϊ�˸��ݸ�infoִ��addcomponent�ķ�����û�������ô�
    private GameObject enemyObj;

    public EnemyInfo testEnemy;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        //SetEnemyInfo(testEnemy);
    }

    public void SetEnemyInfo(EnemyInfo enemyInfoToSet)
    {
        enemyInfo = enemyInfoToSet;
    }

    public void InitializeEnemy()
    {
        enemyObj = Instantiate(enemyPrefab, gM.uiCanvas.transform, false);
        EnemyObjAddComponent(enemyObj);
        enemyObj.transform.SetAsFirstSibling();

        enemyTarget = enemyObj.GetComponent<BasicEnemy>();
        enemyTarget.enemyInfo = enemyInfo;
        enemyTarget.InitializeEnemyUI();
    }

    private void EnemyObjAddComponent(GameObject objToAdd)
    {
        switch (enemyInfo.enemyType)
        {
            case EnemyType.Minion:
                switch (enemyInfo.minionType)
                {
                    case MinionType.TechNerd:
                        objToAdd.AddComponent<EM_TechNerd>();
                        break;
                    case MinionType.ESPlayerMature:
                        objToAdd.AddComponent<EM_ESPlayerMature>();
                        break;
                    case MinionType.Ma:
                        break;
                    case MinionType.Mb:
                        break;
                    case MinionType.Mc:
                        break;
                }
                break;
            case EnemyType.Elite:
                switch (enemyInfo.eliteType)
                {
                    case EliteType.Streamer:
                        break;
                    case EliteType.Ea:
                        break;
                    case EliteType.Eb:
                        break;
                    case EliteType.Ec:
                        break;
                }
                break;
            case EnemyType.Boss:
                switch (enemyInfo.bossType)
                {
                    case BossType.JosefFames:
                        objToAdd.AddComponent<EB_JosefFames>();
                        break;
                    case BossType.Ba:
                        break;
                    case BossType.Bb:
                        break;
                    case BossType.Bc:
                        break;
                }
                break;
        }
    }
}
