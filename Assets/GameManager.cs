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
        rooms = GameObject.FindGameObjectsWithTag("Room");

        rooms = rooms.OrderBy(room => room.name).ToArray();

        foreach (GameObject room in rooms)
        {
            if(room.GetComponent<RoomData>().roomID != 0)
            {
                room.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void reload()
    {
        SceneManager.LoadScene("lvl1");
        rooms[0].transform.GetChild(0).gameObject.SetActive(false);
        rooms[currentRoom.roomID].transform.GetChild(0).gameObject.SetActive(true);

    }
}