using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    Designer,
    Programmmer,
    Artist
}

public class BasicCharacter : MonoBehaviour
{
    public GameMaster gM;
    public int healthPoint;
    public int maxHp;
    public int shieldPoint;

    public Text hpText;
    public Text shieldPText;

    private void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        // Start()的唤醒周期在我们的LoadScene方法后，貌似会导致空指针
        // 此处代码可放OnNewGameStarted()里，如果继承类override了OnNewGameStarted()，调用base.NewGameStarted()即可
    }


    public void TakeDamage(int dmg)
    {
        if (shieldPoint > 0)
        {
            if (shieldPoint >= dmg)
            {
                shieldPoint -= dmg;
            }
            else
            {
                int overdmg = dmg - shieldPoint;
                shieldPoint = 0;

                healthPoint -= overdmg;
            }
        }
        else
        {
            healthPoint -= dmg;
        }
    }

    public virtual void OnNewGameStarted()
    {
        gM = FindObjectOfType<GameMaster>();
        gM.cardFunctionM.FunctionBoolValueReset();
    }

    public virtual void OnPlayerTurnEnded()
    {
        
    }
}
