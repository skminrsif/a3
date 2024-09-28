// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    
    [SerializeField] private Text _qtyText; // quantity text for inventory slot (shows quantity of obj in slot)

    public Text QtyText { // getter for quantity text
        get {
            return _qtyText;
        }
    }

}
