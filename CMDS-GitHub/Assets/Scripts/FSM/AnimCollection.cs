using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimChoice
{
    Null,
    ScaleBounce,
    SliderMove,
    BuffValueChange
}

public class AnimCollection : MonoBehaviour
{
    public GameMaster gM;
    public AnimChoice currentChoice = AnimChoice.Null;
    public bool isAnimEntered = false;
    public bool isAnimEnd = false;

    void Start()
    {
        gM = FindObjectOfType<GameMaster>();
    }

    void Update()
    {
        if (currentChoice != AnimChoice.Null)
        {
            FindAnimFunction();
        }
    }

    public void FindAnimFunction()
    {
        switch (currentChoice)
        {
            case AnimChoice.Null:
                break;
            case AnimChoice.ScaleBounce:
                ScaleBounce();
                break;
            case AnimChoice.SliderMove:
                break;
            case AnimChoice.BuffValueChange:
                BuffValueChange();
                break;
            default:
                break;
        }
    }

    private RectTransform targetTrans;
    private float startValue;
    private float targetValue;
    private float endValue;
    private bool isFirstState = true;
    private float textValue;

    #region ScaleBounce
    public void SetScaleBounceValue(RectTransform rectTrans, float start, float target, float end)
    {
        targetTrans = rectTrans;
        startValue = start;
        targetValue = target;
        endValue = end;
        isAnimEntered = true;
    }

    public void ScaleBounce()
    {
        if (isFirstState == true)
        {
            startValue -= 2 * Time.deltaTime;
            targetTrans.localScale = new Vector3(startValue, startValue);
            if (startValue <= targetValue)
            {
                isFirstState = false;
            }
        }
        else
        {
            targetValue += 2 * Time.deltaTime;
            targetTrans.localScale = new Vector3(targetValue, targetValue);
            if (targetValue >= endValue)
            {
                targetTrans.localScale = new Vector3(endValue, endValue);
                isFirstState = true;
                isAnimEnd = true;
                currentChoice = AnimChoice.Null;
            }
        }
    }
    #endregion

    #region BuffValueChange
    public void SetBuffValueChangeValue(RectTransform rectTrans, float start, float target, float end, float textV)
    {
        targetTrans = rectTrans;
        startValue = start;
        targetValue = target;
        endValue = end;
        textValue = textV;
        isAnimEntered = true;
    }

    public void BuffValueChange()
    {
        if (isFirstState == true)
        {
            startValue += 3 * Time.deltaTime;
            targetTrans.localScale = new Vector3(startValue, startValue);
            if (startValue >= targetValue)
            {
                targetTrans.localScale = new Vector3(targetValue, targetValue);
                isFirstState = false;
            }
        }
        else
        {
            targetTrans.Find("Value").GetComponent<Text>().text = textValue.ToString();
            targetValue -= 2 * Time.deltaTime;
            targetTrans.localScale = new Vector3(targetValue, targetValue);
            if (targetValue <= endValue)
            {
                targetTrans.localScale = new Vector3(endValue, endValue);
                isFirstState = true;
                isAnimEnd = true;
                currentChoice = AnimChoice.Null;
            }
        }
    }
    #endregion
}
