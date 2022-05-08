using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
    }

    public void OnPlay()
    {
        Managers.Instance.SceneLoader.LoadLevel(0);
    }
}