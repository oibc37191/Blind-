using Unity.VisualScripting;
using UnityEngine;
//葉柄（葉っぱと茎をつなぐところ）

public class Petiole : MonoBehaviour
{
    private float Scale_Speed_Y = 3f;
    private float Scale_Speed_XZ = 3f;

    StemGrow steam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        steam = GetComponentInParent<StemGrow>(); //親取得
    }

    // Update is called once per frame
    void Update()
    {
        if(StemGrow.Grow_Sw)
        {
            //親オブジェクトの影響を受けないまま大きくなれる
            float newScale_x = 1f / steam.transform.localScale.y + Scale_Speed_XZ * Time.deltaTime;
            float newScale_y = transform.localScale.y + Scale_Speed_Y * Time.deltaTime;
            float newScale_z = transform.localScale.z + Scale_Speed_XZ * Time.deltaTime;

            transform.localScale = new Vector3(newScale_x, newScale_y, newScale_z);

            float newPosition = transform.localPosition.x - Scale_Speed_Y * Time.deltaTime;

            transform.localPosition = new Vector3(newPosition, 0f, 0f);

            //Debug.Log("葉柄");
        }

        if(StemGrow.Shrink_Sw)
        {
            //親オブジェクトの影響を受けないまま大きくなれる
            float newScale_x = 1f / steam.transform.localScale.y - Scale_Speed_XZ * Time.deltaTime;
            float newScale_y = transform.localScale.y - Scale_Speed_Y * Time.deltaTime;
            float newScale_z = transform.localScale.z - Scale_Speed_XZ * Time.deltaTime;

            transform.localScale = new Vector3(newScale_x, newScale_y, newScale_z);

            float newPosition = transform.localPosition.x + Scale_Speed_Y * Time.deltaTime;

            transform.localPosition = new Vector3(newPosition, 0f, 0f);

            //Debug.Log("葉柄");
        }
    }
}
