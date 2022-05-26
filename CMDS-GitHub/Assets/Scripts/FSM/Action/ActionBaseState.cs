using UnityEngine;
using UnityEngine.UI;

public abstract class ActionBaseState
{
    public abstract void EnterState(GameMaster gM, int value);
    public abstract void BeforeUpdate(GameMaster gM, int value);
    public abstract void UpdateState(GameMaster gM, int value);
    public abstract void AfterUpdate(GameMaster gM, int value);
    public abstract void EndState(GameMaster gM, int value);
    public Transform CurrentAITarget(GameMaster gM)
    {
        if (gM.combatSM.currentState == gM.combatSM.ai1State)
        {
            switch (gM.characterM.mainCharacterType)
            {
                case CharacterType.Designer:
                    return gM.aiM.proAI.transform;
                case CharacterType.Programmmer:
                    return gM.aiM.desAI.transform;
                case CharacterType.Artist:
                    return gM.aiM.desAI.transform;
                default:
                    return gM.aiM.des.transform;
            }
        }
        else if (gM.combatSM.currentState == gM.combatSM.ai2State)
        {
            switch (gM.characterM.mainCharacterType)
            {
                case CharacterType.Designer:
                    return gM.aiM.artAI.transform;
                case CharacterType.Programmmer:
                    return gM.aiM.artAI.transform;
                case CharacterType.Artist:
                    return gM.aiM.proAI.transform;
                default:
                    return gM.aiM.des.transform;
            }
        }
        else
        {
            return gM.aiM.des.transform;
        }
    }
}
