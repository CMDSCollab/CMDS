using UnityEngine;

public abstract class CardBaseState
{
    public bool isUpdate = false;
    public abstract void EnterState(GameMaster gM,int value);
    public abstract void UpdateState(GameMaster gM, int value);
    public abstract void EndState(GameMaster gM, int value);
}
