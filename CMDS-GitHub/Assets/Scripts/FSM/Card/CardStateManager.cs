using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateManager : MonoBehaviour
{
    private GameMaster gM;
    public CardBaseState currentState;
    public CardS_Attack attackState = new CardS_Attack();
    public CardS_TakeDmg takeDmgState = new CardS_TakeDmg();

    public int changedValue;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        currentState = attackState;
    }

    void Update()
    {
        if (currentState.isUpdate == true)
        {
            currentState.UpdateState(gM, changedValue);
        }
    }

    public void EnterCardState(CardBaseState state, int value)
    {
        currentState = state;
        currentState.EnterState(gM, value);
    }
}
