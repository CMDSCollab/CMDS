using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonStatusBar : MonoBehaviour
{
    public Text goldIntText;
    public GameMaster gM;

    public int gold = 0;

    public List<CharacterInfo> chaInfos;
    public CharacterInfo currentInfo;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        DetectChaType();

    }

    public void GoldChange(int changeAmount)
    {
        gold += changeAmount;
        goldIntText.text = gold.ToString();
    }

    public void DetectChaType()
    {
        switch (gM.characterM.mainCharacterType)
        {
            case CharacterType.Designer:
                currentInfo = chaInfos[0];
                break;
            case CharacterType.Programmmer:
                break;
            case CharacterType.Artist:
                break;
            default:
                break;
        }

        GoldChange(currentInfo.initialGold);

    }

    void UpdateUI()
    {
        //goldIntText.text = currentInfo.in .ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
