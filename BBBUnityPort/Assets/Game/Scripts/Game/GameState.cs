using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState I { get; private set; }

    public PlayerState player = new();

    private void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }
}
