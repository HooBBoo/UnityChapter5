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

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));  //정중앙에서 쏘려고 스크린 너비,높이의 반
            RaycastHit hit;  //부딪힌 오브젝트

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

            else //빈 공간에 레이 쏠 때
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