using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ProBuilder.Shapes;
using UnityEngine.U2D;//아틀라스
using UnityEngine.UI;

public partial class AtlasManager : MonoBehaviour
{
    
    [NonReorderable]
    Dictionary<string,SpriteAtlas> DicSpritAtlas = new Dictionary<string,SpriteAtlas>();
    public List<Image> UiImag = new List<Image>();
    public List<string> AtlasName;
    public List<string> DamageSpritName;
    public List<string> SpritName;
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

        for (int iNum = 0; iNum < SpritName.Count; iNum++)
        {
            UiImag[iNum].overrideSprite = GetSpritAtlas($"{AtlasName[iNum]}", $"{SpritName[iNum]}");
            Debug.Log($"Image Object Name = {AtlasName[iNum]},{SpritName[iNum]}");
        }

        gameObject.AddComponent<SpriteRenderer>();

        //GetSpritAtlas(string _Atlas, string _name)
    }
    //private void Start()
    //{
    //    //MySprite();
    //    //GetSpritAtlas("Common", "ak-47");
    //    //GetSpritAtlas("Damage", "Number1 7x10");
    //}
    public List<GameObject> AtlasLoad(List<GameObject> _imageObjects, AtlasType _atlasType) 
    {
        for (int iNum = 0; iNum < AtlasName.Count; iNum++) 
        {
            if (AtlasName[iNum].ToString() == _atlasType.ToString()) 
            {
                switch (_atlasType)
                {
                    case AtlasType.Damage:
                        _imageObjects = LoadDamageImage(_imageObjects, iNum);
                        break;
                }
            }
        }

        return _imageObjects;
    }

    private List<GameObject> LoadDamageImage(List<GameObject> _imageObjects,int _number)
    {
        if (_number >= AtlasName.Count)
        {
            Debug.LogError($"AtlasName에 {_number} 인덱스가 존재하지 않습니다.");
            return null;
        }

        if (_imageObjects.Count != DamageSpritName.Count)
        {
            Debug.LogError("이미지 오브젝트 수와 DamageSpritName 수가 일치하지 않습니다.");
            return null;
        }


        string value = AtlasName[_number];
        for (int iNum = 0; iNum < DamageSpritName.Count; iNum++)
        {
            Image Image = _imageObjects[iNum].GetComponent<Image>();
            if (Image != null)
            {
                Sprite sprite = GetSpritAtlas(value, DamageSpritName[iNum]);
                Debug.Log($"Image Object Name = {Image},{sprite}");

                Image.overrideSprite = sprite;

            }
            else
            {
                Debug.LogWarning($"{_imageObjects[iNum].name} 오브젝트에 Image 컴포넌트가 없습니다.");
            }
        };
        return _imageObjects;
    }
}
