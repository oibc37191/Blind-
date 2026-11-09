using UnityEngine;

public class Leaf : MonoBehaviour
{
    static public float Scale_Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newScale_x = transform.localScale.x + Scale_Speed * Time.deltaTime;
        float newScale_z = transform.localScale.z + Scale_Speed * Time.deltaTime;

        transform.localScale = new Vector3(newScale_x, 0.1f, newScale_z);
    }
}
