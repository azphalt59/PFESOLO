using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTransition : MonoBehaviour
{
    public void CameraTransitionRot()
    {
        //transform.DOLookAt(PossManager.Instance.gameObject.transform.forward, 1).OnComplete(CameraTransitionMvt);
        transform.DORotate(new Vector3(0, PossManager.Instance.gameObject.transform.rotation.y + 180,0),1).OnComplete(CameraTransitionMvt);
    }
    public void CameraTransitionMvt()
    {
        Vector3 targetPos = PossManager.Instance.GetPossItem().transform.position + PossManager.Instance.GetPossItem().GetComponent<Possessable>().GetCamOffset();
        transform.DOMove(targetPos, PlayerMovement.Instance.GetCamTransitionDuration()).OnComplete(EnableCameraRotationControl);
    }
    public void EnableCameraRotationControl()
    {
        GetComponent<CameraRotation>().cameraOffset = PossManager.Instance.GetPossItem().GetComponent<Possessable>().GetCamOffset();
        GetComponent<CameraRotation>().thirdPersonCanRotate = true;

        GetComponent<PlayerFollow>().enabled = true;
        GetComponent<PlayerFollow>().PossededItem = PossManager.Instance.GetPossItem().transform;
        GetComponent<PlayerFollow>().SetCameraOffset(PossManager.Instance.GetPossItem().GetComponent<Possessable>().GetCamOffset());
       
        
    }
   

}
