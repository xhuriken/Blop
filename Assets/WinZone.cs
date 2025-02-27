using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public bool playerBlueInside = false;
    public bool playerRedInside = false;
    private GameObject barrier;
    public int currentRoom;    
    public bool wasActive = false;

    private void Start()
    {
        barrier = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!wasActive && playerBlueInside && playerRedInside)
        {
            wasActive = true;
            barrier.SetActive(false);

            GameManager.Instance.rooms[currentRoom - 1].transform.GetChild(0).gameObject.SetActive(false); //working
            GameManager.Instance.rooms[currentRoom].transform.GetChild(0).gameObject.SetActive(true);

            RoomData roomData = GameManager.Instance.rooms[currentRoom].GetComponent<RoomData>();
            Room _room = new Room(roomData.spawnPoint1, roomData.spawnPoint2, roomData.vcam,roomData.roomID);
            GameManager.Instance.currentRoom = _room;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop"))
        {
            playerBlueInside = true;
        }
        else if (collision.CompareTag("RedBlop"))
        {
            playerRedInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BlueBlop"))
        {
            playerBlueInside = false;
        }
        else if (collision.CompareTag("RedBlop"))
        {
            playerRedInside = false;
        }
    }
}
