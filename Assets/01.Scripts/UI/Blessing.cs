using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blessing : MonoBehaviour
{
    public Image image;
    public float flashSpeed;
    private Coroutine coroutine;

    void Start()
    {
        CharacterManager.Instance.player.condition.onApplyBlessing += Bless;
    }

    public void Bless()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(0f, 1f, 0.3f);
        coroutine = StartCoroutine(FadeAway());
    }

        private IEnumerator FadeAway()
        {
            float startAlpha = 0.5f;
            float a = startAlpha;

            while (a > 0)
            {
                a -= (startAlpha / flashSpeed) * Time.deltaTime;
                image.color = new Color(0f, 1f, 0.3f, a);
                yield return null;
            }

            image.enabled = false;
        }
}