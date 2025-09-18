// InventorySlot.cs (드래그 앤 드롭 기능이 추가된 최종본)
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// 4개의 드래그 관련 인터페이스를 추가로 상속받습니다.
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemIcon;
    public TextMeshProUGUI itemCountText;
    public int slotIndex; // 슬롯의 순번 (UI 업데이트 시 채워줌)

    public InventoryItem CurrentItem { get; private set; }

    // --- 기존 함수들 (SetItem, ClearSlot, OnPointerEnter, OnPointerExit)은 그대로 둡니다. ---
    #region 기존 함수들
    public void SetItem(InventoryItem item)
    {
        CurrentItem = item;
        itemIcon.sprite = CurrentItem.data.icon;
        itemIcon.color = new Color(1, 1, 1, 1);
        itemIcon.enabled = true;

        if (CurrentItem.quantity > 1)
        {
            itemCountText.text = CurrentItem.quantity.ToString();
            itemCountText.enabled = true;
        }
        else
        {
            itemCountText.text = "";
            itemCountText.enabled = false;
        }
    }

    public void ClearSlot()
    {
        CurrentItem = null;
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        itemIcon.color = new Color(1, 1, 1, 0);
        itemCountText.text = "";
        itemCountText.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 슬롯이 비어있든 아니든, 마우스가 올라오면 무조건 자신을 hoveredSlot으로 설정합니다.
        InventoryManager.Instance.hoveredSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.hoveredSlot = null;
    }
    #endregion

    // --- 드래그 앤 드롭을 위한 4개의 함수를 새로 추가합니다! ---

    // 1. 드래그를 시작했을 때 한번 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 빈 슬롯은 드래그 불가
        if (CurrentItem == null) return;

        // InventoryManager에 드래그 시작 슬롯 정보 저장
        InventoryManager.Instance.originalSlot = this;

        // 마우스를 따라다닐 아이콘 설정
        InventoryManager.Instance.dragIcon.sprite = CurrentItem.data.icon;
        InventoryManager.Instance.dragIcon.gameObject.SetActive(true);

        // 원래 슬롯의 아이콘은 잠시 투명하게
        itemIcon.color = new Color(1, 1, 1, 0);
        itemCountText.enabled = false;
    }

    // 2. 드래그하는 동안 계속 호출
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 아이콘이 마우스 위치를 따라다니도록 함
        InventoryManager.Instance.dragIcon.transform.position = eventData.position;
    }

    // 3. 드래그를 마쳤을 때 한번 호출 (드롭 성공 여부와 무관)
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 아이콘을 숨기고, 원본 슬롯 정보 초기화
        InventoryManager.Instance.dragIcon.gameObject.SetActive(false);
        InventoryManager.Instance.originalSlot = null;

        // UI를 새로고침해서 모든 슬롯의 아이콘을 다시 제대로 표시
        InventoryManager.Instance.onInventoryChanged?.Invoke();
    }

    // 4. 다른 슬롯에서 드래그한 아이템을 내 슬롯 위에서 드롭했을 때 호출
    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot originalSlot = InventoryManager.Instance.originalSlot;

        // 드래그 시작 슬롯이 있고, 그게 자기 자신이 아닐 때
        if (originalSlot != null && originalSlot != this)
        {
            // InventoryManager에게 아이템 위치를 바꿔달라고 요청
            InventoryManager.Instance.SwapItems(originalSlot.slotIndex, this.slotIndex);
        }
    }

    

}