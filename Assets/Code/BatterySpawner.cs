using Unity.VisualScripting;
using UnityEngine;

public class BatterySpawner : InteractableBase
{
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Transform spawnPoint;
    private float timeSinceSpawn = 10f;
    [SerializeField] private float spawnInterval = 10f;
    protected override void PerformInteraction()
    {
        if(timeSinceSpawn>spawnInterval)
        {
            Instantiate(batteryPrefab, spawnPoint.position, Quaternion.identity);
            timeSinceSpawn = 0;
        }
    }

    private void Update()
    {
        timeSinceSpawn+=Time.deltaTime;
    }
}

