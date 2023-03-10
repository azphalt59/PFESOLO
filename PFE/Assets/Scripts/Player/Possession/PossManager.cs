using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossManager : MonoBehaviour
{
    public static PossManager Instance;
    public enum PossessionState
    {
        Free, InPossession
    }
    public PossessionState possessionState;
    [SerializeField] private GameObject possItem;
    [SerializeField] private GameObject cursor;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        possessionState = PossessionState.Free;
    }
    private void Update()
    {
        if (possessionState == PossessionState.InPossession)
            cursor.SetActive(false);
        else
            cursor.SetActive(true);
    }
    public void PossessionEntry()
    {
        possessionState = PossessionState.InPossession;
    }

    public void SetPossItem(GameObject item)
    {
        possItem = item;
    }
    public GameObject GetPossItem()
    {
        return possItem;
    }
}
