using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Cord : MonoBehaviour
{
    public Move_Blind_Slat blind;

    public float pullAmount = 0f;
    public float pullSpeed = 50f;
    public float maxPull = 80f;

    private bool playerInRange = false;

    void Update()
    {
        // プレイヤーが近くにいる時だけE入力を受け付ける
        if (playerInRange)
        {
            if (Keyboard.current.eKey.isPressed)
            {
                pullAmount += pullSpeed * Time.deltaTime;
            }
            else if(Keyboard.current.rKey.isPressed)
            {
                pullAmount -= pullSpeed * Time.deltaTime;
            }

            pullAmount = Mathf.Clamp(pullAmount, 0f, maxPull);

            blind.SetAngle(pullAmount);
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