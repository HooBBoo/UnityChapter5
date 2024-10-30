using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;  //ray를 얼마나 자주 쏠 지
    private float lastCheckTime;     //ray를 마지막으로 쏜 시간
    public float maxCheckDistance;   //얼마나 멀리있는 것을 체크할지
    public LayerMask layerMask;

    public GameObject curInteractGameObject; //인터렉션 된 게임 오브젝트의 정보
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
                    curConsumable = null; // 기존 소비 가능 아이템 초기화
                    SetPromptText();
                }
                else if (consumable != null && consumable != curConsumable)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curConsumable = consumable;
                    curInteractable = null; // 기존 상호작용 아이템 초기화
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