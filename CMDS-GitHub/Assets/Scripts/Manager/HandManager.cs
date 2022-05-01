using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<GameObject> handCardList;
    public Vector3 initialCardPos;


    private void Start()
    {
        handCardList = new List<GameObject>();
    }

    public void OrganizeHand()
    {
        for(int index = 1; index < handCardList.Count + 1; index++)
        {
            Transform cardTrans = handCardList[index - 1].transform;
            cardTrans.position = new Vector3(initialCardPos.x + 150 *(index - 1), initialCardPos.y, -index);
            handCardList[index - 1].GetComponent<CardManager>().handIndex = index;
        }
    }
}
