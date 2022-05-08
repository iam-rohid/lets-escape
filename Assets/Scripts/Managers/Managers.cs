using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    // Game Manager
    [SerializeField]
    private GameManager _gameManager;

    public GameManager GameManager
    { get { return _gameManager; } }

    // Scene Loader
    [SerializeField]
    private SceneLoader _sceneLoader;

    public SceneLoader SceneLoader
    { get { return _sceneLoader; } }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}