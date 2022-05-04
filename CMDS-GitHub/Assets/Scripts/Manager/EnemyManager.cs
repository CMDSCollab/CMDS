using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> currentEnemies;
    public Enemy currentTarget;

    public GameMaster gM;

    public List<GameObject> enemyObjs = new List<GameObject>();
    public EnemyInfo enemyInfo;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();

        enemyInfo = enemyObjs[0].GetComponent<Enemy>().enemyInfo;
        InitializeEnemy(enemyObjs[0]);
        FacingPlayerAsDesigner();
    }

    public void InitializeEnemy(GameObject enemyToGenerate)
    {
        GameObject enemyObj = Instantiate(enemyToGenerate,gM.uiCanvas.transform,false);
        currentTarget = enemyObj.GetComponent<Enemy>();
        enemyObj.transform.SetAsFirstSibling();
        
    }

    public void CheckMC()
    {
        foreach (Enemy unit in currentEnemies)
        {
            unit.ChallengeVsSkill();
        }
    }

    public void FacingPlayerAsDesigner()
    {
        foreach (Enemy unit in currentEnemies)
        {
            unit.isPlDesigner = true;
        }
    }

    public void GenerateIntentions()
    {
        foreach(Enemy unit in currentEnemies)
        {
            unit.enAI.GenerateIntention();
        }
    }

    public void EnemiesActions()
    {
        currentTarget.TakeAction();
        //foreach(Enemy unit in currentEnemies)
        //{
        //    unit.TakeAction();
        //}
    }
}
