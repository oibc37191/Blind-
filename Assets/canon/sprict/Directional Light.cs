using UnityEngine;

public class DirectionalLight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(
            transform.position,//始点
            transform.forward//正面方向
        );

        // Rayを可視化
        Debug.DrawRay(ray.origin, //始点
            ray.direction * 100f, //方向と長さ
            Color.red);           //色

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if(hit.collider.tag == "Flower")
            {
                StemGrow.Grow_Sw = true;
            }

            Debug.Log("光が当たったオブジェクト：" + hit.collider.name);
        }
    }
}
