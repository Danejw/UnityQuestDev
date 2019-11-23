using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPracticeManager : MonoBehaviour
{
    // Variable for UI and Stats
    [HideInInspector]
    public int totalTargetsDestroyed = 0;
    [HideInInspector]
    public int round = 0;
    [HideInInspector]
    public float timeElapsed = 0;

    public GameObject targetPrefab;
    public Transform lookAtCenter;
    public GameObject UI;

    private bool isGameOn = false;

    private int amtOfTargets = 5;

    public float endGameTime = 60;

    public bool startGame = false;

    // used for second interval timer
    private float nextActionTime = 0;
    private float interval = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Event Listener that listens for a Destroyed Target
        Target.TargetDestroyed += TargetDestroyed;

        timeElapsed = 0;
        totalTargetsDestroyed = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        if (startGame)
        {
            StartGame(); 
        }

        if (isGameOn)
        { 
            // Start Timer
            AddToTime();

            // Check and collect targets into array every second interval
            if (Time.time > nextActionTime)
            {
                nextActionTime += interval;
                CollectTargets();
            }

            //  Next Round of Targets
            if (AllTargetsDestroyed())
            {
                NewRound();
            }

            // Game Over
            if (timeElapsed >= endGameTime)
            {
                EndGame();
            }    
        }
    }

    public void StartGame()
    {
        // Hide Superpower UI
        if (UI != null) { UI.SetActive(false); }
        // Init Game
        startGame = true;
        //init start game data
        round = 0;
        amtOfTargets = 4;
        totalTargetsDestroyed = 0;
        // Game On
        isGameOn = true;
        // Create Targets
        NewRound();
        startGame = false;
    }

    void NewRound()
    {
        if (isGameOn)
        {    
            AddToTargetAmt();
            round += 1;
            StartCoroutine(WaitSeconds(1));
            CreateTargets();
        }
    }

    void CreateTargets()
    {
        if (targetPrefab != null)
        {
            // create amount targets in random positions with random roational values
            for (int i = 0; i < amtOfTargets; i++)
            {
                GameObject newTarget = Instantiate(targetPrefab, GetRandomPosition(), Quaternion.identity, lookAtCenter);
                newTarget.GetComponent<Rotate>().pivotPoint = lookAtCenter;
                newTarget.GetComponent<Rotate>().rotationSpeed = Random.Range(-10, 10);
                newTarget.transform.LookAt(lookAtCenter);
            }
        }
    }

    void AddToTime()
    {
        timeElapsed += 1 * Time.deltaTime;
        //print("Time Elapsed: " + timeElapsed);
    }

    void AddToTargetAmt()
    {
        amtOfTargets += 1;
    }

    // listens for the OnTargetDesrtoyed event, then this method is triggered
    void TargetDestroyed()
    {
        totalTargetsDestroyed += 1;
    }

    // Check if all targets are destroyed
    bool AllTargetsDestroyed()
    {
        if (CollectTargets() == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DestroyAllTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targets)
        {
            Destroy(target);
        }
    }

    public void EndGame()
    {
        if (isGameOn)
        {
            //Destroy All Targets
            DestroyAllTargets();

            // Reset timer
            timeElapsed = 0;

            // Display Superpower UI
            if (UI != null) { UI.SetActive(true); }

            // GameOver
            isGameOn = false;
        }
    }


    // Utility Methods
    Vector3 GetRandomPosition()
    {  
        Vector3 randomPos = new Vector3( Random.Range(-15, 15), Random.Range(-1, 20), Random.Range(-15, 15));
        return randomPos;
    }


    //Collects all objects with tag "Target" and add them to a list
    private int CollectTargets()
    {
        GameObject[] targetArray = GameObject.FindGameObjectsWithTag("Target");
        return targetArray.Length;
    }

    IEnumerator WaitSeconds(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
