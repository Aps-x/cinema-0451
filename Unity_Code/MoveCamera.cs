using UnityEngine;
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    void LateUpdate()
    {
        transform.position = player.position;
    }
}
