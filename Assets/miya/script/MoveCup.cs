using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveCup : MonoBehaviour
{
    [Header("保持距離")]
    public float HoldDistance = 0.8f;

    [Header("高さ補正")]
    public float HeightOffset = -0.5f;

    private Rigidbody rb;

    private bool grabbed;
    private bool IsGround;

    private Transform holder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;

        rb.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (!grabbed)
            return;

        if (holder == null)
            return;

        float dir = holder.eulerAngles.y > 90f &&holder.eulerAngles.y < 270f? 1f: -1f;

        Vector3 targetPos =
            new Vector3(holder.position.x + dir * HoldDistance,holder.position.y + HeightOffset,transform.position.z);

        transform.position = targetPos;
    }

    public void SetGrabbed(bool value, Transform player)
    {
        grabbed = value;

        if (value)
        {
            holder = player;
            Debug.Log("コップを掴んだ");
        }
        else
        {
            holder = null;
            Debug.Log("コップを離した");
        }
    }

    public bool IsGrabbed()
    {
        return grabbed;
    }

    public bool IsGrounded()
    {
        return IsGround;
    }

    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = false;
        }
    }
}