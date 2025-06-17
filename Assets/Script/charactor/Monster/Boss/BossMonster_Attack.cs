using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BossMonster : Monster
{

    protected override void AttackAnimation(Weapon _weapon, GameObject _weaponObj)
    {
        _weapon.gameObject.SetActive(true);
        _weapon.WeaponReset();

        granaidAttack(weaponHandObject.transform.position, targetTrs.position, _weaponObj);
    }
    

    private IEnumerator CheckSlashHit()
    {
        float slashRange = 5f;         
        float slashAngle = 90f;          
        Transform weaponOrigin = MainWeaponObj.transform;

        HashSet<Transform> damagedEnemies = new HashSet<Transform>();

        //List<Monster> monsetrPos = Shared.MonsterManager.MonsterList;
        List<GameObject> monsetrPos = Shared.BattelManager.LoadToCharcterList(ObjectType.Player);
        isChecking = true;

        while (isChecking) 
        {

            for (int iNum = 0; iNum < monsetrPos.Count; iNum++)
            {
                if (monsetrPos[iNum] == null || damagedEnemies.Contains(monsetrPos[iNum].transform)) continue;

                Vector3 toTarget = monsetrPos[iNum].transform.position - weaponOrigin.position; // �� ������ �� ���� ����
                toTarget.y = 0f;

                float distance = toTarget.magnitude;

                if (distance > slashRange) continue; // ������ ��� ���ʹ� ����

                float angle = Vector3.Angle(weaponOrigin.forward, toTarget.normalized);

                if (angle < slashAngle / 2f)
                {
                    damagedEnemies.Add(monsetrPos[iNum].transform);

                    Character character = monsetrPos[iNum].gameObject.GetComponent<Character>();
                    Shared.BattelManager.DamageCheck(this, character, MAINWEAPON);
                }
            }
            yield return null;
            Debug.Log("�ڷ�ƾ ���� - ���� ���� ��");
        }
    }

    IEnumerator CheckThrustHit()
    {
        float thrustRange = 3.0f;           // ��� ���� �Ÿ�
        float thrustRadius = 0.5f;          // ��� �β� (���� �ֺ� ��� ����)
        Transform origin = SubWeaponObj.transform; // ��� ������ (�� �� �Ǵ� �� ��ġ)

        HashSet<Transform> damagedEnemies = new HashSet<Transform>();
        List<GameObject> monsetrPos = Shared.BattelManager.LoadToCharcterList(ObjectType.Player);
        isChecking = true;
        while (isChecking)
        {
            for (int iNum = 0; iNum < monsetrPos.Count; iNum++)
            {
                if (monsetrPos[iNum] == null || damagedEnemies.Contains(monsetrPos[iNum].transform)) continue;

                Vector3 toTarget = monsetrPos[iNum].gameObject.transform.position - origin.position;

                float forwardDist = Vector3.Dot(origin.forward, toTarget);

                if (forwardDist < 0f || forwardDist > thrustRange) continue;

                Vector3 closestPointOnLine = origin.position + origin.forward * forwardDist;
                float perpendicularDist = Vector3.Distance(monsetrPos[iNum].gameObject.transform.position, closestPointOnLine);

                if (perpendicularDist > thrustRadius) continue;

                damagedEnemies.Add(monsetrPos[iNum].transform);

                Character character = monsetrPos[iNum].gameObject.GetComponent<Character>();
                Shared.BattelManager.DamageCheck(this, character, MAINWEAPON);
            }

            yield return null;
        }

        Debug.Log("Thrust �ڷ�ƾ ����");
    }
}
