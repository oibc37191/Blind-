using UnityEngine;
using UnityEngine.InputSystem;

public class StemGrow : MonoBehaviour
{
    [Header("伸びるスピード")]
    public float Grow_Speed = 0.5f;

    [Header("最大の大きさ")]
    public float Max_Scale = 10f;

    static public bool Grow_Sw = false; //伸び始めのスイッチ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grow_Sw = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.gKey.wasPressedThisFrame && !Grow_Sw)
        {
            Grow_Sw = true;
        }

        if (Grow_Sw)
        {        
            if (transform.localScale.y < Max_Scale)
            {
                float newScale = transform.localScale.y + Grow_Speed * Time.deltaTime;

                //値が小さいほうを入れる
                newScale = Mathf.Min(newScale, Max_Scale);

                transform.localScale = new Vector3(0.1f, newScale, 0.1f);

                //位置固定
                transform.localPosition = new Vector3(0f, newScale - 1f, 0f);

                Leaf.Scale_Speed = 0.1f;
            }
            else
            {
                Grow_Sw = false;

                Leaf.Scale_Speed = 0f;
            }
        }       
    }
}
