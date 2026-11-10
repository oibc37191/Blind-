using UnityEngine;
//葉身

public class LeafBlade : MonoBehaviour
{
    private float Scale_Speed = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StemGrow.Grow_Sw)
        {
            float newScale_x = transform.localScale.x + Scale_Speed * Time.deltaTime;
            float newScale_z = transform.localScale.z + Scale_Speed * Time.deltaTime;

            //大きくなるにつれて場所も移動する
            float newPositionX = transform.localPosition.x - Scale_Speed * Time.deltaTime;

            transform.localScale = new Vector3(newScale_x, 0.1f, newScale_z);
            transform.localPosition = new Vector3(newPositionX, 0f, 0f);
            //Debug.Log("葉");
        }

    }
}
