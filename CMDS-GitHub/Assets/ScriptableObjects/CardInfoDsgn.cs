using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SpecialDesFunctionType
{
    None,
    ChangeChallenge,
    ChangeSkill
}

[System.Serializable]

public struct specialDesFunction
{
    public SpecialDesFunctionType desFunctionType;
    public int value;
}

[CreateAssetMenu(fileName = "New Designer Card", menuName = "Scriptable Object/New Designer Card")]
public class CardInfoDsgn : CardInfo
{
    [Header("Designer Area")]

    public List<specialDesFunction> desSpecialFunctions;

}
