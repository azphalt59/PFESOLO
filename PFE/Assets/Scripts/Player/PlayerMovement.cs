using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject cam;

    [Header("First Person Mvt")]
    [SerializeField] private float firstPersonSpeed = 2f;

    [Header("Third Person Mvt")]
    [SerializeField] private float thirdPersonSpeed = 2f;

    [Header("Possession Transition")]
    [SerializeField] private float playerObjectTransitionDuration = 3f;
    [SerializeField] private float camTransitionDuration = 3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if(PossManager.Instance.possessionState == PossManager.PossessionState.Free)
            FirstPersonMvt();
        if (PossManager.Instance.possessionState == PossManager.PossessionState.InPossession)
        {
            if(Input.GetMouseButtonDown(1))
            {
                ReleaseFromPossession();
                return;
            }
            ThirdPersonMvt();
        }
    }

    public void FirstPersonMvt()
    {
        Vector3 inputVector;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        Vector3 vertMvt = transform.TransformDirection(Vector3.forward) *  Input.GetAxis("Vertical") * firstPersonSpeed;
        Vector3 horiMvt = transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * firstPersonSpeed;
        transform.parent.position += (vertMvt * Time.deltaTime) + (horiMvt * Time.deltaTime);
    }

    public void ThirdPersonMvt()
    {
        Vector3 inputVector;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        Vector3 camDir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = inputVector.z * camDir + inputVector.x * Camera.main.transform.right;

        PossManager.Instance.GetPossItem().transform.LookAt(PossManager.Instance.GetPossItem().transform.position - movement);
        movement = transform.InverseTransformDirection(movement);
       

        transform.parent.position += movement.normalized * thirdPersonSpeed * Time.deltaTime;
        
    }

    public void PossessionTransitionMvt(Vector3 endPosition)
    {
        playerParent.transform.DOMove(endPosition, playerObjectTransitionDuration).OnComplete(PossessionItemChangeParent);
    }
    public void PossessionItemChangeParent()
    {
        PossManager.Instance.GetPossItem().transform.parent = playerParent.transform;
        cam.GetComponent<CameraTransition>().CameraTransitionRot();
    }

    public float GetCamTransitionDuration()
    {
        return camTransitionDuration;
    }

    public void ReleaseFromPossession()
    {
        PossManager.Instance.possessionState = PossManager.PossessionState.Free;
        PossManager.Instance.GetPossItem().transform.parent = null;
        PossManager.Instance.SetPossItem(null);
        cam.GetComponent<PlayerFollow>().enabled = false;
    }

   
    
}
