using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Minion,
    Elite,
    Boss
}

public enum EnemyRace
{
    JosefFames,
    EdmundMcMillen
}

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Object/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public EnemyRace enemyRace;
}
