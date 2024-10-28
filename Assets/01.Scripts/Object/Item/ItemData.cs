using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Misc,
    Consumable,
    //Equipable 고민중
}

public enum ConsumableType
{
    Health,
    MentalHealth
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Interaction")]
    public bool canBeInteracted; // 상호작용 가능 여부 설정
    public string interactPrompt;

    //[Header("Equip")]
    //public GameObject equipPrefab;
}
