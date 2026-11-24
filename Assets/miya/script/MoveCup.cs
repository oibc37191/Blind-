using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveCup : MonoBehaviour
{
    [Header("保持距離")]
    public float HoldDistance = 0.8f;

    [Header("追従速度")]
    public float FollowSpeed = 8f;

    [Header("解除落下距離")]
    public float ReleaseFallDistance = 2.0f;

    private Rigidbody rb;

    private bool grabbed;
    private Transform holder;

    // 掴んだ時の高さ
    private float grabY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.constraints =
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!grabbed || holder == null)
            return;

        // 掴んだ位置から一定距離落下したら解除
        if (transform.position.y <= grabY - ReleaseFallDistance)
        {
            SetGrabbed(false, null);
            return;
        }

        float dir =
            (holder.eulerAngles.y > 90f &&
             holder.eulerAngles.y < 270f)
            ? 1f
            : -1f;

        float targetX =
            holder.position.x + dir * HoldDistance;

        float moveX =
            (targetX - rb.position.x) * FollowSpeed;

        rb.linearVelocity = new Vector3(
            moveX,
            rb.linearVelocity.y,
            0f);
    }

    public void SetGrabbed(bool value, Transform player)
    {
        grabbed = value;

        if (value)
        {
            holder = player;

            // 掴んだ時の高さを記録
            grabY = transform.position.y;

            rb.constraints =
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;

            Debug.Log("コップを掴んだ");
        }
        else
        {
            holder = null;

            rb.linearVelocity = new Vector3(
                0f,
                rb.linearVelocity.y,
                0f);

            rb.constraints =
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionZ |
                RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;

            Debug.Log("コップを離した");
        }
    }

    public bool IsGrabbed()
    {
        return grabbed;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        Debug.Log("Hit : " + other.name);

        if (other.CompareTag("Goal"))
        {
            Debug.Log("Goal!");

            GameManager.Instance.ClearGame();
        }
}
}