using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : CharacterMate
{
    public Canvas UICanvas;
    public HitPanel hitP; //连击风格style和连击值consistency在玩家视角中的UI呈现

    public int consistency;//风格连击值
    public string currentStyle; //当前连击风格  现有风格：Pixel，LowPoly,LoveCraft,ACG,LaiZi

    //像素机制相关
    public int pixelPotential; //中断时，会给予敌人的伤害
    public int dmgLv1 = 2;//0-2次连击,会给予敌人的伤害
    public int dmgLv2 = 3;//3-5次连击，会给予敌人的伤害
    public int dmgLv3 = 4;//6次以上
    public int dmgLv4 = 5;//9次以上

    //二次元机制相关
    public int acgPotential;//中断时,给予自己的金钱
    public int acgLv1 = 1;//0-2次连击
    public int acgLv2 = 2;//3-5次连击
    public int acgLv3 = 4;//6次以上
    public int acgLv4 = 8;//9次以上

    //LowPoly机制相关
    public int lowPolyPotential;//中断时，给予自己的恢复量
    public int lpLv1 = 1;//0-2次连击
    public int lpLv2 = 2;//3-5次连击
    public int lpLv3 = 3;//6次以上
    public int lpLv4 = 4;//9次以上


    //克苏鲁机制相关
    public int loveCraftPotential;//中断时，给予敌人的真实伤害 设定中缺失
    public int lcLv1 = 1;
    public int lcLv2 = 2;
    public int lcLv3 = 3;
    public int lcLv4 = 4;


    //public enum StyleType {Pixel};

    public override void Start()
    {
        base.Start();
        PrepareHitsUI();
    }

    public void PrepareHitsUI()
    {
        UICanvas = gM.uiCanvas;
        GameObject hitPanel = Instantiate(gM.characterM.hitsPanel, UICanvas.transform, false);
        hitP = hitPanel.GetComponent<HitPanel>();
    }


    //异术家所有卡牌都有style，并需要在每次打出后，进行连击检测
    public void StyleCheck(CardInfoArt card)
    {
        if (currentStyle == null)
        {
            consistency = 1;
            currentStyle = card.style.ToString();
            hitP.SycnConsistencyAndStyle(currentStyle, consistency);
            return;
        }

        if (card.style.ToString() == currentStyle || card.style.ToString() == "LaiZi")
        {
            consistency += 1;
        }
        else
        {
            BreakChainEffects();
            currentStyle = card.style.ToString();
            consistency = 1;
        }

        hitP.SycnConsistencyAndStyle(currentStyle, consistency);

    }

    //在己方回合结束后，进行style的连击效果结算
    public void StyleEffect()
    {
        switch (currentStyle)
        {
            //像素连击：在每回合结束时，每1点像素将给予敌人2点伤害
            case "Pixel":
                gM.enM.enemyTarget.TakeDamage(consistency * PixelCheck());
                break;
            //LowPloy连击：在每回合结束时，每1点治疗1hp
            case "LowPoly":
                HealSelf(LowPolyCheck());
                break;
            //ACG连击：在每回合结束时，每1点将给予玩家1点收入
            case "ACG":
                gold += ACGCheck();
                break;
            case "LoveCraft":
                //克苏鲁连击：在每回合结束时，每1点连击转换为对敌方的1点真实伤害
                gM.enM.enemyTarget.TakeTrueDamage(consistency * LoveCraftCheck());
                break;
        }

    }

    public void BreakChainEffects()
    {
        switch (currentStyle) 
        {
            case "Pixel":
                int x = PixelCheck();// x本身并没有作用 只是为了跑一边pixel Check进行 potential的更新 否则同一回合内（不按回合结束的按键）无法对potential进行数据同步 下面的check同理
                gM.enM.enemyTarget.TakeDamage(pixelPotential);
                break;
            case "ACG":
                int y = ACGCheck();
                Debug.Log("acg" + acgPotential);
                gold += acgPotential;
                break;
            case "LoveCraft":
                int z = LoveCraftCheck();
                gM.enM.enemyTarget.TakeTrueDamage(loveCraftPotential);
                break;
            case "LowPoly":
                int s = LowPolyCheck();
                HealSelf(lowPolyPotential);
                break;

        }

    }



    //像素自身的连击机制
    public int PixelCheck()
    {
        if(consistency > 2)
        {
            pixelPotential = 6;
            return dmgLv2;
        }
        else if(consistency > 5)
        {
            pixelPotential = 12;
            return dmgLv3;
        }
        else if(consistency > 8)
        {
            pixelPotential = 18;
            return dmgLv4;
        }
        else
        {
            pixelPotential = 0;
            return dmgLv1;
        }
    }

    public int ACGCheck()
    {
        if (consistency > 2)
        {
            acgPotential = 15;
            return acgLv2;
        }
        else if (consistency > 5)
        {
            acgPotential = 30;
            return acgLv3;
        }
        else if (consistency > 8)
        {
            acgPotential = 45;
            return acgLv4;
        }
        else
        {
            acgPotential = 0;
            return acgLv1;
        }
    }

    public int LowPolyCheck()
    {
        if (consistency > 2)
        {
            lowPolyPotential = 3;
            return lpLv2;
        }
        else if (consistency > 5)
        {
            lowPolyPotential = 6;
            return lpLv3;
        }
        else if (consistency > 8)
        {
            lowPolyPotential = 9;
            return lpLv4;
        }
        else
        {
            lowPolyPotential = 0;
            return lpLv1;
        }
    }

    public int LoveCraftCheck()
    {
        if (consistency > 2)
        {
            loveCraftPotential = 3;
            return lcLv2;
        }
        else if (consistency > 5)
        {
            
            return lcLv3;
        }
        else if (consistency > 8)
        {
            return lcLv4;
        }
        else
        {
            return lcLv1;
        }
    }



}
