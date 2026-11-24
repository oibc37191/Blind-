using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Cord : MonoBehaviour
{
    public Move_Blind_Slat blind;

    public float pullAmount = 0f;
    public float pullSpeed = 50f;
    public float maxPull = 80f;

    private bool playerInRange;

    private void Update()
    {
        if (!playerInRange)
            return;

        // Eで閉じる→持ち上げる
        if (Keyboard.current.eKey.isPressed)
        {
            if (!blind.GetMaxAngle())
            {
                pullAmount +=
                    pullSpeed * Time.deltaTime;

                pullAmount =
                    Mathf.Clamp(
                        pullAmount,
                        0f,
                        maxPull);

                blind.SetAngle(pullAmount);
            }
            else
            {
                blind.Raise();
            }
        }

        // Rで下げる→開く
        if (Keyboard.current.rKey.isPressed)
        {
            if (!blind.IsUnfolded())
            {
                blind.Lower();
            }
            else
            {
                pullAmount -=
                    pullSpeed * Time.deltaTime;

                pullAmount =
                    Mathf.Clamp(
                        pullAmount,
                        0f,
                        maxPull);

                blind.SetAngle(pullAmount);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}