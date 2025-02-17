using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvent[] gameEvents; 
    public static GameManager Instance;
    
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
        
    }

    public void startEvent(string eventName)
    {
        foreach (var gameEvent in gameEvents)
        {
            if (gameEvent.eventName == eventName)
            {
                gameEvent.isActive = true;
            }
        }
    }
}
