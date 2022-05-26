using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct goldRange { public int minGold; public int maxGold; }

public class RewardPanel : MonoBehaviour
{
    public GameMaster gM;
    public List<goldRange> goldRangesByLv;

    public GameObject goldButton;
    public Text goldButtonText;

    public int currentGold;
    public int RollTheGold(int enemyLv)
    {
        return Random.Range(goldRangesByLv[enemyLv].minGold, goldRangesByLv[enemyLv].maxGold);
    }

    public void SetRewardPanel(int enemyLv)
    {
        SetGold(enemyLv);
    }

    public void SetGold(int enemyLv)
    {
        currentGold = RollTheGold(enemyLv);
        goldButtonText.text = "Gold: " + currentGold.ToString();
        goldButton.SetActive(true);
    }

    public void OnClickGoldButton()
    {
        gM.characterM.mainCharacter.gold += currentGold;
        goldButton.SetActive(false);
    }

    private void Awake()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
