using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Designer Card", menuName = "Scriptable Object/New Designer Card")]
public class CardInfoDsgn : CardInfo
{
    [Header("Designer Area")]
    [Tooltip("是否给异术家加能量")]
    public bool isArtEnergy;
    [Tooltip("是否给程序猿加能量")]
    public bool isProEnergy;
    [Tooltip("为其增加能量的数量")]
    public int eneryPoint;
    [Tooltip("是否增加1格矩形能量槽")]
    public bool isAddCubeSlot;
    [Tooltip("是否增加1格圆能量槽")]
    public bool isAddCircleSlot;
    [Tooltip("是否会改变程序猿的意图")]
    public bool isChangeProIntention;
    [Tooltip("是否会改变异术家的意图")]
    public bool isChangeArtIntention;
    [Tooltip("是否会增加挑战值")]
    public bool isAddChallenge;
    [Tooltip("是否会减少挑战值")]
    public bool isSubChallenge;
}
