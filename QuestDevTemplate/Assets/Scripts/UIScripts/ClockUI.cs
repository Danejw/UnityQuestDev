using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{
    public TargetPracticeManager targetManager;
    public GameObject clockText;
    private int timeLeft;

    private void Start()
    {
        if(targetManager == null) { Debug.Log("TargetPracticeManager is Null"); };
        if (clockText == null) { Debug.Log("Clock Text is Null"); };
    }

    private void Update()
    {
        CountDown();
        clockText.GetComponent<TMP_Text>().text = timeLeft.ToString();
    }

    void CountDown()
    {
        timeLeft = (int)targetManager.endGameTime - (int)Mathf.Round(targetManager.timeElapsed);
    }
}
