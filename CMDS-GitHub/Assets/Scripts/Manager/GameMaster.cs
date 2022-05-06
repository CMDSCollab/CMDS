using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public AIManager aiM;
    public EnemyManager enM;
    public HandManager handM;
    public DeckManager deckM;
    public CardRepoManager cardRepoM;
    public ButtonManager buttonM;
    public CardFuntionManager cardFunctionM;
    public CharacterManager characterM;
    public SceneMaster sceneM;
    public LocalMaster localM;


    public void LoadingForFightScene()
    {
        StartCoroutine(LoadLocalMaster()) ;
    }

    public void PrepareFight()
    {
        enM.currentTarget.MagicCircleDetection(100);
        deckM.PrepareDeckAndHand();
    }

    IEnumerator LoadLocalMaster()
    {
        yield return new WaitForSeconds(0.1f);
        localM = FindObjectOfType<LocalMaster>();
        aiM = localM.aiM;
        enM = localM.emM;
        handM = localM.handM;
        deckM = localM.deckM;
        cardRepoM = localM.cardRepoM;
        cardFunctionM = localM.cardfunctionM;
        buttonM = localM.buttonM;
        if (characterM.chosenCharacter == "Designer")
        {
            characterM.designerPl = aiM.des;
            characterM.mainCharacter = aiM.des;
        }

        if(characterM.chosenCharacter == "Programmer")
        {
            characterM.programmerPl = aiM.pro;
            characterM.mainCharacter = aiM.pro;
            characterM.programmerPl.OnNewGameStarted();
        }

        PrepareFight();
    }
}
