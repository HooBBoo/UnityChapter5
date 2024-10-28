using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
    bool CanBeInteracted { get; }
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    public bool canBeInteracted;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        if (data.canBeInteracted) // Item data에서 상호작용 가능 여부와 프롬프트 확인
        {
            str += $"\n{data.interactPrompt}";
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
            Debug.Log($"{data.displayName} 아이템이 삭제되었습니다.");
        }
    }
    public bool CanBeInteracted => canBeInteracted;
}