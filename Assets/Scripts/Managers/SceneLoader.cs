using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Image _loadingBar;

    [SerializeField]
    private GameObject _loadingCanvas;

    [SerializeField]
    private float _transitionDelay = 0.5f;

    [SerializeField]
    private SceneCollection _sceneCollection;

    public SceneCollection SceneCollection
    { get { return _sceneCollection; } }

    private float _progress;

    private void Awake()
    {
        _loadingCanvas.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        StartLoading(() =>
        {
            StartCoroutine(IELoadScene(scene));
        });
    }

    public void LoadLevel(int levelIndex)
    {
        var scene = _sceneCollection.GetLevel(levelIndex);
        LoadScene(scene.name);
    }

    private IEnumerator IELoadScene(string scene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            _progress = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
        _progress = 1;
        EndLoading();
    }

    private void StartLoading(Action onComplete)
    {
        _loadingBar.fillAmount = 0;
        _progress = 0;
        _loadingCanvas.SetActive(true);
        onComplete?.Invoke();
    }

    private void EndLoading()
    {
        _loadingCanvas.SetActive(false);
    }

    public void LoadMainMenu()
    {
        LoadScene(_sceneCollection.mainMenu.name);
    }

    private void Update()
    {
        _loadingBar.fillAmount = Mathf.MoveTowards(_loadingBar.fillAmount, _progress, Time.deltaTime * 10f);
    }
}