using UnityEngine;

public class Move_Blind_Slat : MonoBehaviour
{
    public Transform[] Slats;
    public float MaxAngle = 80f;
    private float MinAngle = 0f;

    private bool IsMaxAngle = false;

    public void SetAngle(float angle)
    {
        angle = Mathf.Clamp(angle, 0f, MaxAngle);

        foreach (Transform slat in Slats)
        {
            slat.localRotation = Quaternion.Euler(angle, 0, 0);

            if (angle == MaxAngle)
            {
                IsMaxAngle = true;
                //Debug.Log("MaxAngle");
            }
            else if (angle == MinAngle && !StemGrow.Is_Min_Scale)
            {
                IsMaxAngle = false;
                StemGrow.Shrink_Sw = true;
                //Debug.Log("角度は０です");
            }
            else
            {
                IsMaxAngle = false;
                StemGrow.Shrink_Sw = false;
                //Debug.Log("NoAngle");
            }
        }
    }

    //角度が最大値になったかどうかを返す関数
    public bool GetMaxAngle()
    {
        return IsMaxAngle;
    }
}