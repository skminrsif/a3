// Djaleen Malabonga
// Student #3128901

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGGameManager : MonoBehaviour
{
    [SerializeField] private SpawnPoint _playerSpawnPoint; // player spawn point
    [SerializeField] private RPGCameraManager _cameraManager; // camera manager obj

    [SerializeField] private Text _questText; // boss quest
    [SerializeField] private Text _gameWinText; // game win text
    private static RPGGameManager _instance = null;  // singleton instance

    [SerializeField] private GameObject _boss; // boss object


    public static RPGGameManager Instance { // getter for instance
        get {
            return _instance;
        }
    }

    void Awake() { // singleton constructor
        if (_instance != null && _instance != this) { 
            Destroy(gameObject);

        } else {
            _instance = this;

        }
    }

    void Start() { // set up the scene from the start
        SetUpScene();

    }

    public void SetUpScene() {
        SpawnPlayer(); // spawns player
    }

    public void SpawnPlayer() { // spawn player at spawn point
        if (_playerSpawnPoint != null) {
            GameObject player = _playerSpawnPoint.SpawnObject();
            _cameraManager.VirtualCamera.Follow = player.transform;

        }
    }

    public void CongratulatePlayer() { // congratulate the player for completing the boss quest and let them exit the game
        _gameWinText.gameObject.SetActive(true);
        _questText.gameObject.SetActive(false);
        GameObject.Find("NPC").GetComponent<NPCExit>().QuestCompleted();
    }

    public void SpawnBoss() { // spawn the boss
        _boss.SetActive(true);
    }


}
