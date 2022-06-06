using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Intentions{None,Attack,Heal,Shield,Buff,Debuff}

[System.Serializable]
public struct IntentionManager
{
    public Intentions intention;
    public Sprite image;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Object/New Character")]
public class CharacterInfo : ScriptableObject
{
    public CharacterType characterType;
    public List<IntentionManager> intentions;
    public int maxHp;
    public int initialGold;
}
