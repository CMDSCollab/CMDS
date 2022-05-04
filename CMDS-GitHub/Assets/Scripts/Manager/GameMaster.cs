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
    //public SceneMaster sceneM;
    public LocalMaster localM;

    public Canvas uiCanvas;
    public CharacterType characterType;
    [HideInInspector]
    public BasicCharacter mainCharacter;

    public void Start()
    {
        if (GameObject.Find("GlobalManager") != null)
        {
            characterType = GameObject.Find("GlobalManager").GetComponent<GlobalMaster>().characterType;
        }

        if (characterType == CharacterType.Designer)
        {
            mainCharacter = aiM.des;
            //characterM.designerPl = aiM.des;
        }
        if (characterType == CharacterType.Programmmer)
        {
            mainCharacter = aiM.pro;
            //characterM.programmerPl = aiM.pro;
            //characterM.programmerPl.OnNewGameStarted();
            aiM.pro.OnNewGameStarted();
        }
        PrepareFight();
    }

    public void PrepareFight()
    {
        deckM.PrepareDeckAndHand();
    }
}
