// GameInputManager.cs (수정 후)
using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    public InventoryUI inventoryUI;

    void Update()
    {
        // Tab 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.ToggleInventory();
        }

        // --- 이 아래 부분을 새로 추가합니다! ---

        // Q 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 인벤토리가 열려있고, 마우스가 어떤 슬롯 위에 있을 때만
            if (inventoryUI.inventoryPanel.activeSelf && InventoryManager.Instance.hoveredSlot != null)
            {
                InventoryManager.Instance.DropItem();
            }
        }
    }
}