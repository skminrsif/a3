// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private ItemData _item;

    // getter for item data
    
    public ItemData Item {
        get {
            return _item;
        }

    }
}
