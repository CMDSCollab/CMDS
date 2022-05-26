using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct StyleImage { public string style; public Sprite styleIcon; }

public class HitPanel : MonoBehaviour
{
    public Image hitImage;
    public Text hitIndexText;
    public string currentStyle;
    public List<StyleImage> StyleImages;
    public Dictionary<string, Sprite> styleImageDic;

    private void Awake()
    {
        MakeImageDic();
    }

    public void MakeImageDic()
    {
        styleImageDic = new Dictionary<string, Sprite>();
        foreach(StyleImage styleImg in StyleImages)
        {
            styleImageDic.Add(styleImg.style, styleImg.styleIcon);
        }
    }

    public void SycnConsistencyAndStyle(string style,int consistency)
    {
        if(style != currentStyle)
        {
            ChangeStyleImage(style);
        }

        hitIndexText.text = consistency.ToString();
    }

    public void ChangeStyleImage(string style)
    {
        hitImage.sprite = styleImageDic[style];
    }

}
