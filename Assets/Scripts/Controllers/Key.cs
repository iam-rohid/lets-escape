using UnityEngine;
using DG.Tweening;

public class Key : MonoBehaviour, ICollectable
{
    [SerializeField]
    private float _endYPosition = 2f;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private float _startYPosition = 1f;

    public bool isCollected { get; private set; } = false;

    public bool CanCollect(Player player)
    {
        return !isCollected;
    }

    public bool Collect(Player player)
    {
        if (!CanCollect(player)) return false;

        isCollected = true;
        LogService.Log("Key Collected");

        // Collect Animation
        transform.DOKill();
        transform.DOScale(0, 0.35f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            Destroy(gameObject);
        });

        return true;
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, _startYPosition, transform.position.z);
        transform.DOMoveY(_endYPosition, _moveSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}