using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if (data.canBeInteracted) // Item data에서 상호작용 가능 여부와 프롬프트 확인
        {
            str += $"\n\n{data.interactPrompt}";
        }
        return str;
    }

    public void OnInteract()
    {
        if (data.canBeInteracted)
        {
            CharacterManager.Instance.player.itemData = data;
            CharacterManager.Instance.player.addItem?.Invoke();
            Destroy(gameObject); // 상호작용 후 아이템 삭제
        }
    }
}