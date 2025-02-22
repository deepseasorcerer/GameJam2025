using System.Collections.Generic;
using UnityEngine;

public class FireEvent : InteractableBase
{
    [SerializeField] string GameEventName = "Fire";
    [SerializeField] private GameEventType eventType = GameEventType.Fire;
    [SerializeField] private List<GameObject> firePoints;
    [SerializeField] private GameObject firePrefab;

    private List<GameObject> availableFirePoints;

    

    private void Awake()
    {
        availableFirePoints = new List<GameObject>(firePoints);
        isActive = false;
    }

    public void ActivateTask()
    {
        Debug.Log("Fire has started");
        ActivateFire();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ActivateTask();
        }
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
            randomIndex = Random.Range(0, availableFirePoints.Count);
            selectedFirePoint = availableFirePoints[randomIndex];
        } while (selectedFirePoint.transform.childCount > 0);

        // Instantiate the firePrefab as a child of the selected fire point
        GameObject fireInstance = Instantiate(firePrefab, selectedFirePoint.transform.position, Quaternion.identity);
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