using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCup : MonoBehaviour
{
    public Transform player;

    [Header("掴む距離")]
    public float grabDistance = 2f;

    [Header("保持距離")]
    public float holdDistance = 1.5f;

    public float moveSpeed = 5f;

    private bool grabbed = false;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            float distance = Vector3.Distance(
                player.position,
                transform.position);

            // 近い時だけ掴む
            if (!grabbed && distance <= grabDistance)
            {
                grabbed = true;
            }
            // 掴んでいたら離す
            else if (grabbed)
            {
                grabbed = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!grabbed) return;

        Vector3 targetPos =
            player.position +
            player.forward * holdDistance;

        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.fixedDeltaTime
        );
    }
}