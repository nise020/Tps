using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : Charactor
{
    protected virtual void MobAttackTimecheck() 
    {
        Patterntimer += Time.deltaTime;
        int number = 0;
        if (Patterntimer >  Patternltime) 
        {
            if (NumberOn == false)
            {
                number = Random.Range(0, 1);
                NumberOn = true;
            }
            MobAttackTimer(number);
        }
        
    }
    protected void MobAttackTimer(int number) 
    {
        if (number == 0) 
        {
            //nomalAttack();
        }
        else if (number == 1) 
        {
            //GrenadeAttack();
        }
        //yield return null;
    }

    protected virtual void targetOn(ref int _value,List<GameObject>_listObj) 
    {
        int count = _listObj.Count;//������ �÷��̾� ����
        _value = Random.Range(0, count);//�������� Ÿ�� ��ȣ ����
    }

    public void Dead() 
    {
        //BATTELMANAGER.Mincount -= 1;
    }
}
