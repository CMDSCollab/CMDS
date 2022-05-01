using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Designer Card", menuName = "Scriptable Object/New Designer Card")]
public class CardInfoDsgn : CardInfo
{
    [Header("Designer Area")]
    [Tooltip("�Ƿ�������Ҽ�����")]
    public bool isArtEnergy;
    [Tooltip("�Ƿ������Գ������")]
    public bool isProEnergy;
    [Tooltip("Ϊ����������������")]
    public int eneryPoint;
    [Tooltip("�Ƿ�����1�����������")]
    public bool isAddCubeSlot;
    [Tooltip("�Ƿ�����1��Բ������")]
    public bool isAddCircleSlot;
    [Tooltip("�Ƿ��ı����Գ����ͼ")]
    public bool isChangeProIntention;
    [Tooltip("�Ƿ��ı������ҵ���ͼ")]
    public bool isChangeArtIntention;
    [Tooltip("�Ƿ��������սֵ")]
    public bool isAddChallenge;
    [Tooltip("�Ƿ�������սֵ")]
    public bool isSubChallenge;
}
