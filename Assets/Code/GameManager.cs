using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameEvents;
    public static GameManager Instance;
    private float timeSinceLastEvent = 0;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameManager in the scene");
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        
    }
    
    void Update()
    {
        timeSinceLastEvent += Time.deltaTime;
        if(timeSinceLastEvent >= 4f)
        {
            timeSinceLastEvent = -1000000000f;
            startEvent("AirLeak");
        }
    }

    public void startEvent(string eventName)
    {
        foreach (var gameEvent in gameEvents)
        {
            if (gameEvent.name == eventName)
            {
                gameEvent.GetComponent<AirLeakEvent>().ActivateTask();
            }
        }
    }
}
