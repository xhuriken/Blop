using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    public WinZone winZone;
    public GameObject barrier;
    public CinemachineVirtualCamera currentVCam;
    public CinemachineVirtualCamera newVCam;

    //private void Start()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    void Update()
    {
        if (winZone.playerBlueInside && winZone.playerRedInside)
        {
            barrier.SetActive(false);
            currentVCam.Priority = 5;
            newVCam.Priority = 10;
        }
    }
}
