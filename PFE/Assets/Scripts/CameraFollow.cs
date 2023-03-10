using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float camHeightOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PossManager.Instance.possessionState == PossManager.PossessionState.Free)
        {
            transform.position = player.transform.position + new Vector3(0, camHeightOffset, 0);
        }
            
    }
}
