// InventoryItem.cs (새 파일의 전체 내용)

[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    public InventoryItem(ItemData itemData, int amount)
    {
        data = itemData;
        quantity = amount;
    }
}