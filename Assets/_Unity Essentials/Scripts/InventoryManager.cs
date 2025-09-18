// InventoryManager.cs (배열 구조로 리팩토링한 최종본)
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;


    [HideInInspector]
    public InventoryItem[] items;
    public int inventoryCapacity = 9;
    public System.Action onInventoryChanged;

    [Header("아이템 버리기 설정")]
    public InventorySlot hoveredSlot;
    public Transform playerTransform;

    [Header("드래그 앤 드롭 설정")]
    public Image dragIcon;
    public InventorySlot originalSlot;

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

        // 배열을 생성합니다.
        items = new InventoryItem[inventoryCapacity];

        // [추가된 부분] 모든 슬롯을 강제로 null로 확실하게 비워줍니다.
        // 유니티 에디터의 '유령 데이터'를 무시하고 배열을 깨끗한 상태로 만듭니다.
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }

        Debug.Log($"인벤토리 시스템 초기화 완료. 용량(Capacity): {items.Length}. 모든 슬롯을 null로 초기화했습니다.");
    }

    public void AddItem(ItemData itemData)
    {
        // [디버그] 함수가 호출되었는지, 배열 크기는 몇인지 확인합니다.
        Debug.Log($"AddItem 함수 호출됨: {itemData.itemName}. 현재 인벤토리 배열 크기: {items.Length}");

        // 1. 스택 가능한 아이템 찾기
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].data == itemData && items[i].quantity < itemData.stackSize)
            {
                items[i].quantity++;
                onInventoryChanged?.Invoke();
                return;
            }
        }

        // 2. 비어있는 첫 번째 슬롯 찾기
        for (int i = 0; i < items.Length; i++)
        {
            // [디버그] 각 슬롯의 상태를 하나하나 확인합니다.
            Debug.Log($"슬롯 {i} 확인 중... 비어있나? --> {items[i] == null}");

            if (items[i] == null)
            {
                items[i] = new InventoryItem(itemData, 1);
                Debug.Log($"성공: 슬롯 {i}에 아이템 추가 완료."); // [디버그] 성공 메시지
                onInventoryChanged?.Invoke();
                return;
            }
        }

        // [디버그] 빈 칸을 못 찾고 여기까지 왔다면 이 메시지가 뜹니다.
        Debug.LogWarning("빈 슬롯을 찾지 못했습니다. '인벤토리 가득 참' 메시지를 출력합니다.");
        Debug.Log("인벤토리가 가득 찼습니다!");
    }

    // [변경점] DropItem 로직을 배열에 맞게 수정
    public void DropItem()
    {
        int slotIndex = hoveredSlot.slotIndex;
        if (hoveredSlot == null || items[slotIndex] == null) return;

        Instantiate(items[slotIndex].data.itemPrefab,
                    playerTransform.position + playerTransform.forward * 1.5f + (Vector3.up * 0.5f),
                    Quaternion.identity);

        items[slotIndex].quantity--;

        if (items[slotIndex].quantity <= 0)
        {
            // [변경점] Remove 대신 null로 설정하여 칸을 비움
            items[slotIndex] = null;
        }

        onInventoryChanged?.Invoke();
    }

    // [변경점] SwapItems 로직을 배열에 맞게 훨씬 간단하게 수정
    public void SwapItems(int indexA, int indexB)
    {
        // indexA와 indexB의 내용을 그냥 맞바꿈 (null이어도 상관없음)
        InventoryItem temp = items[indexA];
        items[indexA] = items[indexB];
        items[indexB] = temp;

        onInventoryChanged?.Invoke();
    }

    // RefreshUI 함수는 이제 InventoryUI가 담당하므로 여기서 삭제하거나,
    // InventoryUI에서 이 함수를 호출하는 방식으로 유지해도 됩니다.
    // 여기서는 삭제하고 InventoryUI가 직접 처리하도록 하겠습니다.
}