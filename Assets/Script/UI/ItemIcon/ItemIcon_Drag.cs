using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public partial class ItemIcon : MonoBehaviour
{
    private Vector2 localMousePos;
    private RectTransform rectTransform;
    private RectTransform beforRectTransform;
    private Canvas canvas;
    private GameObject IconSwapImg;
    //IPointerDownHandler, IPointerUpHandler, IDragHandler

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //if (EventSystem.current.IsPointerOverGameObject(Mouse.current.deviceId))
        //{
        //    Debug.Log("�� Input �ý��ۿ��� ���콺�� UI ���� ����");
        //}
    }
    public void OnDown(BaseEventData eventData)//����
    {
        Shared.UiManager.UI_INVENTORY.OnDown(eventData,this,itemData);
    }

    public void OnDrag(BaseEventData eventData)//���� ��������
    {
        Shared.UiManager.UI_INVENTORY.OnDrag(eventData);

    }
    public void OnUp(BaseEventData eventData)//��
    {
        Shared.UiManager.UI_INVENTORY.EndDrag(eventData,this, itemData);

    }

    public void OnDrop(BaseEventData eventData)
    {

    }

    public void UpdateData(ItemData _data)
    {
        itemData = _data;
        // ������ �̹���, ���� �� ������Ʈ
    }
}
