using UnityEngine;
using System.Collections;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI notificationTextBox;
    [SerializeField] private float popUpTime;
    [SerializeField] private float fadeDuration;

    private void Awake()
    {
        Instance = this;
        notificationTextBox.gameObject.SetActive(false);
    }


    public void Notify(string text, Color txtColor)
    {
        notificationTextBox.text = text;
        notificationTextBox.color = txtColor;
        StopAllCoroutines();
        StartCoroutine(PopUp());
    }

    private IEnumerator PopUp()
    {
        notificationTextBox.alpha = 1f;
        notificationTextBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(popUpTime);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            notificationTextBox.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        notificationTextBox.gameObject.SetActive(false);
    }
}
