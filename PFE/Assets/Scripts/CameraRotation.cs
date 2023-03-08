using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	public GameObject player;
    // Sensibilité de la souris
    public float mouseSensitivity = 100f;

    // Rotation en hauteur de la caméra
    float xRotation = 0f;
    public bool canRotate = false;

    void Start()
    {
        // Bloque le curseur dans la fenêtre de jeu et le cache
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(PossManager.Instance.possessionState == PossManager.PossessionState.Free)
        {
            CameraRot();
        }
        if(PossManager.Instance.possessionState == PossManager.PossessionState.InPossession)
        {
            
        }
      
    }

    public void CameraRot()
    {
        // Récupère les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Tourne la caméra en fonction des mouvements de la souris
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Fait tourner le joueur pour qu'il soit face à la vue de la caméra
        player.transform.Rotate(Vector3.up * mouseX);
        transform.parent.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);
    }
}
