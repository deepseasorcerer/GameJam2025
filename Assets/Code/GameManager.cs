using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameEvents;
    
    private static GameManager Instance;
    private float timeSinceLastEvent = 30f;
    [SerializeField] private float timeBetweenEvents = 40f;
    [SerializeField] private float timeBetweenEventsRandomness = 10f;
    [SerializeField] private float eventAcceleration = 4f;
    
    [SerializeField] private float GameLength = 300f;
    private float timeElapsed = 0f;

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
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= GameLength)
        {
            Debug.Log("Winner!");
        }
        timeSinceLastEvent += Time.deltaTime;
        if(timeSinceLastEvent >= timeBetweenEvents)
        {
            timeSinceLastEvent = timeBetweenEvents + Random.Range(-timeBetweenEventsRandomness, timeBetweenEventsRandomness);
            timeBetweenEvents -= eventAcceleration;
            StartRandomEvent();
        }
    }
    private void StartRandomEvent()
    {
        int randomEvent = Random.Range(0, gameEvents.Length);
        
        //todo: Is there a better way? -Dork (either way not called much);
        gameEvents[randomEvent].TryGetComponent<AirLeakEvent>(out var airLeakEvent);
        if(airLeakEvent is not null)
        {
            gameEvents[randomEvent].GetComponent<AirLeakEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<FireEvent>(out var fireEvent);
        if(fireEvent is not null)
        {
            gameEvents[randomEvent].GetComponent<FireEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<PowerOutageEvent>(out var powerOutageEvent);
        if(powerOutageEvent is not null)
        {
            gameEvents[randomEvent].GetComponent<PowerOutageEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<PipeBreakEvent>(out var pipeBreakEvent);
        if(pipeBreakEvent is not null)
        {
            gameEvents[randomEvent].GetComponent<PipeBreakEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<RedirectShipEvent>(out var redirectShipEvent);
        if(redirectShipEvent is not null)
        {
            gameEvents[randomEvent].GetComponent<RedirectShipEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<FuelThrustersEvent>(out var fuelThrustersEvent);
        if(fuelThrustersEvent)
        {
            gameEvents[randomEvent].GetComponent<FuelThrustersEvent>().ActivateTask();
        }
        
    }
    
}

public enum GameEventType
{
    AirLeak,
    Fire,
    PowerOutage,
    PipeBreak,
    RedirectShip,
    FuelThrusters
}
