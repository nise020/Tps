using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gun : MonoBehaviour
{
    [Header("���� ����,�Ѿ�")]
    public int bullet;
    public int RelodingBullet;
    float ChargeingTime;
    float ChargeingTimer = 0.0f;
    float RerodingTime = 3.0f;
    float RerodingTimer = 0.0f;
    //[SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    public void GunBulletType()//���� ����
    {
        if (GunEnumType == GunTags.MG)
        {
            bullet = 300;
            ChargeingTime = 0.1f;
        }
        else if (GunEnumType == GunTags.SMG)
        {
            bullet = 30;
            ChargeingTime = 0.2f;
        }
        else if (GunEnumType == GunTags.SR)
        {
            bullet = 5;
            ChargeingTime = 3.0f;
        }
        //RerodingBullet = bullet;
    }
    public void Reloding()
    {
        if (bullet == RelodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime)
        {
            bullet = RelodingBullet;
            RerodingTimer = 0.0f;
            //BulletCount.text = bullet.ToString();//ã���� ���ٰ� ��

            Debug.Log("Reroding off");
        }
    }
    public void Charging()//�ѱ� �׼�,(���ε� �ƴ�!)
    {
        GunHole.SetActive(false);
        ChargeingTimer += Time.deltaTime;
        if (ChargeingTimer > ChargeingTime)//���콺 ������ ���� �ʿ�
        {
            if (GunEnumType == GunTags.SR && Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("SR");
                GunHole.SetActive(true);
                ChargeingTimer = 0.0f;
            }
            else
            {
                Debug.Log("Not SR");
                GunHole.SetActive(true);
                ChargeingTimer = 0.0f;
            }
        }
    }
}
