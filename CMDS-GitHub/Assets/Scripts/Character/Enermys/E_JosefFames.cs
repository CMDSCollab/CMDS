using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_JosefFames : Enemy
{
    public float maxHealth1P;
    public float maxHealth2P;
    public int damage1P;
    public int damage2P;
    public int respawnTurn;
    private int enemySequence = 1;
    private int respawnCount;

    public override void TakeAction()
    {
        if (enemySequence == 1)
        {
            gM.mainCharacter.TakeDamage(damage1P);
            Debug.Log("hhhh");
        }
        if (enemySequence == 2)
        {
            gM.mainCharacter.TakeDamage(damage2P);
        }
    }
}
