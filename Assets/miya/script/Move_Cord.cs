using UnityEngine;
using UnityEngine.InputSystem;

public class Move_Cord : MonoBehaviour
{
    public Move_Blind_Slat blind;

    public float pullAmount = 0f;
    public float pullSpeed = 50f;
    public float maxPull = 80f;

    void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            pullAmount += pullSpeed * Time.deltaTime;
        }
        else
        {
            pullAmount -= pullSpeed * Time.deltaTime;
        }

        pullAmount = Mathf.Clamp(pullAmount, 0f, maxPull);

        blind.SetAngle(pullAmount);
    }
}