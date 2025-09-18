// ItemData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public GameObject itemPrefab;

    [Header("스태킹 정보")]
    [Tooltip("한 슬롯에 쌓을 수 있는 최대 아이템 개수")]
    public int stackSize = 16; // 예: 포션은 16개까지, 칼은 1개까지
}