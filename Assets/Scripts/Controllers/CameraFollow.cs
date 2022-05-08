using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private float followSpeed = 5f;

    [SerializeField]
    private Vector3 followDirections = Vector3.forward;

    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Player>();
        }
    }

    private void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.x = followDirections.x > 0 ? playerPos.x : 0;
        playerPos.y = followDirections.y > 0 ? playerPos.y : 0;
        playerPos.z = followDirections.z > 0 ? playerPos.z : 0;
        playerPos += offset;
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * followSpeed);
    }
}