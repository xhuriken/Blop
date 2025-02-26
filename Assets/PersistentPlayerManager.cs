using UnityEngine;
using UnityEngine.InputSystem;

public class PersistentPlayerManager : MonoBehaviour
{
    private static PersistentPlayerManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
