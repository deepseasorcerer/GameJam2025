using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameEvents;
    private InteractableBase[] gameEventsInteractable;
    private static GameManager Instance;
    private float timeSinceLastEvent = 30f;
    [SerializeField] private float timeBetweenEvents = 40f;
    [SerializeField] private float timeBetweenEventsRandomness = 10f;
    [SerializeField] private float minTimeBetweenEvents = 25f; 
    [SerializeField] private float eventAcceleration = 4f;
    
    [SerializeField] public float GameLength = 300f;
    [HideInInspector] public float timeElapsed = 0f;
    private float timeSinceCheckedEvents = 0f;
    private MusicManager _musicManager;

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
        gameEventsInteractable = gameEvents.Select(x => x.GetComponent<InteractableBase>()).ToArray();
    }
    
    private void Start()
    {
        _musicManager = MusicManager.Instance;
        if (_musicManager == null)
        {
            Debug.LogError("No MusicManager found in scene");
        }
    }
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= GameLength)
        {
            SceneManager.LoadScene("VictoryScreen");
        }
        timeSinceLastEvent += Time.deltaTime;
        if(timeSinceLastEvent >= timeBetweenEvents)
        {
            timeSinceLastEvent = 0;
            timeBetweenEvents -= eventAcceleration;
            timeBetweenEvents += Random.Range(-timeBetweenEventsRandomness, timeBetweenEventsRandomness);
            if(timeBetweenEvents < minTimeBetweenEvents)
            {
                timeBetweenEvents = minTimeBetweenEvents;
            }
            StartRandomEvent();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartRandomEvent();
        }
        
        
        timeSinceCheckedEvents += Time.deltaTime;
        if(timeSinceCheckedEvents >= 2f)
        {
            timeSinceCheckedEvents = 0f;
            var activeGECount = gameEventsInteractable.Count(x => x.isActive);
            if(activeGECount == 0)
            {
                _musicManager.SetMusicIntensity(MusicManager.MusicIntensity.Low);
            }
            if(activeGECount >= 1)
            {
                _musicManager.SetMusicIntensity(MusicManager.MusicIntensity.High);
            }
        }
    }
    private void StartRandomEvent()
    {
        var nonActiveGe = gameEventsInteractable.Where(x=>x.isActive == false).ToList();
        if(nonActiveGe.Count == 0)
        {
            //Debug.Log("No more events to start");
            return;
        }
        int eventIndex = Random.Range(0, nonActiveGe.Count);
        nonActiveGe[eventIndex].TryGetComponent<AirLeakEvent>(out var airLeakEvent);
        if(airLeakEvent is not null && airLeakEvent.isActive == false)
        {
            nonActiveGe[eventIndex].GetComponent<AirLeakEvent>().ActivateTask();
        }
        nonActiveGe[eventIndex].TryGetComponent<FireEvent>(out var fireEvent);
        if(fireEvent is not null)
        {
            nonActiveGe[eventIndex].GetComponent<FireEvent>().ActivateTask();
        }
        nonActiveGe[eventIndex].TryGetComponent<PowerOutageEvent>(out var powerOutageEvent);
        if(powerOutageEvent is not null)
        {
            nonActiveGe[eventIndex].GetComponent<PowerOutageEvent>().ActivateTask();
        }
        nonActiveGe[eventIndex].TryGetComponent<PipeBreakEvent>(out var pipeBreakEvent);
        if(pipeBreakEvent is not null)
        {
            nonActiveGe[eventIndex].GetComponent<PipeBreakEvent>().ActivateTask();
        }
        nonActiveGe[eventIndex].TryGetComponent<FuelThrustersEvent>(out var fuelThrustersEvent);
        if(fuelThrustersEvent)
        {
            nonActiveGe[eventIndex].GetComponent<FuelThrustersEvent>().ActivateTask();
        }
    }
}

public enum GameEventType
{
    AirLeak,
    Fire,
    PowerOutage,
    PipeBreak,
    FuelThrusters
}
