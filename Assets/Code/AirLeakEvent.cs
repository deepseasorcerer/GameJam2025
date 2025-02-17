using UnityEngine;

[System.Serializable]
public class AirLeakEvent : GameEvent
{
    public float timeLeft;
    
    
    void Start()
    {
        timeLeft = eventLength;
    }
    
    void Update()
    {
        if (isActive)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                FailedTask();
            }
        }
    }
    
    private void FailedTask()
    {
        isActive = false;
        Debug.Log("Air leak event failed");
    }

    public void ActivateTask()
    {
        isActive = true;
        timeLeft = eventLength;
    }
}
