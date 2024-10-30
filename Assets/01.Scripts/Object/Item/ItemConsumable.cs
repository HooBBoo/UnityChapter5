using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    string GetConsumePrompt();
    void OnConsume();
}

public class ItemConsumable : MonoBehaviour, IConsumable
{
    public ItemData data;

    public string GetConsumePrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if (data.Consumable)
        {
            str += $"\n\n{data.ConsumablePrompt}";
        }
        return str;
    }

    public void OnConsume()
    {
        if (data.Consumable && data.type == ItemType.Consumable)
        {
            CharacterManager.Instance.player.condition.IncreaseMentalHealth(data.consumable.value); // ���� �ǰ� ����
            Debug.Log("ddd");
        }
    }
}