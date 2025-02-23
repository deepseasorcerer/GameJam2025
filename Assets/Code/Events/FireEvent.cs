using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEvent : InteractableBase
{
    [SerializeField] string GameEventName = "Fire";
    [SerializeField] private GameEventType eventType = GameEventType.Fire;
    [SerializeField] private List<GameObject> firePoints;
    [SerializeField] private GameObject firePrefab;
    private List<GameObject> availableFirePoints;

    public static event Action<string> FireEventChangedNarrative;

    private void Awake()
    {
        availableFirePoints = new List<GameObject>(firePoints);
        isActive = false;
    }

    public void ActivateTask()
    {
        FireEventChangedNarrative?.Invoke("How shall I put this lightly. The ship is on fire. Please put it out. Be precise, I don't want anything damaged.");

        ActivateFire();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ActivateTask();
        }
        
    }
    public void FailedTask()
    {
        isActive = false;
        SceneManager.LoadScene("DefeatScreen");
    }
    



    protected override void PerformInteraction()
    {
        throw new System.NotImplementedException();
    }

    void ActivateFire()
    {
        if (AllFirePointsFull())
        {
            EveryIsFull();
            return;
        }

        // Keep selecting a random fire point until an empty one is found
        int randomIndex;
        GameObject selectedFirePoint;

        do
        {
            randomIndex = UnityEngine.Random.Range(0, availableFirePoints.Count);
            selectedFirePoint = availableFirePoints[randomIndex];
        } while (selectedFirePoint.transform.childCount > 0);

        // Instantiate the firePrefab as a child of the selected fire point
        GameObject fireInstance = Instantiate(firePrefab, selectedFirePoint.transform.position, selectedFirePoint.transform.rotation);
        fireInstance.transform.SetParent(selectedFirePoint.transform);
        Fire fireScript = fireInstance.GetComponent<Fire>();
        if (fireScript != null)
        {
            fireScript.SetFireEvent(this);
        }

        // Check if all fire points are full
        if (AllFirePointsFull())
        {
            EveryIsFull();
            return;
        }



    }

    bool AllFirePointsFull()
    {
        foreach (GameObject firePoint in availableFirePoints)
        {
            if (firePoint.transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }

    void EveryIsFull()
    {
        Debug.Log("All fire points are full!");
        isActive = true;

    }
    
    public void DestroyedFire()
    {
        Debug.Log("Destroyed");
        isActive = false;
    }


}