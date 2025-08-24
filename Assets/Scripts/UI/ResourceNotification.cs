using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ResourceNotification : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountTxt;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float lifeTime;
    [SerializeField] private float fadeDuration;

    public void Init(Sprite resIcon, int amount)
    {
        icon.sprite = resIcon;
        amountTxt.text = "+" + amount.ToString();
        StartCoroutine(FadeAndRemove());
    }

    private IEnumerator FadeAndRemove()
    {
        yield return new WaitForSeconds(lifeTime);

        float t = 0;
        while(t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            canvasGroup.alpha = 1f - t;
            yield return null;
        }
        NotificationManager.Instance.RemoveResourceNotification(GetComponent<RectTransform>());
    }
}
