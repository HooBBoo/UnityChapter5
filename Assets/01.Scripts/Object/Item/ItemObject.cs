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
        if (data.canBeInteracted) // Item data���� ��ȣ�ۿ� ���� ���ο� ������Ʈ Ȯ��
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
            Destroy(gameObject); // ��ȣ�ۿ� �� ������ ����
            Debug.Log($"{data.displayName} �������� �����Ǿ����ϴ�.");
        }
    }
    public bool CanBeInteracted => canBeInteracted;
}