using UnityEngine;
using UnityEngine.InputSystem;
public class Move_Player : MonoBehaviour
{
    [Header("プレイヤー番号")]
    public int PlayerID = 1;

    [Header("移動")]
    public float Move_Speed = 5f;

    [Header("ジャンプ")]
    public float JumpPower = 6f;

    [Header("茎")]
    public float ClimbSpeed = 3f;

    [Header("重力")]
    public float FallMultiplier = 5f;
    public float LowJumpMultiplier = 3f;

    private Rigidbody rd;

    private float Move_Input;
    private float ClimbInput;

    private bool IsGround;
    private bool CanClimb;
    private bool IsStem;

    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Player1
        if (PlayerID == 1)
        {
            Move_Input =
                (Keyboard.current.dKey.isPressed ? 1 : 0) -
                (Keyboard.current.aKey.isPressed ? 1 : 0);

            ClimbInput =
                (Keyboard.current.wKey.isPressed ? 1 : 0) -
                (Keyboard.current.sKey.isPressed ? 1 : 0);

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }
        }
        // Player2
        else if (PlayerID == 2)
        {
            Move_Input =
                (Keyboard.current.rightArrowKey.isPressed ? 1 : 0) -
                (Keyboard.current.leftArrowKey.isPressed ? 1 : 0);

            ClimbInput =
                (Keyboard.current.upArrowKey.isPressed ? 1 : 0) -
                (Keyboard.current.downArrowKey.isPressed ? 1 : 0);

            if (Keyboard.current.rightCtrlKey.wasPressedThisFrame)
            {
                Jump();
            }
        }

        // 茎掴み
        if (CanClimb && Mathf.Abs(ClimbInput) > 0.1f)
        {
            IsStem = true;
        }

        // 向き変更
        if (Move_Input > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Move_Input < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        // 茎登り
        if (IsStem)
        {

            rd.useGravity = false;

            rd.linearVelocity = new Vector3(0f, ClimbInput * ClimbSpeed, 0f);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            return;
        }

        rd.useGravity = true;

        rd.linearVelocity = new Vector3(Move_Input * Move_Speed, rd.linearVelocity.y, 0f);

        // 落下加速
        if (rd.linearVelocity.y < 0)
        {
            rd.linearVelocity +=
                Vector3.up * Physics.gravity.y *
                (FallMultiplier - 1f) *
                Time.fixedDeltaTime;
        }
        // 上昇を短く
        else if (rd.linearVelocity.y > 0)
        {
            rd.linearVelocity +=
                Vector3.up * Physics.gravity.y *
                (LowJumpMultiplier - 1f) *
                Time.fixedDeltaTime;
        }
    }

    private void Jump()
    {
        // 茎からジャンプ
        if (IsStem)
        {
            IsStem = false;
            rd.useGravity = true;

            rd.linearVelocity = new Vector3(
                rd.linearVelocity.x,
                JumpPower,
                0f);

            return;
        }

        // 地面ジャンプ
        if (IsGround)
        {
            rd.linearVelocity = new Vector3(
                rd.linearVelocity.x,
                JumpPower,
                0f);

            IsGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stem"))
        {
            CanClimb = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stem"))
        {
            CanClimb = false;
            IsStem = false;

            rd.useGravity = true;
        }
    }
}
