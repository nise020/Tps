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
    public static BattelManager BattelMgr;
    public static AtlasManager AtlasMgr;
    public static SoundMgr SoundMgr;
    public static ShakeCamera ShakeCamera;
    public static MainCamera MainCamera;
    public static TableMgr TableMgr;
    public static TableMgr InutTableMgr() 
    {
        if (TableMgr == null)
        {
            TableMgr = new TableMgr();
            TableMgr.Init();
        }
        return TableMgr;
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
