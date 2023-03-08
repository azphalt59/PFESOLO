using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Possessable : MonoBehaviour, IPossesable
{
    [SerializeField] private float possessionRange;
    [SerializeField] private bool inPossessionRange;
    [SerializeField] private bool inPossession;

    [SerializeField] private Material glowMat;
    [SerializeField] private Material normalMat;

    [SerializeField] private Vector3 camPositionOffset;

    private void Update()
    {
        float playerDistance = Vector3.Distance(PlayerMovement.Instance.transform.parent.position, transform.position);
        if (possessionRange >= playerDistance)
        {
            inPossessionRange = true;
            Glow();
        } 
        else
        {
            inPossessionRange = false;
            UnGlow();
        }
            
    }
    public void Glow()
    {
        GetComponent<MeshRenderer>().material = glowMat;
    }
    public void UnGlow()
    {
        GetComponent<MeshRenderer>().material = normalMat;
    }
    public void BeingPossessed()
    {
        Debug.Log("JE SUIS POSSEDE");
        PossManager.Instance.possessionState = PossManager.PossessionState.InPossession;
        PossManager.Instance.SetPossItem(gameObject);
        PlayerMovement.Instance.PossessionTransitionMvt(transform.position);
        inPossession = true;
        
    }
    private void OnMouseDown()
    {
        if (inPossessionRange)
            BeingPossessed();
        else
            Debug.Log("Trop loin");
    }

    public Vector3 GetCamOffset()
    {
        return camPositionOffset;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, possessionRange);

        Gizmos.color = Color.black;
        float size = transform.localScale.x * 0.2f;
        Gizmos.DrawCube(transform.position + camPositionOffset, new Vector3(size, size, size));
    }

}
