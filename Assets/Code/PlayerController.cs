using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float lookSpeed = 400f;
    [SerializeField] private float lookXLimit = 45f;

    private float rotationX = 0;
    private Vector3 _direction;
    private CharacterController _controller;
    private float _yVelocity;
    
    private Camera playerCamera;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera = Camera.main;

        // Load mouse sensitivity
        lookSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 5f) * 80f; // Scale for better control
        Debug.Log($"Mouse Sensitivity Applied: {lookSpeed}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = Input.GetAxis("Vertical");
        float curSpeedY = Input.GetAxis("Horizontal");
        //float movementDirectionY = _direction.y;
        _direction = (forward * curSpeedX) + (right * curSpeedY);

        
        #region rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime, 0);
        #endregion

        if (_direction.magnitude > 1)
        {
            _direction.Normalize();    
        }
        _controller.Move(_direction * (speed * Time.deltaTime));
    }
}
