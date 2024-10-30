using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blessing : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI blessingText;
    public float flashSpeed;
    private Coroutine coroutine;

    void Start()
    {
        CharacterManager.Instance.player.condition.onApplyBlessing += Bless;
        blessingText.gameObject.SetActive(false);
    }

    public void Bless()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(171f / 255f, 241f / 255f, 163f / 255f); // �ʱ� ���� ����
        coroutine = StartCoroutine(FadeAway());

        blessingText.gameObject.SetActive(true);
        blessingText.text = "��� ���� ����:)";
        StartCoroutine(DisableText(60));
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(171f / 255f, 241f / 255f, 163f / 255f, a);
            yield return null;
        }

        image.enabled = false;
    }

    private IEnumerator DisableText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        blessingText.gameObject.SetActive(false); // 60�� �� ������� �ؽ�Ʈ ��Ȱ��ȭ
    }
}
