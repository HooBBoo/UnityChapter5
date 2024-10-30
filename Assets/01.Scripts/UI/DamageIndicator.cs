using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    void Start()
    {
        CharacterManager.Instance.player.condition.onTakeDamage += Flash;
    }

    public void Flash()
    {
        if (coroutine != null) // 중복 되지 않게 기존의 것 꺼주기
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 105f / 255f, 143f / 255f);
        coroutine = StartCoroutine(FadeAway());

    }
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 105f / 255f, 143f / 255f, a);
            yield return null;
        } 
        
        image.enabled = false;
    }
}
