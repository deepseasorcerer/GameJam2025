using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BatterySpawner : InteractableBase
{
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Transform spawnPoint;
    private float timeSinceSpawn = 5f;
    [SerializeField] private float spawnInterval = 5f;
    
    private SoundManager _soundManager;
    private void Awake()
    {
    }

    private void Start()
    {
        _soundManager = SoundManager.Instance;
        if (_soundManager == null)
        {
            Debug.LogError("No SoundManager found in scene");
        }
    }
    
    protected override void PerformInteraction()
    {
        if(timeSinceSpawn>spawnInterval)
        {
            //animator.SetTrigger("Get_Bat");
            _soundManager.PlaySound("Thoop");
            Instantiate(batteryPrefab, spawnPoint.position, Quaternion.identity);
            timeSinceSpawn = 0;
        }
;
    }

    private void Update()
    {
        timeSinceSpawn+=Time.deltaTime;
    }
}

