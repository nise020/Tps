using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InventoryManager : MonoBehaviour
{
    public DragState currentState = DragState.None;
    private ItemIcon draggingIcon;
    private GameObject draggingVisual;

    public void BeginDrag(ItemIcon icon)
    {
        ItemData date = icon.LoadData();
        if (date.itemID == 0) return;

        currentState = DragState.Dragging;
        draggingIcon = icon;

        // �巡�� �� ����ٴ� �ð��� �̹��� ����
        draggingVisual = Instantiate(icon.gameObject, transform);
        draggingVisual.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void UpdateDrag(Vector2 screenPos)
    {
        if (draggingVisual != null)
            draggingVisual.transform.position = screenPos;
    }

    public void EndDrag(ItemIcon targetIcon)
    {
        if (currentState != DragState.Dragging)
            return;

        if (targetIcon != draggingIcon)
        {
            // ������ ��ü
            SwapIcons(draggingIcon, targetIcon);
        }

        // �巡�� ���� �ʱ�ȭ
        Destroy(draggingVisual);
        draggingVisual = null;
        draggingIcon = null;
        currentState = DragState.None;
    }

    private void SwapIcons(ItemIcon a, ItemIcon b)
    {
        ItemData temp = a.LoadData();
        a.ItemDataCheck(b.LoadData());
        b.ItemDataCheck(temp);
    }

}
