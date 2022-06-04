using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonStatusBar : MonoBehaviour
{
    public Text goldIntText;
    public GameMaster gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    void UpdateUI()
    {
        goldIntText.text = gM.characterM.mainCharacter.gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
