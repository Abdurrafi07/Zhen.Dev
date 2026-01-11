using UnityEngine;

public class CameraFollowCenter : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            -10f   // KUNCI Z (WAJIB)
        );
    }
}
