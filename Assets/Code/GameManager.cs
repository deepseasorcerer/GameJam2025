using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameEvents;
    
    private static GameManager Instance;
    private float timeSinceLastEvent = 0;
    [SerializeField] private float timeBetweenEvents = 40f;
    [SerializeField] private float timeBetweenEventsRandomness = 10f;
    [SerializeField] private float eventAcceleration = 4f;

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
            timeSinceLastEvent = timeBetweenEvents + Random.Range(-timeBetweenEventsRandomness, timeBetweenEventsRandomness);
            timeBetweenEvents -= eventAcceleration;
            StartRandomEvent();
        }
    }
    private void StartRandomEvent()
    {
        int randomEvent = Random.Range(0, gameEvents.Length);
        Debug.Log("Starting event: " + gameEvents.Length);
        //todo: Is there a better way? -Dork
        
        gameEvents[randomEvent].TryGetComponent<AirLeakEvent>(out var airLeakEvent);
        if(airLeakEvent != null)
        {
            gameEvents[randomEvent].GetComponent<AirLeakEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<FireEvent>(out var fireEvent);
        if(fireEvent != null)
        {
            gameEvents[randomEvent].GetComponent<FireEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<PowerOutageEvent>(out var powerOutageEvent);
        if(powerOutageEvent != null)
        {
            gameEvents[randomEvent].GetComponent<PowerOutageEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<PipeBreakEvent>(out var pipeBreakEvent);
        if(pipeBreakEvent != null)
        {
            gameEvents[randomEvent].GetComponent<PipeBreakEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<RedirectShipEvent>(out var redirectShipEvent);
        if(redirectShipEvent != null)
        {
            gameEvents[randomEvent].GetComponent<RedirectShipEvent>().ActivateTask();
        }
        gameEvents[randomEvent].TryGetComponent<FuelThrustersEvent>(out var fuelThrustersEvent);
        if(fuelThrustersEvent != null)
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
