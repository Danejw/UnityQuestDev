using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TargetPracticeManager targetManager;
    public GameObject roundText;
    public GameObject targetsDestroyedText;

    private void Start()
    {
        if (targetManager == null) { Debug.Log("TargetPracticeManager is Null"); };
        if (roundText == null) { Debug.Log("Round Text is Null"); };
        if (targetsDestroyedText == null) { Debug.Log("Target Destoyed Text is Null"); };
    }

    private void Update()
    {
        roundText.GetComponent<TMP_Text>().text = targetManager.round.ToString();
        targetsDestroyedText.GetComponent<TMP_Text>().text = targetManager.totalTargetsDestroyed.ToString();
    }
}
