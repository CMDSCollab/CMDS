using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Error
{
    public string errorName;
    public DebugType debug;
}

public class Programmer : BasicCharacter
{
    [SerializeField] private Canvas UIParent;
    [SerializeField] private ProDebugUI debugUIPrefab;
    [SerializeField] private List<Error> potentialErrors;

    public const int MAX_ERROR_COUNT = 6; 
    public List<Error> currentErrors { get; private set; }
    [HideInInspector] public int codeRedundancy = 0;
    private ProDebugUI debugUI;

    public override void OnNewGameStarted()
    {
        base.OnNewGameStarted();

        InitCharacter();

        if (gM.characterM.chosenCharacter == "Programmer")
        {
            CreateDebugUI();
        }
    }

    private void InitCharacter()
    {
        if (currentErrors == null)
        {
            currentErrors = new List<Error>();
        }

        currentErrors.Clear();
        codeRedundancy = 0;
    }

    private void CreateDebugUI()
    {
        debugUI = Instantiate(debugUIPrefab, UIParent.transform, false);
        debugUI.SetUp(this);
        debugUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(-600, 250, 0);
        debugUI.transform.SetAsFirstSibling();
    }

    public void AddRedundantCode(int value)
    {
        codeRedundancy += value;
        if (codeRedundancy < 0)
        {
            codeRedundancy = 0;
        }
    }

    public override void OnPlayerTurnEnded()
    {
        base.OnNewGameStarted();

        CheckRedundantCode();
    }

    private void CheckRedundantCode()
    {
        if (codeRedundancy > 0)
        {
            bool hasNewError = false;

            // codeRedundancy ÿ��1�㣬����10%�ĸ�����غϲ���1���µ�Error ��5ʱ��41%��10ʱ��65.2%��
            for (int i = 0; i < codeRedundancy; i++)
            {
                if (UnityEngine.Random.Range(0, 10) == 0)
                {
                    hasNewError = true;
                }
            }

            if (hasNewError)
            {
                GenerateNewError();
            }
        }
    }

    private void GenerateNewError()
    {
        if (currentErrors.Count >= MAX_ERROR_COUNT)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, potentialErrors.Count);
        currentErrors.Add(potentialErrors[randomIndex]);
    }

    public void CheckCardDebug(DebugType debugType)
    {
        foreach (Error error in currentErrors)
        {
            if (debugType == error.debug)
            {
                RemoveError(error);
            }
        }
        //foreach (Error error in currentErrors)
        //{
        //    if (card.Equals(error.fix))
        //    {
        //        RemoveError(error);
        //    }
        //}
    }

    private void RemoveError(Error error)
    {
        if (currentErrors.Contains(error))
        {
            currentErrors.Remove(error);
        }
    }

}