using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Player : MonoBehaviour
{
    [Header("プレイヤー番号")]
    public int PlayerID = 1;

    [Header("移動速度")]
    public float Move_Speed = 5f;

    [Header("掴み中速度")]
    public float PushSpeed = 2f;

    [Header("コップを掴める距離")]
    public float PushModeDistance = 1.5f;

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

    private bool IsPushMode;

    private MoveCup currentCup;

    void Start()
    {
        rd = GetComponent<Rigidbody>();

        rd.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        //--------------------------------
        // Eキーで掴む・離す
        //--------------------------------
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!IsPushMode)
            {
                Collider[] hits =
                    Physics.OverlapSphere(transform.position,PushModeDistance);

                foreach (Collider hit in hits)
                {
                    MoveCup cup =
                        hit.GetComponent<MoveCup>();

                    if (cup == null)
                        continue;

                    currentCup = cup;

                    currentCup.SetGrabbed(true,transform);

                    IsPushMode = true;

                    break;
                }
            }
            else
            {
                
                if (currentCup != null)
                {
                    currentCup.SetGrabbed(false,null);

                    currentCup = null;
                }

                IsPushMode = false;
            }
        }
        
        //--------------------------------
        // Player1
        //--------------------------------
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
        //--------------------------------
        // Player2
        //--------------------------------
        else
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

        //--------------------------------
        // 茎を掴む
        //--------------------------------
        if (CanClimb && Mathf.Abs(ClimbInput) > 0.1f)
        {
            IsStem = true;
        }

        //--------------------------------
        // 向き変更
        //--------------------------------
        if (!IsPushMode)
        {
            if (Move_Input > 0)
            {
                transform.rotation =
                    Quaternion.Euler(0, 180, 0);
            }
            else if (Move_Input < 0)
            {
                transform.rotation =
                    Quaternion.Euler(0, 0, 0);
            }
        }

    }

    void FixedUpdate()
    {
        //--------------------------------
        // 茎登り
        //--------------------------------
        if (IsStem)
        {
            rd.useGravity = false;

            rd.linearVelocity =
                new Vector3(0f,ClimbInput * ClimbSpeed,0f);

            return;
        }

        rd.useGravity = true;

        float speed =IsPushMode? PushSpeed: Move_Speed;

        rd.linearVelocity =new Vector3(Move_Input * speed,rd.linearVelocity.y,0f);

        //--------------------------------
        // 落下加速
        //--------------------------------
        if (rd.linearVelocity.y < 0)
        {
            rd.linearVelocity +=Vector3.up *Physics.gravity.y *(FallMultiplier - 1f) *Time.fixedDeltaTime;
        }
        else if (rd.linearVelocity.y > 0)
        {
            rd.linearVelocity +=Vector3.up *Physics.gravity.y *(LowJumpMultiplier - 1f) *Time.fixedDeltaTime;
        }
    }

    void Jump()
    {
        if (IsPushMode)
        {
            ReleaseCup();
        }
        if (IsStem)
        {
            IsStem = false;

            rd.useGravity = true;

            rd.linearVelocity =new Vector3(rd.linearVelocity.x,JumpPower,0f);

            return;
        }

        if (IsGround)
        {
            rd.linearVelocity =new Vector3(rd.linearVelocity.x,JumpPower,0f);

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

    public bool IsPushing()
    {
        return IsPushMode;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            PushModeDistance);
    }
    private void ReleaseCup()
    {
        Debug.Log("ReleaseCup");

        if (currentCup != null)
        {
            currentCup.SetGrabbed(false, null);
            currentCup = null;
        }

        IsPushMode = false;
    }
}