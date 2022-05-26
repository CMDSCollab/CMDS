using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameMaster gM;
    public GlobalMaster globalM;
    public CardRepoManager cardRepoM;

    public Button drawPileButton;
    public Button discardPileButton;
    public GameObject startScene;
    public GameObject chooseCharacterPanel;
  

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();

    }

    void Update()
    {
        
    }

    public void OnDrawPileClicked()
    {
        cardRepoM.PresentDrawPile();
    }

    public void OnDicardPileClicked()
    {
        cardRepoM.PresentDiscardPile();
    }

    public void OnCardLayoutReturnClicked()
    {
        cardRepoM.RemoveCardsFromLayout();
        cardRepoM.gameObject.SetActive(false);
    }

    public void SynchronizeCardsCountInPileButton(string whichPileNumber)
    {
        if (whichPileNumber == "Draw")
        {

            drawPileButton.transform.Find("DrawPileNumber").GetComponent<Text>().text = gM.deckM.cardInDeckCopy.Count.ToString();
        }
        else if (whichPileNumber == "Discard")
        {
            discardPileButton.transform.Find("DiscardPileNumber").GetComponent<Text>().text = cardRepoM.discardPile.Count.ToString();
        }
       
    }

    public void OnClickNextTurn()
    {
        //gM.fightM.FightProcessManager();
        gM.combatSM.CombatStateProcess();
    }

    public void onClickStartButton()
    {
        startScene.SetActive(false);
        chooseCharacterPanel.SetActive(true);
    }

    public void onClickQuitButton()
    {
        QuitGame();
    }
    public void OnClickDesigner()
    {
        globalM.characterType = CharacterType.Designer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickProgrammer()
    {
        globalM.characterType = CharacterType.Programmmer;
        SceneManager.LoadScene("FightScene");
    }

    public void OnClickArtist()
    {
        globalM.characterType = CharacterType.Artist;
        SceneManager.LoadScene("FightScene");
    }

    public void EndFightBackToMap()
    {
        gM.uiCanvas.transform.Find("RewardPanel").gameObject.SetActive(false);
        gM.FightEndReset();
        gM.mapM.gameObject.SetActive(true);
        gM.uiCanvas.gameObject.SetActive(false);
    }

    public void OnClickBackToMainMenu()
    {
        //globalM.characterType = null;
        SceneManager.LoadScene("SelectCharacter");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
