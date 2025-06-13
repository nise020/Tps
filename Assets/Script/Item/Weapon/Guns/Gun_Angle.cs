using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public partial class Gun : Weapon
{
    UI_Battle ui;
    public bool reLoed = false;

    [SerializeField] float gunRazer;
    [SerializeField] bool razerOn;

    public GameObject GunHoleObj;//gunHole
    [SerializeField] GameObject gunObj;//gun
    [SerializeField] Bullet_Player bulletObj;//bullet
    [SerializeField] Transform magazine;
    [SerializeField] float gunRotSpeed = 0.0f;
    [SerializeField] GameObject razerEndObj;
    [SerializeField] bool angleOn = true;
    LineRenderer gunLazer;
    GunState State = GunState.Off;
    [SerializeField] float bulletSpeed = 10.0f;
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
    public void StateUpdate(GunState gunState) 
    {
        State = gunState;
    }
    public override void Attack(Vector3 _finalAim)
    {
        if (State != GunState.Off || bulletcount >= maxBullet) 
        {
            return;
        }

        GameObject go = bulletObjList[bulletcount];
        if (go == null) return;

        go.transform.position = GunHoleObj.transform.position;
        go.transform.rotation = Quaternion.LookRotation(_finalAim);
        go.SetActive(true);

        StartCoroutine(MoveBullet(go.transform, _finalAim, bulletSpeed, 3f));

        bulletcount++;
        nowBullet--;

        State = GunState.On;
        Invoke("AttackDelay", 0.3f);
    }
    public IEnumerator MoveBullet(Transform bullet, Vector3 direction, float speed, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            bullet.position += direction.normalized * speed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }

        bullet.gameObject.SetActive(false);
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
        
        Debug.DrawLine(GunHoleObj.transform.position, targetPos, Color.red);
        return _player.transform.localRotation;
    }

    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }
    
    
}
