using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;  //ray�� �󸶳� ���� �� ��
    private float lastCheckTime;     //ray�� ���������� �� �ð�
    public float maxCheckDistance;   //�󸶳� �ָ��ִ� ���� üũ����
    public LayerMask layerMask;

    public GameObject curInteractGameObject; //���ͷ��� �� ���� ������Ʈ�� ����
    private IInteractable curInteractable;
    private IConsumable curConsumable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                var consumable = hit.collider.GetComponent<IConsumable>();

                if (interactable != null && interactable != curInteractable)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = interactable;
                    curConsumable = null; // ���� �Һ� ���� ������ �ʱ�ȭ
                    SetPromptText();
                }
                else if (consumable != null && consumable != curConsumable)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curConsumable = consumable;
                    curInteractable = null; // ���� ��ȣ�ۿ� ������ �ʱ�ȭ
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                curConsumable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        if (curInteractable != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        else if (curConsumable != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curConsumable.GetConsumePrompt();
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    public void OnConsumeInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curConsumable != null)
        {
            curConsumable.OnConsume();
            curConsumable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}