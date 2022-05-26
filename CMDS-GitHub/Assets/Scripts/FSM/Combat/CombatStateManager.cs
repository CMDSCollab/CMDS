using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateManager : MonoBehaviour
{
    private GameMaster gM;
    public CombatBaseState currentState;
    public CS_Start startState = new CS_Start();
    public CS_AI1 ai1State = new CS_AI1();
    public CS_AI2 ai2State = new CS_AI2();
    public CS_Enemy enemyState = new CS_Enemy();
    public CS_End endState = new CS_End();

    public bool isUpdate = false;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        currentState = startState;
    }

    void Update()
    {
        if (isUpdate == true)
        {
            currentState.UpdateState(gM);
        }
    }

    public void SwitchCombatState(CombatBaseState state)
    {
        currentState = state;
        currentState.EnterState(gM);
    }

    public void CombatStateProcess()
    {
        currentState = startState;
        currentState.EnterState(gM);
    }
}
