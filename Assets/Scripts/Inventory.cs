// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab; // slot prefab
    private const int _numSlots = 5; // number of slots
    private Image[] _itemImages = new Image[_numSlots]; // item images
    private ItemData[] _items = new ItemData[_numSlots]; // items
    private GameObject[] _slots = new GameObject[_numSlots]; // slots

    // Start is called before the first frame update
    void Start()
    {
        CreateSlots(); // instantiate slots
    }

    public void CreateSlots() { // instantiate slots based on number of slots
        if (_slotPrefab != null) {
            for (int i = 0; i < _numSlots; i++) {
                GameObject newSlot = Instantiate(_slotPrefab);
                newSlot.name = "ItemSlot_" + i; 

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                _slots[i] = newSlot;
                _itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();

            }
        }
    }

    public bool AddItem(ItemData itemToAdd) { // add items in the inventory when it gets picked up
        for (int i = 0; i < _items.Length; i++) {
            if (_items[i] != null && _items[i].Type == itemToAdd.Type && itemToAdd.IsStackable == true) { // if item is stackable, keep adding to the quantity
                _items[i].Quantity = _items[i].Quantity + 1;
                Slot slotScript = _slots[i].gameObject.GetComponent<Slot>();
                Text quantityText = slotScript.QtyText;
                quantityText.enabled = true;
                quantityText.text = _items[i].Quantity.ToString();
                return true;

            }

            if (_items[i] == null) { // add new item to slot (for some weird reason, this section of code hates stackable items)
                _items[i] = Instantiate(itemToAdd);
                _itemImages[i].gameObject.SetActive(true);
                _items[i].Quantity = 1;
                _itemImages[i].sprite = itemToAdd.Sprite;
                _itemImages[i].enabled = true;
                return true;

            }

        }

        return false; 
        
    }
}
