// Item.cs (최종 수정본)
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    // 아이템을 주웠을 때 호출될 함수
    public void PickUp()
    {
        // 인벤토리에 아이템 추가를 요청하고
        InventoryManager.Instance.AddItem(itemData);
        // 자신은 사라집니다.
        Destroy(gameObject);
    }
}