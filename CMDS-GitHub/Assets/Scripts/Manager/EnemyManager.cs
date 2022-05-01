using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> currentEnemies;
    public GameMaster gM;
    public Enemy currentTarget;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        FacingPlayerAsDesigner();
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
        foreach(Enemy unit in currentEnemies)
        {
            unit.TakeAction();
        }
    }
}
