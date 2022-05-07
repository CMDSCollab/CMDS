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
    //public SceneMaster sceneM;
    public LocalMaster localM;

    public Canvas uiCanvas;


    public void Start()
    {
        if (GameObject.Find("GlobalManager") != null)
        {
            characterM.mainCharacterType = GameObject.Find("GlobalManager").GetComponent<GlobalMaster>().characterType;
        }
        characterM.InitializeCharacters();
        enM.InitializeEnemy();
        PrepareFight();
    }

    public void PrepareFight()
    {
        deckM.PrepareDeckAndHand();
    }
}
