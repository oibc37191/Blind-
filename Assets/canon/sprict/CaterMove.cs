using UnityEngine;

public class CaterMove : MonoBehaviour
{
    private float speed = 2f;
    private Vector3 direction = new Vector3(0f, 0f, 1f); //方向

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(transform.position.z > 5f)
        {
            direction = new Vector3(0f, 0f, -1f);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }

        if(transform.position.z < -5f)
        {
            direction = new Vector3(0f, 0f, 1f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
