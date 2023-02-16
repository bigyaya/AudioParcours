using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    //private float horizontal;
    //private float vertical;
    public float mouseSensitivity = 100f;
    //public float padSensitivity = 100f;

    public Transform playerBody; //une reférence du script au gameObject "Player"

    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //récupère les axes X et Y; Time.deltaTime = Il s'agit du temps écoulé depuis la dernière fois que nous avons bougé le personnage
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //float gamePadX = Input.GetAxis("RightHorizontal") * padSensitivity * Time.deltaTime;
        //float gamePadY = Input.GetAxis("RightVertical") * padSensitivity * Time.deltaTime;

        //horizontal += gamePadX;
        //horizontal += gamePadY;

        //limite la rotation sur l'axe Y
        xRotation -= mouseY;//à chaque frame on baisse xRotation à partir de mouseY
        xRotation = Mathf.Clamp(xRotation, -90f, 90f)
;
        //rotation du joueur
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
