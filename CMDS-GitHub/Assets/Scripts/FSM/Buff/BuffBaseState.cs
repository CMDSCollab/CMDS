using UnityEngine;

public abstract class BuffBaseState
{
    public abstract void EnterState(GameMaster gM);
    public abstract void UpdateState(GameMaster gM);
    public abstract void EndState(GameMaster gM);
}
