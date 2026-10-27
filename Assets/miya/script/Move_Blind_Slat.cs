using UnityEngine;

public class Move_Blind_Slat : MonoBehaviour
{
    public Transform[] Slats;
    public float MaxAngle = 80f;

    public void SetAngle(float angle)
    {
        angle = Mathf.Clamp(angle, 0f, MaxAngle);

        foreach (Transform slat in Slats)
        {
            slat.localRotation = Quaternion.Euler(angle, 0, 0);
        }
    }
}