using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody; //une reférence du script au gameObject "Player"
    float xRotation = 0f;

    // Ajout des variables pour la gestion des sticks de la manette
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public float joystickSensitivity = 100f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Gestion des mouvements du joystick
        float joystickX = Input.GetAxis("RightHorizontal") * joystickSensitivity * Time.deltaTime;
        float joystickY = Input.GetAxis("RightVertical") * joystickSensitivity * Time.deltaTime;

        //limite la rotation sur l'axe Y
        xRotation -= mouseY;//à chaque frame on baisse xRotation à partir de mouseY
        xRotation -= joystickY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f)
;
        //rotation du joueur
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
