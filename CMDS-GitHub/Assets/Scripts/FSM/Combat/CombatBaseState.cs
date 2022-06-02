using UnityEngine;

public abstract class CombatBaseState
{
    public abstract void EnterState(GameMaster gM);
    public abstract void UpdateState(GameMaster gM);
    public abstract void EndState(GameMaster gM);
}
