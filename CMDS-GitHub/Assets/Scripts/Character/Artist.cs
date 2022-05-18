using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : CharacterMate
{
    public int consistency;//�������ֵ
    public string currentStyle; //��ǰ�������
    public int income;//����

    //���ػ������
    public int pixelPotential; //�ж�ʱ���������˵��˺�
    public int dmgLv1 = 2;//0-2������,�������˵��˺�
    public int dmgLv2 = 3;//3-5���������������˵��˺�
    public int dmgLv3 = 4;//6������
    public int dmgLv4 = 5;//9������

    //����Ԫ�������
    public int acgPotential;//�ж�ʱ,�����Լ��Ľ�Ǯ
    public int acgLv1 = 1;//0-2������
    public int acgLv2 = 2;//3-5������
    public int acgLv3 = 4;//6������
    public int acgLv4 = 8;//9������

    //LowPoly�������
    public int lowPolyPotential;//�ж�ʱ�������Լ��Ļָ���
    public int lpLv1 = 1;//0-2������
    public int lpLv2 = 2;//3-5������
    public int lpLv3 = 3;//6������
    public int lpLv4 = 4;//9������



    //public enum StyleType {Pixel};




    //���������п��ƶ���style������Ҫ��ÿ�δ���󣬽����������
    public void StyleCheck(CardInfoArt card)
    {
        if (currentStyle == null)
        {
            consistency = 1;
            currentStyle = card.style.ToString();
            return;
        }
        if (card.style.ToString() == currentStyle || card.style.ToString() == "LaiZi")
        {
            consistency += 1;
        }
        else
        {
            consistency = 1;
            currentStyle = card.style.ToString();
        }
    }

    //�ڼ����غϽ����󣬽���style������Ч������
    public void StyleEffect()
    {
        switch (currentStyle)
        {
            //������������ÿ�غϽ���ʱ��ÿ1�����ؽ��������2���˺�
            case "Pixel":
                gM.enM.enemyTarget.TakeDamage(consistency * PixelCheck());
                break;
            //LowPloy��������ÿ�غϽ���ʱ��ÿ1������1hp
            case "LowPoly":
                HealSelf(LowPolyCheck());
                break;
            //ACG��������ÿ�غϽ���ʱ��ÿ1�㽫�������1������
            case "ACG":
                income += ACGCheck();
                break;
            case "LoveCraft":
            //����³��������ÿ�غϽ���ʱ��ÿ1������ת��Ϊ�Եз���1����ʵ�˺�
                break;
        }

    }


    //�����������������
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



}
