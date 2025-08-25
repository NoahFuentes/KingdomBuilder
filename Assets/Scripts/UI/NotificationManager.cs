using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    //Screen text notifications
    [SerializeField] private TextMeshProUGUI notificationTextBox;
    [SerializeField] private float popUpTime;
    [SerializeField] private float fadeDuration;

    //resource gathering notifications
    [SerializeField] private GameObject resourceGatherPopUpObject;
    [SerializeField] private RectTransform resourceNotificationsParent;
    [SerializeField] private float spacing;
    [SerializeField] private float moveDuration;
    private List<RectTransform> activeResourceNotifications = new List<RectTransform>();

    //damage notifications
    [SerializeField] private Transform dmgNotifParent;
    [SerializeField] private DamageNotification damageNotification;
    [SerializeField] int poolSize;
    private Queue<DamageNotification> damageNotificationsPool = new Queue<DamageNotification>();
    [SerializeField] private GameObject damageFlash;
    [SerializeField] private float flashDuration;

    private void Awake()
    {
        Instance = this;
        notificationTextBox.gameObject.SetActive(false);

        //populate damage notif pool
        for (int i = 0; i < poolSize; i++)
        {
            DamageNotification dn = Instantiate(damageNotification, dmgNotifParent);
            dn.gameObject.SetActive(false);
            damageNotificationsPool.Enqueue(dn);
        }
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

    public void ShowDamageNotification(Vector3 worldPos, int amount, Color color)
    {
        DamageNotification dn = damageNotificationsPool.Count > 0 ? damageNotificationsPool.Dequeue() : Instantiate(damageNotification, dmgNotifParent);
        dn.transform.position = worldPos;
        dn.transform.rotation = Camera.main.transform.rotation;
        dn.gameObject.SetActive(true);
        dn.Init(amount, color);
    }

    public void ReturnDamageNotifToPool(DamageNotification dn)
    {
        dn.gameObject.SetActive(false);
        damageNotificationsPool.Enqueue(dn);
    }

    public void FlashScreenRed()
    {
        StartCoroutine(FlashScreenRedCoroutine()); 
    }
    private IEnumerator FlashScreenRedCoroutine()
    {
        damageFlash.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        damageFlash.SetActive(false);

    }

}
