// HotbarUI.cs (새로 생성)
using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    // 각 슬롯은 상호작용이 필요 없으므로 간단하게 GameObject로 관리
    public GameObject[] hotbarSlots;

    void Start()
    {
        // 인벤토리 데이터가 변경될 때마다 핫바 UI도 업데이트하도록 함수를 등록
        InventoryManager.Instance.onInventoryChanged += UpdateHotbarUI;
    }

    // 핫바 UI를 업데이트하는 함수
    void UpdateHotbarUI()
    {
        // 인벤토리의 아이템 목록을 가져옴
        InventoryItem[] items = InventoryManager.Instance.items;

        // 핫바 슬롯 개수만큼 반복
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            // 해당하는 인벤토리 데이터 슬롯을 가져옴
            InventoryItem itemInSlot = (i < items.Length) ? items[i] : null;

            // 핫바 슬롯에 붙어있는 InventorySlot 스크립트를 가져옴
            InventorySlot slotScript = hotbarSlots[i].GetComponent<InventorySlot>();

            // 데이터가 있으면 아이템을 그리고, 없으면 슬롯을 비움
            if (itemInSlot != null)
            {
                slotScript.SetItem(itemInSlot);
            }
            else
            {
                slotScript.ClearSlot();
            }
        }
    }
}