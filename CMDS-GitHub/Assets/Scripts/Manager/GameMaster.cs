using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AIManager aiM;
    public EnemyMaster enM;
    public HandManager handM;
    public DeckManager deckM;
    public CardRepoManager cardRepoM;
    public ButtonManager buttonM;
    public CardFuntionManager cardFunctionM;
    public CharacterManager characterM;
    public BuffManager buffM;
    public FightManager fightM;
    public MapManager mapM;

    public Canvas uiCanvas;

    public void PrepareFight()
    {
        if (GameObject.Find("GlobalManager") != null)
        {
            characterM.mainCharacterType = GameObject.Find("GlobalManager").GetComponent<GlobalMaster>().characterType;
        }
        characterM.InitializeCharacters();
        enM.InitializeEnemy();
        deckM.PrepareDeckAndHand();
    }

    public void FightEndReset()
    {
        buffM.activeCharacterBuffs.Clear();
        buffM.activeEnemyBuffs.Clear();
        buffM.activeCBuffs.Clear();
        buffM.activeEBuffs.Clear();
        handM.handCardList.Clear();
        switch (characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                Destroy(aiM.proAI.gameObject);
                Destroy(aiM.artAI.gameObject);
                Destroy(aiM.des.gameObject);
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
        }
        Destroy(enM.enemyTarget.gameObject);
        GameObject handObj = uiCanvas.transform.Find("Hand").gameObject;
        for (int i = 0; i < handObj.transform.childCount; i++)
        {
            Destroy(handObj.transform.GetChild(i).gameObject);
        }
    }
}
