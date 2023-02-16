using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Vector2 _mouseSensitivity;
    [SerializeField] private Vector2 _padSensitivity;
    [SerializeField] private Vector2 _mouseYLimit;

    private float _horizontal;
    private float _vertical;

    private Transform _cameraTransform;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity.y * Time.deltaTime;

        float gamePadX = Input.GetAxis("RightHorizontal") * _padSensitivity.x * Time.deltaTime;
        float gamePadY = Input.GetAxis("RightVertical") * _padSensitivity.y * Time.deltaTime;

        _horizontal += mouseX + gamePadX;
        _vertical += mouseY + gamePadY;

        _vertical = Mathf.Clamp(_vertical, _mouseYLimit.x, _mouseYLimit.y);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _horizontal, transform.eulerAngles.z);
        _cameraTransform.eulerAngles = new Vector3(_vertical, _cameraTransform.eulerAngles.y, _cameraTransform.eulerAngles.z);
    }
}
