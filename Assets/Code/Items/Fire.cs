using UnityEngine;
using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour
{
    public float extinguishRate = 0.0001f;
    private Vector3 initialScale; // Store the initial scale of the parent
    private bool canReduce = false;
    [SerializeField] private float eventDuration = 50f;
    float timeLeft;
    FireEvent fireEvent;
    [SerializeField] AudioSource fireSound;

    private void Start()
    {
        timeLeft = eventDuration;
        initialScale = transform.localScale; // Get the initial scale of the parent
        fireSound.Play();
    }

    public void SetFireEvent(FireEvent fireEvent)
    {
        this.fireEvent = fireEvent;
    }


    void Update()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            FailedTask();
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hello");
        if (other.CompareTag("ExtinguisherFoam"))
        {
            Debug.Log("Hellp2");
            ReduceFire();
        }
    }

    private void ReduceFire()
    {
        Debug.Log("ReduceFire called!");
        initialScale.y = initialScale.y - extinguishRate;
        initialScale.x = initialScale.x - extinguishRate;
        initialScale.z = initialScale.z - extinguishRate;
        float heightChange = transform.localScale.y - initialScale.y;
        transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - heightChange / 2.0f, transform.position.z);

        if (initialScale.y <= 0.15f)
        {
            fireEvent?.DestroyedFire();
            Destroy(gameObject);
        }
    }

    private void FailedTask()
    {
        SceneManager.LoadScene("DefeatScreen");
    }
}