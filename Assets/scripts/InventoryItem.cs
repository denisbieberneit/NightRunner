using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    public GameObject skin;
    [SerializeField]
    public bool inUse;
    [SerializeField]
    public bool owned;
    [SerializeField]
    public int coinCost;
}
