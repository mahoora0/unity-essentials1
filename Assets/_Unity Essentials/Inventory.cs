// Inventory.cs (대규모 수정 후)
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    // 인벤토리가 변경될 때 UI를 업데이트하기 위한 이벤트
    public System.Action onInventoryChanged;

    // [중요] List<ItemData> 에서 List<InventoryItem> 으로 변경!
    public List<InventoryItem> items = new List<InventoryItem>();

    public int inventoryCapacity = 20; // 인벤토리 최대 칸 수

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 아이템 추가 로직 (완전히 새로 작성)
    public void AddItem(ItemData itemData)
    {
        // 1. 이미 아이템이 있고, 최대 수량 미만인 슬롯이 있는지 검색
        for (int i = 0; i < items.Count; i++)
        {
            // 만약 같은 종류의 아이템이고, 아직 꽉 차지 않았다면
            if (items[i].data == itemData && items[i].quantity < itemData.stackSize)
            {
                items[i].quantity++; // 수량만 1 증가시키고
                onInventoryChanged?.Invoke(); // UI 업데이트 신호 보내기
                Debug.Log($"{itemData.itemName}의 수량이 1 증가했습니다.");
                return; // 함수 종료
            }
        }

        // 2. 1번에서 쌓을 곳을 못 찾았다면, 새 슬롯이 있는지 확인
        if (items.Count < inventoryCapacity)
        {
            // 새 슬롯에 아이템 추가
            items.Add(new InventoryItem(itemData, 1));
            onInventoryChanged?.Invoke(); // UI 업데이트 신호 보내기
            Debug.Log($"{itemData.itemName}을(를) 새로 획득했습니다.");
        }
        else
        {
            // 3. 인벤토리가 가득 찼다면
            Debug.Log("인벤토리가 가득 찼습니다!");
        }
    }
}