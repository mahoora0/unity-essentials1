// InventoryUI.cs (최종 수정본)
using UnityEngine;
using StarterAssets;

public class InventoryUI : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject inventoryPanel;
    public Transform slotHolder;

    private InventorySlot[] slots;
    private StarterAssetsInputs starterAssetsInputs;

    void Start()
    {
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        slots = slotHolder.GetComponentsInChildren<InventorySlot>();

        // InventoryManager의 신호를 받도록 정확히 연결합니다.
        InventoryManager.Instance.onInventoryChanged += UpdateUI;

        inventoryPanel.SetActive(false);
    }

    // (이 아래 코드는 수정할 필요 없습니다)
    public void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);
        ToggleCursor(isActive);
    }

    private void ToggleCursor(bool isInventoryOpen)
    {
        if (isInventoryOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            starterAssetsInputs.cursorInputForLook = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            starterAssetsInputs.cursorInputForLook = true;
        }
    }

    public void UpdateUI()
    {

        Debug.LogWarning($"[디버깅] UI 슬롯 배열 크기 (slots.Length): {slots.Length}");
        Debug.LogWarning($"[디버깅] 데이터 배열 크기 (items.Length): {InventoryManager.Instance.items.Length}");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;

            // [변경점] items.Count 대신, 배열의 해당 인덱스가 null이 아닌지 확인
            if (InventoryManager.Instance.items[i] != null)
            {
                slots[i].SetItem(InventoryManager.Instance.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}