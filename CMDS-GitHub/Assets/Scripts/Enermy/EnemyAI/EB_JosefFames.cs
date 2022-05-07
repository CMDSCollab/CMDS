using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_JosefFames : BasicEnemy
{
    public float maxHealth1P;
    public float maxHealth2P;
    public int damage1P = 10;
    public int damage2P = 5;
    public int respawnTurn;
    private int enemySequence = 1;
    private int respawnCount;

    public override void TakeAction()
    {
        if (enemySequence == 1)
        {
            gM.characterM.mainCharacter.TakeDamage(damage1P);
        }
        if (enemySequence == 2)
        {
            gM.characterM.mainCharacter.TakeDamage(damage2P);
        }
    }

    public override void EnemyDefeated()
    {
        base.EnemyDefeated();
    }
}
