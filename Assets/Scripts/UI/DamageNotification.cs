using UnityEngine;
using TMPro;

public class DamageNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountTxt;
    [SerializeField] private Color textColor;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private Vector3 floatDirection = new Vector3(0, 1, 0);
    private float timer;

    public void Init(int amount, Color color)
    {
        amountTxt.color = color;
        amountTxt.text = amount.ToString();
        timer = 0f;
    }

    private void Update()
    {
        transform.position += floatDirection * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
        textColor = new Color(textColor.r, textColor.g, textColor.b, alpha);

        if(timer >= lifeTime)
        {
            NotificationManager.Instance.ReturnDamageNotifToPool(this);
        }
    }
}
