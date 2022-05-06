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
    public CharacterInfo characterInfo;

    public virtual void Start()
    {
        gM = FindObjectOfType<GameMaster>();
        
        // Start()的唤醒周期在我们的LoadScene方法后，貌似会导致空指针
        // 此处代码可放OnNewGameStarted()里，如果继承类override了OnNewGameStarted()，调用base.NewGameStarted()即可
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
