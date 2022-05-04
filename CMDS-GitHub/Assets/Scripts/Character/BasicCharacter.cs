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
        // Start()�Ļ������������ǵ�LoadScene������ò�ƻᵼ�¿�ָ��
        // �˴�����ɷ�OnNewGameStarted()�����̳���override��OnNewGameStarted()������base.NewGameStarted()����
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
