using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ProBuilder.Shapes;
using UnityEngine.U2D;//��Ʋ��
using UnityEngine.UI;

public partial class AtlasManager : MonoBehaviour
{
    
    [NonReorderable]
    Dictionary<string,SpriteAtlas> DicSpritAtlas = new Dictionary<string,SpriteAtlas>();
    public List<Image> UiImag = new List<Image>();
    public List<string> AtlasName;//���̺� �ε�
    public List<string> DamageSpritName;//�̹��� �̸� �ε�
    public List<string> ItemSpritName;//�̹��� �̸� �ε�
    public UnityEngine.Sprite GetSpritAtlas(string _Atlas, string _name) 
    {
        if (DicSpritAtlas.ContainsKey(_Atlas))
            return DicSpritAtlas[_Atlas].GetSprite(_name);

        UnityEngine.Object obj = null;

        obj = Resources.Load("Atlas/"+ _Atlas);

        if (obj == null) 
        {
            Debug.Log("load fail" + _Atlas); 
            return null;
        }

        SpriteAtlas sa = obj as SpriteAtlas;

        if (sa != null) 
        {
            DicSpritAtlas.Add(_Atlas, sa);
            return sa.GetSprite(_name);
        }

        return null;
    }

    private void Awake()
    {
        if (Shared.AtlasManager == null)
        {
            Shared.AtlasManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void MySprite() 
    {
        //SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        for (int iNum = 0; iNum < ItemSpritName.Count; iNum++)
        {
            UiImag[iNum].overrideSprite = GetSpritAtlas($"{AtlasName[iNum]}", $"{ItemSpritName[iNum]}");
            Debug.Log($"Image Object Name = {AtlasName[iNum]},{ItemSpritName[iNum]}");
        }

        gameObject.AddComponent<SpriteRenderer>();

    }

    public List<GameObject> AtlasLoad_List(List<GameObject> _imageObjects, AtlasType _atlasType) 
    {
        for (int iNum = 0; iNum < AtlasName.Count; iNum++) 
        {
            if (AtlasName[iNum].ToString() == _atlasType.ToString()) 
            {
                switch (_atlasType)
                {
                    case AtlasType.Damage:
                        _imageObjects = loadImages_List(_imageObjects, iNum, DamageSpritName);
                        break;
                }
            }
        }

        return _imageObjects;
    }

    public Dictionary< string, Sprite> AtlasLoad_Dictionary(AtlasType _atlasType) 
    {
        Dictionary<string, Sprite> imageObjects = new Dictionary<string, Sprite>();

        for (int iNum = 0; iNum < AtlasName.Count; iNum++)
        {
            if (AtlasName[iNum].ToString() == _atlasType.ToString())
            {
                switch (_atlasType)
                {
                    case AtlasType.Item:
                        imageObjects = LoadImages_Dictionary(ItemSpritName,iNum);
                        break;
                }
            }
        }

        return imageObjects;
    }
    public Dictionary<string, Sprite> LoadImages_Dictionary(List<string> _spriteName,int _number) 
    {
        Dictionary<string, Sprite> datas = new Dictionary<string, Sprite>();

        for (int iNum = 0; iNum < _spriteName.Count; iNum++) 
        {
            string atlasName = AtlasName[_number];
            Sprite sprite = GetSpritAtlas(atlasName, _spriteName[iNum]);
            datas.Add(_spriteName[iNum], sprite);

        };
        return datas;
    }

    private List<GameObject> loadImages_List(List<GameObject> _imageObjects,
        int _number, List<string> _spriteName)
    {
        if (_number >= AtlasName.Count)
        {
            Debug.LogError($"AtlasName�� {_number} �ε����� �������� �ʽ��ϴ�.");
            return null;
        }

        if (_imageObjects.Count != _spriteName.Count)
        {
            Debug.LogError($"�̹��� ������Ʈ ���� {_spriteName} ���� ��ġ���� �ʽ��ϴ�.");
            return null;
        }


        string atlasName = AtlasName[_number];
        for (int iNum = 0; iNum < _spriteName.Count; iNum++)
        {
            Image Image = _imageObjects[iNum].GetComponent<Image>();
            if (Image != null)
            {
                Sprite sprite = GetSpritAtlas(atlasName, _spriteName[iNum]);
                //Debug.Log($"Image Object Name = {Image},{sprite}");

                Image.overrideSprite = sprite;
                Image.gameObject.SetActive(false);

            }
            else
            {
                Debug.LogWarning($"{_imageObjects[iNum].name} ������Ʈ�� Image ������Ʈ�� �����ϴ�.");
            }
        };
        return _imageObjects;
    }
    //public Image AtlasLoad(Image image, AtlasType _atlasType)
    //{
    //    for (int iNum = 0; iNum < AtlasName.Count; iNum++)
    //    {
    //        if (AtlasName[iNum].ToString() == _atlasType.ToString())
    //        {
    //            switch (_atlasType)
    //            {
    //                case AtlasType.Damage:
    //                    image = LoadDamageImage(_imageObjects, iNum);
    //                    break;
    //            }
    //        }
    //    }

    //    return image;
    //}


    //private Image LoadDamageImage(Image _imageObject)
    //{
    //    if (_number >= AtlasName.Count)
    //    {
    //        Debug.LogError($"AtlasName�� {_number} �ε����� �������� �ʽ��ϴ�.");
    //        return null;
    //    }

    //    if (_imageObject.Count != DamageSpritName.Count)
    //    {
    //        Debug.LogError("�̹��� ������Ʈ ���� DamageSpritName ���� ��ġ���� �ʽ��ϴ�.");
    //        return null;
    //    }


    //    string value = AtlasName[_number];
    //    for (int iNum = 0; iNum < DamageSpritName.Count; iNum++)
    //    {
    //        Image Image = _imageObject[iNum].GetComponent<Image>();
    //        if (Image != null)
    //        {
    //            Sprite sprite = GetSpritAtlas(value, DamageSpritName[iNum]);
    //            //Debug.Log($"Image Object Name = {Image},{sprite}");

    //            Image.overrideSprite = sprite;
    //            Image.gameObject.SetActive(false);

    //        }
    //        else
    //        {
    //            Debug.LogWarning($"{_imageObject[iNum].name} ������Ʈ�� Image ������Ʈ�� �����ϴ�.");
    //        }
    //    }
    //    ;
    //    return _imageObject;
    //}
}
