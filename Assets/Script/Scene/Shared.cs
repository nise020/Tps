using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shared //�޸�
{
    //���� �⤲�ų� �Ҷ� ���� ������ ����ϱ� ���ؼ� 
    //MonoBehaviour�� ������� �ʴ´�
    //�̱���,static<--�������� �����ϴ� ����,���ſ� �޸𸮸� ����ϱ� ���ؼ� ���
    public static GameManager GameManager;
    public static SceneMgr SceneMgr;
    public static BattelManager BattelManager;
    public static AtlasManager AtlasManager;
    public static SoundManager SoundManager;
    public static ShakeCamera ShakeCamera;
    public static MainCamera MainCamera;
    public static TableManager TableManager;
    public static BattelUI BattelUI;
    public static FaidInOut FaidInOut;

    public static TableManager InutTableMgr() 
    {
        if (TableManager == null)
        {
            TableManager = new TableManager();
            TableManager.Init();
        }
        return TableManager;
    }
    //private void Awake()//����
    //{
    //    shared.BattelMgr = this;
    //}

    //32bit 2�� 32�� ������ ó��
    //64bit 2�� 64�� ������ ó��
    //static�� �ִ��� ������� �ʴ°� ����
    //�ÿ�Ǵ� �޸𸮰� �󸶳� �Ǵ��� �𸣱� ������ ���ſ����׷�

    //����Ƽ,���־� ��Ʃ����� �������� - win main<--������ ��ǻ�Ϳ��� ����

    //������ �Ŵ��� �ȿ� ������ ���� �ϴ°� ���� �� �Ÿ���

}
