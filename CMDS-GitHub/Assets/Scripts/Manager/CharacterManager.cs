using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    #region 角色通用变量
    [Header("通用")]
    public GameMaster gM;
    public CharacterType mainCharacterType;
    public GameObject templateAI;
    public GameObject templateCha;
    public List<CharacterInfo> characters = new List<CharacterInfo>();
    public List<Sprite> characterImages;
    [HideInInspector]
    public CharacterMate mainCharacter;
    public GameObject energyPrefab;
    public List<Sprite> energyImages;
    #endregion

    #region 程序变量
    [Header("程序")]
    public ProDebugUI debugUIPrefab;
    public List<Error> potentialErrors;
    #endregion

    public List<BasicCharacter> charactersList;

    public void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    public void InitializeCharacters()
    {
        switch (mainCharacterType)
        {
            case CharacterType.Designer:
                MainChaGenerateAndInitialize(CharacterType.Designer);
                AIGenerateAndInitialize(CharacterType.Artist, "Left");
                AIGenerateAndInitialize(CharacterType.Programmmer, "Right");

                mainCharacter = gM.aiM.des;
                break;
            case CharacterType.Programmmer:
                MainChaGenerateAndInitialize(CharacterType.Programmmer);
                AIGenerateAndInitialize(CharacterType.Artist, "Left");
                AIGenerateAndInitialize(CharacterType.Designer, "Right");

                mainCharacter = gM.aiM.pro;
                gM.aiM.pro.OnNewGameStarted();
                break;
            case CharacterType.Artist:
                MainChaGenerateAndInitialize(CharacterType.Artist);
                AIGenerateAndInitialize(CharacterType.Designer, "Left");
                AIGenerateAndInitialize(CharacterType.Programmmer, "Right");

                //mainCharacter = gM.aiM.art;
                break;
            default:
                break;
        }
    }

    public void AIGenerateAndInitialize(CharacterType characterType,string leftOrRight)
    {
        GameObject aiObj = Instantiate(templateAI, gM.uiCanvas.transform, false);
        aiObj.transform.SetAsFirstSibling();
        if (leftOrRight == "Left")
        {
            aiObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-750, -30, 0);
        }
        else
        {
            aiObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, -30, 0);
        }
        switch (characterType)
        {
            case CharacterType.Designer:
                aiObj.AddComponent<DesignerAI>();
                aiObj.GetComponent<DesignerAI>().characterInfo = characters[0];
                aiObj.transform.Find("CharacterImage").GetComponent<Image>().sprite = characterImages[0];
                gM.aiM.desAI = aiObj.GetComponent<DesignerAI>();
                break;
            case CharacterType.Programmmer:
                aiObj.AddComponent<ProgrammerAI>();
                aiObj.GetComponent<ProgrammerAI>().characterInfo = characters[1];
                aiObj.transform.Find("CharacterImage").GetComponent<Image>().sprite = characterImages[1];
                gM.aiM.proAI = aiObj.GetComponent<ProgrammerAI>();
                break;
            case CharacterType.Artist:
                aiObj.AddComponent<ArtistAI>();
                aiObj.GetComponent<ArtistAI>().characterInfo = characters[2];
                aiObj.transform.Find("CharacterImage").GetComponent<Image>().sprite = characterImages[2];
                gM.aiM.artAI = aiObj.GetComponent<ArtistAI>();
                break;
        }
    }

    public void MainChaGenerateAndInitialize(CharacterType characterType)
    {
        GameObject chaObj = Instantiate(templateCha, gM.uiCanvas.transform, false);
        chaObj.transform.SetAsFirstSibling();
        chaObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-625, 300, 0);
        
        switch (characterType)
        {
            case CharacterType.Designer:
                chaObj.AddComponent<Designer>();
                chaObj.GetComponent<Designer>().characterInfo = characters[0];
                chaObj.transform.Find("CharacterImage").GetComponent<Image>().sprite = characterImages[0];
                gM.aiM.des = chaObj.GetComponent<Designer>();
                break;
            case CharacterType.Programmmer:
                chaObj.AddComponent<Programmer>();
                chaObj.GetComponent<Programmer>().characterInfo = characters[1];
                chaObj.transform.Find("CharacterImage").GetComponent<Image>().sprite = characterImages[1];
                gM.aiM.pro = chaObj.GetComponent<Programmer>();
                break;
            case CharacterType.Artist:
                //aiObj.AddComponent<Artist>();
                break;
        }
    }
}
