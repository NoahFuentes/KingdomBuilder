using UnityEngine;
using TMPro;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance { get; private set; }

    [SerializeField] private float realSecondsPerGameMinute = 1f;
    public float currentTimeOfDayMinutes = 0f;

    public float startOfWorkDayTime = 360f; //6am
    public float endOfWorkDayTime = 1320f; //10pm

    public bool companionsHaveBeenSentHome = false;
    public bool companionsHaveBeenSentToWork = false;

    [SerializeField] private TextMeshProUGUI clockTxt;

    
    private string GetClockText()
    {
        int totalMinutes = Mathf.FloorToInt(currentTimeOfDayMinutes);
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        string ampm = (hours >= 12) ? "PM" : "AM";

        // Convert 24h ? 12h
        int displayHour = hours % 12;
        if (displayHour == 0)
            displayHour = 12;

        return $"{displayHour}:{minutes:00} {ampm}";
    }

    private void endOfDay()
    {
        companionsHaveBeenSentHome = false;
        companionsHaveBeenSentToWork = false;
    }

    //UNITY FUNCTIONS
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        currentTimeOfDayMinutes += (Time.deltaTime / realSecondsPerGameMinute);

        if (currentTimeOfDayMinutes >= 1440)
        {
            currentTimeOfDayMinutes = 0f;
            endOfDay();
        }

        if (!companionsHaveBeenSentHome && currentTimeOfDayMinutes >= endOfWorkDayTime)
        {
            CompanionManager.Instance.SendAllCompanionsHome();
            companionsHaveBeenSentHome = true;
        }
        if (!companionsHaveBeenSentToWork && currentTimeOfDayMinutes >= startOfWorkDayTime)
        {
            CompanionManager.Instance.WakeAllCompanionsUp();
            companionsHaveBeenSentToWork = true;
        }

        clockTxt.text = GetClockText();
    }
}
