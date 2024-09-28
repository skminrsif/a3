// Djaleen Malabonga
// Student #3128901
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class NPCExit : MonoBehaviour
{

    private bool _allowExit = false; // if near npc
    private bool _questComplete = false; // if boss quest is completed

    // Update is called once per frame
    void Update()
    {
        if (_allowExit && _questComplete) { // if both conditions are true, player can exit
            if (Input.GetKeyDown(KeyCode.E)) {
                #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
        }   
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider) { // true when player is near npc
        if (collider.tag == "Player") {
            _allowExit = true;

        }
        
    }

    void OnTriggerExit2D(Collider2D collider) { // false when player is not near npc
        if (collider.tag == "Player") {
            _allowExit = false;
        }
    }

    public void QuestCompleted() { // when boss is dead
        _questComplete = true;

    }
}
