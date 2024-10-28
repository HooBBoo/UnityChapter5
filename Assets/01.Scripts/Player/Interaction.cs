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

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));  //���߾ӿ��� ����� ��ũ�� �ʺ�,������ ��
            RaycastHit hit;  //�ε��� ������Ʈ

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                var interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null && interactable != curInteractable)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = interactable;
                    SetPromptText();
                }
            }

            else //�� ������ ���� �� ��
            {
                curInteractGameObject = null;
                curInteractable = null;
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
}