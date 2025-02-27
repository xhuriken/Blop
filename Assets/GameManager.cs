using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Room currentRoom;
    public GameObject[] rooms;
    public bool isPlayerDead = false;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ReloadRooms();
    }

    public void ReloadRooms()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");

        rooms = rooms.OrderBy(room => room.name).ToArray();

        foreach (GameObject room in rooms)
        {
            if (room.GetComponent<RoomData>().roomID != 0)
            {
                room.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("Room " + room.GetComponent<RoomData>().roomID + " is inactive");
            }
        }
    }
    public IEnumerator reload()
    {
        SceneManager.LoadScene("lvl1");
        yield return new WaitForSeconds(0.2f);
        ReloadRooms();
        Debug.Log(currentRoom.spawnPoint1 + " | " + currentRoom.spawnPoint2 + " | " + currentRoom.roomID);
        rooms[0].transform.GetChild(0).gameObject.SetActive(false);
        rooms[currentRoom.roomID].transform.GetChild(0).gameObject.SetActive(true);

    }
}