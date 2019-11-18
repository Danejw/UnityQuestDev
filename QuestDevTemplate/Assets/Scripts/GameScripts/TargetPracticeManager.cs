using UnityEngine;

public class TargetPracticeManager : MonoBehaviour
{
    // Variable for UI and Stats
    [HideInInspector]
    public int targetsDestroyed = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        // Event Listener that listens for a Destroyed Target
        Target.TargetDestroyed += TargetDestroyed;

        timeElapsed = 0;
        targetsDestroyed = 0;
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
            
            //  Next Round of Targets
            if (AllTargetsDestroyed())
            {
                NewRound();
            }

            // Game Over
            if(timeElapsed >= endGameTime)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        // Hide Superpower UI
        if (UI != null) { UI.SetActive(false); }
        // Game On
        isGameOn = true;
        //reset Round #
        round = 0;
        // reset Total Targets Destroyed
        totalTargetsDestroyed = 0;
        // Create Targets
        NewRound();
        startGame = false;
    }

    void NewRound()
    {
        if (isGameOn)
        {
            AddToTargetAmt();
            ResetTargetsDestroyed();
            round += 1;
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
        targetsDestroyed += 1;
    }

    // Check if all targets are destroyed
    bool AllTargetsDestroyed()
    {
        if (targetsDestroyed >= amtOfTargets)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ResetTargetsDestroyed()
    {
        targetsDestroyed = 0;
    }

    void DestroyAllTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targets)
        {
            Destroy(target);
        }
    }

    void EndGame()
    {
        // GameOver
        isGameOn = false;

        //Destroy All Targets
        DestroyAllTargets();

        // Display Superpower UI
        if(UI != null) { UI.SetActive(true); }
        
        //Display Stats

    }


    // Utility Methods
    Vector3 GetRandomPosition()
    {  
        Vector3 randomPos = new Vector3( Random.Range(-35, 35), Random.Range(-1, 40), Random.Range(-35, 35
));
        return randomPos;
    }
}
