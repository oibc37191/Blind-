using UnityEngine;
using UnityEngine.InputSystem;

public class StemGrow : MonoBehaviour
{
    [Header("太陽光")]
    public DirectionalLight Sun_Light; //太陽光参照

    [Header("ブラインド")]
    public Move_Blind_Slat Blind;//ブラインド参照

    [Header("伸びるスピード")]
    public float Grow_Speed = 0.5f;

    [Header("最大の大きさ")]
    public float Max_Scale = 10f;
    private float Min_Scale = 1.0f; //最小の大きさ

    static public bool Grow_Sw = false; //伸び始めのスイッチ
    static public bool Shrink_Sw = false; //縮み始めのスイッチ

    private bool Is_Max_Scale = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Sun_Light == null)
            Sun_Light = GetComponent<DirectionalLight>(); 

        if(Blind == null)
            Blind = gameObject.GetComponent<Move_Blind_Slat>();

        Grow_Sw = false;
        Shrink_Sw = false;
    }

    // Update is called once per frame
    void Update()
    {
        //実験
        if(Keyboard.current.gKey.wasPressedThisFrame && !Grow_Sw)
        {
            Shrink_Sw = true;
        }

        //*** 確認 ***//
        if (Blind == null)
            Debug.Log("Blind = null");

        if (Sun_Light == null)
            Debug.Log("Light = null");
        //*******************************//

        if (transform.localScale.y < Max_Scale)
        {
            Is_Max_Scale = false;
        }

        //太陽までRayが届いたら、かつブラインドの角度がマックスになったら、かつ最大大きさになるまでtrueが返される   
        Grow_Sw = Sun_Light.Is_Hit_Light(transform) && Blind.GetMaxAngle() && !Is_Max_Scale;

        Debug.Log("伸びるかどうか" + Grow_Sw);
        Debug.Log("縮むかどうか" + Shrink_Sw);

        //伸びる処理
        if (Grow_Sw)
        {        
            //最大の大きさまで大きくなる
            if (transform.localScale.y < Max_Scale)
            {
                Is_Max_Scale = false;

                //どんどん大きく
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
                Is_Max_Scale = true;
                Grow_Sw = false;
            }
        }
        
        //縮む処理
        if(Shrink_Sw)
        {
            if(transform.localScale.y > Min_Scale)
            {
                //どんどん小さく
                float newScale = transform.localScale.y - Grow_Speed * Time.deltaTime;
                float newPosition = transform.localPosition.y - Grow_Speed * Time.deltaTime;

                //値が小さいほうを入れる
                newScale = Mathf.Min(newScale, Max_Scale);

                transform.localScale = new Vector3(0.1f, newScale, 0.1f);

                //中心点（position）を徐々に下に
                transform.localPosition = new Vector3(0f, newPosition, 0f);

            }
        }
    }
}
