using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Object/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public float health;
    public float mana;
}
