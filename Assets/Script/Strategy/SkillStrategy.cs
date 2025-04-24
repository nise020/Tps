using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillStrategy : Strategy
{
    int Damagevalue;
    GameObject WarriorSkillEffect1 => Resources.Load<GameObject>("Assets\\Resources\\Prefabs\\Effect\\Fab_SwordEffect 1");
    GameObject WarriorSkillEffect2 => Resources.Load<GameObject>("Assets\\Resources\\Prefabs\\Effect\\Fab_SwordEffect 2");
    GameObject GunnerSkillEffect1 => Resources.Load<GameObject>("Assets\\Resources\\Prefabs\\Effect\\Fab_SwordEffect");
    //GameObject GunnerSkillEffect2 => Resources.Load<GameObject>("Assets\\Resources\\Prefabs\\Effect\\Fab_SwordEffect");
    public override void Skill(CharactorJobEnum _type, int _skillNumber,out int _damageValue)
    {
        switch (_type)
        {
            case CharactorJobEnum.Gunner:
                Damagevalue = GunnerSkill(_skillNumber, Damagevalue);
                break;
            case CharactorJobEnum.Warrior:
                Damagevalue = WarriorSkill(_skillNumber, Damagevalue);
                break;
        }
        _damageValue = Damagevalue;
    }
    public int GunnerSkill(int _skill, int _damageValue)
    {
        if (_skill == 1)//attack skill1
        {
            DamegeUp = 60;
            //GameObject go = Instantiate(SkillEffect,Vector3.zero,Quaternion.identity);
        }
        else if (_skill == 2)//buff skill2
        {
            DamegeUp = 80;
        }
        return _damageValue = DamegeUp;
    }
    public int WarriorSkill(int _skill, int _damageValue)
    {
        if (_skill == 1)//attack skill1
        {
            DamegeUp = 10;
        }
        else if (_skill == 2)//buff skill2
        {
            DamegeUp = 20;
        }
        return _damageValue = DamegeUp;
    }
    public GameObject AddSkillObject(SkillObjType _skill) 
    {
        if (_skill == SkillObjType.Sowrd) 
        {
            return WarriorSkillEffect1;
        }
        else if (_skill == SkillObjType.Gun)
        {
            return GunnerSkillEffect1;
        }
        return null;
    }
}
