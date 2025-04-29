using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Weapon
{
    UI_Battle ui;
    public bool reLoed = false;

    [SerializeField] float gunRazer;
    [SerializeField] bool razerOn;

    public GameObject gunHoleObj;//gunHole
    [SerializeField] GameObject gunObj;//gun
    [SerializeField] Bullet_Player bulletObj;//bullet
    [SerializeField] Transform magazine;
    [SerializeField] float gunRotSpeed = 0.0f;
    [SerializeField] GameObject razerEndObj;
    [SerializeField] bool angleOn = true;
    LineRenderer gunLazer;
    GunState State = GunState.Off;
    public void reloed()
    {
        for (int iNum = 0; iNum < bulletData.Count; iNum++) 
        {
            if (bulletData[iNum].activeSelf)
            {
                bulletData[iNum].SetActive(false);
            }
            else { return; }
        }
    }

    public override void Attack(Vector3 _pos)
    {
        if (State == GunState.Off)
        {
            GameObject go = bulletData[bulletcount];
            if (bulletData[bulletcount] == null) 
            {
                return;
            }
            go.transform.position = gunHoleObj.transform.position;
            Bullet_Player plBullet = go.GetComponent<Bullet_Player>();
            //plBullet.targetPos = gameObject.transform.position;
            //수정 필요
            plBullet.WeaponTrs = _pos;
            
            bulletcount++;
            nowbullet--;

            State = GunState.On;

            Invoke("AttackDelay", 0.3f);

            go.SetActive(true);

            StartCoroutine(HideObject(go, 3f));
        }
    }
    private void AttackDelay() 
    {
        State = GunState.Off;
    }
    private IEnumerator HideObject(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }

    public Quaternion AimGun(GameObject _player,Vector3 _hitPos)//Aim 오브젝트를 기준으로 바꿔야함
    {
        Vector3 targetPos = _hitPos;
        Vector3 distanse = (targetPos - gunObj.transform.position);
        Quaternion startRot = Quaternion.LookRotation(gunObj.transform.forward);
        Quaternion endRot = Quaternion.LookRotation(distanse.normalized);
        _player.transform.localRotation = Quaternion.Lerp(startRot, endRot, gunRotSpeed);//* Time.deltaTime

        // 상하 각도 제한
        //Vector3 eulerRotation = gunObj.transform.eulerAngles;
        //eulerRotation.x = clampAngle(eulerRotation.x, -45f, 45f);
        //eulerRotation.z = 0f;
        //gunObj.transform.eulerAngles = eulerRotation;
        
        Debug.DrawLine(gunHoleObj.transform.position, targetPos, Color.red);
        return _player.transform.localRotation;
    }

    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }
    
    
}
