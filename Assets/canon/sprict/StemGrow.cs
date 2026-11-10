using UnityEngine;
using UnityEngine.InputSystem;

public class StemGrow : MonoBehaviour
{
    [Header("太陽光")]
    public DirectionalLight Sun_Light; //太陽光参照

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

        //太陽までRayが届いたらtrueが返される
        Grow_Sw = Sun_Light.Is_Hit_Light(transform);

        if (Grow_Sw)
        {        
            //最大の大きさまで大きくなる
            if (transform.localScale.y < Max_Scale)
            {
                float newScale = transform.localScale.y + Grow_Speed * Time.deltaTime;
                float newPosition = transform.localPosition.y + Grow_Speed * Time.deltaTime;

                //値が小さいほうを入れる
                newScale = Mathf.Min(newScale, Max_Scale);

                transform.localScale = new Vector3(0.1f, newScale, 0.1f);

                //中心点（position）を徐々に上に
                transform.localPosition = new Vector3(0f, newPosition, 0f);
            }
            else
            {
                Grow_Sw = false;

            }
        }       
    }
}
