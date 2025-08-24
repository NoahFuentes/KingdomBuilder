using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI notificationTextBox;
    [SerializeField] private float popUpTime;
    [SerializeField] private float fadeDuration;

    [SerializeField] private GameObject resourceGatherPopUpObject;
    [SerializeField] private RectTransform resourceNotificationsParent;
    [SerializeField] private float spacing;
    [SerializeField] private float moveDuration;
    private List<RectTransform> activeResourceNotifications = new List<RectTransform>();

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

    public void ShowResourceNotification(Sprite icon, int amount)
    {
        GameObject notif = Instantiate(resourceGatherPopUpObject, resourceNotificationsParent);
        RectTransform notifRectTrans = notif.GetComponent<RectTransform>();

        notif.GetComponent<ResourceNotification>().Init(icon, amount);

        notifRectTrans.anchoredPosition = Vector2.zero;

        for(int i = 0; i < activeResourceNotifications.Count; i++)
        {
            RectTransform r = activeResourceNotifications[i];
            Vector2 target = r.anchoredPosition + new Vector2(0, spacing);
            StartCoroutine(MoveActiveResourceNotifsUp(r, target));
        }   
        activeResourceNotifications.Insert(0, notifRectTrans);
    }

    private IEnumerator MoveActiveResourceNotifsUp(RectTransform rectTrans, Vector2 target)
    {
        Vector2 start = rectTrans.anchoredPosition;
        float t = 0;
        while(t < 1f)
        {
            if (null == rectTrans) yield break;
            t += Time.deltaTime / moveDuration;
            rectTrans.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }
        if(null != rectTrans)
            rectTrans.anchoredPosition = target;
    }

    public void RemoveResourceNotification(RectTransform rectTrans)
    {
        activeResourceNotifications.Remove(rectTrans);
        Destroy(rectTrans.gameObject);
    }


}
