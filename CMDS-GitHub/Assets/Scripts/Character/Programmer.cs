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

public class Programmer : CharacterMate
{
    private Canvas UIParent;
    private List<Error> potentialErrors;
    public const int MAX_ERROR_COUNT = 6;
    public List<Error> currentErrors { get; private set; }
    [HideInInspector] public int codeRedundancy = 0;
    private ProDebugUI debugUI;

    public override void OnNewGameStarted()
    {
        base.OnNewGameStarted();

        InitCharacter();

        if (gM.characterM.mainCharacterType == CharacterType.Programmmer)
        {
            CreateDebugUI();
        }
    }

    private void InitCharacter()
    {
        UIParent = gM.uiCanvas;
        potentialErrors = gM.characterM.potentialErrors;

        if (currentErrors == null)
        {
            currentErrors = new List<Error>();
        }

        currentErrors.Clear();
        codeRedundancy = 0;
    }

    private void CreateDebugUI()
    {
        debugUI = Instantiate(gM.characterM.debugUIPrefab, UIParent.transform, false);
        debugUI.SetUp(this);
        debugUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(650, 250, 0);
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

            // codeRedundancy 每有1点，就有10%的概率这回合产生1个新的Error （5时有41%，10时有65.2%）
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
        for (int i = (currentErrors.Count - 1); i >= 0; i--)
        {
            if (debugType == currentErrors[i].debug)
            {
                RemoveError(currentErrors[i]);
            }
        }
    }

    private void RemoveError(Error error)
    {
        if (currentErrors.Contains(error))
        {
            currentErrors.Remove(error);
        }
    }

}
