using UnityEngine;

public class Move_Blind_Slat : MonoBehaviour
{
    [Header("スラット")]
    public Transform[] Slats;

    [Header("角度")]
    public float MaxAngle = 80f;

    [Header("昇降速度")]
    public float RaiseSpeed = 0.15f;

    [Header("収納時の間隔")]
    public float FoldSpacing = 0.01f;

    private float MinAngle = 0f;
    private bool IsMaxAngle = false;

    private Vector3[] DefaultPos;

    // 収納済みの境界
    private int currentStackIndex;

    // 現在移動中のスラット
    private int movingIndex;

    private void Start()
    {
        DefaultPos = new Vector3[Slats.Length];

        for (int i = 0; i < Slats.Length; i++)
        {
            DefaultPos[i] = Slats[i].localPosition;
        }

        currentStackIndex = Slats.Length - 2;
        movingIndex = currentStackIndex;
    }

    //--------------------------------------------------
    // 角度
    //--------------------------------------------------
    public void SetAngle(float angle)
    {
        angle = Mathf.Clamp(angle, MinAngle, MaxAngle);

        foreach (Transform slat in Slats)
        {
            slat.localRotation =
                Quaternion.Euler(angle, 0f, 0f);
        }

        IsMaxAngle =
            Mathf.Approximately(angle, MaxAngle);
    }

    //--------------------------------------------------
    // 上げる
    //--------------------------------------------------
    public void Raise()
    {
        if (!IsMaxAngle)
            return;

        // 全収納済み
        if (movingIndex < 0)
            return;

        Transform moving = Slats[movingIndex];

        float targetY;

        // 一番上(0)だけ特別処理
        if (movingIndex == 0)
        {
            targetY = DefaultPos[0].y;
        }
        else
        {
            targetY =
                Slats[movingIndex - 1].localPosition.y
                - FoldSpacing;
        }

        Vector3 pos = moving.localPosition;

        pos.y = Mathf.MoveTowards(
            pos.y,
            targetY,
            RaiseSpeed * Time.deltaTime);

        moving.localPosition = pos;

        // 下側を追従
        for (int i = Slats.Length - 1;
             i > movingIndex;
             i--)
        {
            Vector3 followPos =
                Slats[i].localPosition;

            followPos.y =
                Slats[i - 1].localPosition.y
                - FoldSpacing;

            Slats[i].localPosition =
                followPos;
        }

        // 到着
        if (Mathf.Abs(pos.y - targetY) < 0.0001f)
        {
            pos.y = targetY;
            moving.localPosition = pos;

            // この時点で下側を完全に揃える
            for (int i = movingIndex + 1;
                 i < Slats.Length;
                 i++)
            {
                Vector3 p =
                    Slats[i].localPosition;

                p.y =
                    Slats[i - 1].localPosition.y
                    - FoldSpacing;

                Slats[i].localPosition = p;
            }

            currentStackIndex =
                movingIndex - 1;

            movingIndex--;
        }
    }

    //--------------------------------------------------
    // 下ろす
    //--------------------------------------------------
    public void Lower()
    {
        // 全収納状態から復帰
        if (movingIndex < 0)
        {
            movingIndex = 0;
        }

        if (movingIndex >= Slats.Length - 1 &&
            Mathf.Approximately(
                Slats[Slats.Length - 1].localPosition.y,
                DefaultPos[Slats.Length - 1].y))
        {
            return;
        }

        Transform moving =
            Slats[movingIndex];

        Vector3 pos =
            moving.localPosition;

        float targetY =
            DefaultPos[movingIndex].y;

        pos.y = Mathf.MoveTowards(
            pos.y,
            targetY,
            RaiseSpeed * Time.deltaTime);

        moving.localPosition = pos;

        if (Mathf.Approximately(pos.y, targetY))
        {
            pos.y = targetY;
            moving.localPosition = pos;

            // 次に戻すスラットへ
            if (movingIndex < Slats.Length - 1)
            {
                movingIndex++;
                currentStackIndex = movingIndex;
            }

  
        }

        FollowStackLower();
    }

    //--------------------------------------------------
    // 収納時追従
    //--------------------------------------------------
    private void FollowStack()
    {
        for (int i = Slats.Length - 1;
             i > movingIndex; i--)
        {
            Vector3 pos =
                Slats[i].localPosition;

            pos.y =
                Slats[i - 1].localPosition.y
                - FoldSpacing;

            Slats[i].localPosition = pos;
        }
    }

    //--------------------------------------------------
    // 展開時追従
    //--------------------------------------------------
    private void FollowStackLower()
    {
        for (int i = movingIndex + 1;
             i < Slats.Length; i++)
        {
            Vector3 pos =
                Slats[i].localPosition;

            pos.y =
                Slats[i - 1].localPosition.y
                - FoldSpacing;

            Slats[i].localPosition = pos;
        }
    }

   
    //--------------------------------------------------
    // 状態
    //--------------------------------------------------
    public bool GetMaxAngle()
    {
        return IsMaxAngle;
    }

    public bool IsFolded()
    {
        return movingIndex <= 0;
    }

    public bool IsUnfolded()
    {
        return movingIndex >=
               Slats.Length - 1;
    }
}