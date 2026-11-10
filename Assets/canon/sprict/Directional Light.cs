using UnityEngine;
//太陽光

public class DirectionalLight : MonoBehaviour
{
    public bool Is_Hit_Light(Transform target)
    {
        //太陽光の向きと逆に進む
        Vector3 direction = -transform.forward;

        //物体から放つRayの場所たち
        Vector3[] position = { target.position,
                               target.position + target.up,
                               target.position - target.up};
        
        //6ビットに値するのレイヤーは遮蔽物としない
        int layerMask = 1 << 6;
        layerMask = ~layerMask; //反転

        int count_cover = 0; //遮蔽物に当たった数

        for(int i = 0; i < position.Length; i++)
        {
            //物体から太陽の方向に飛ばす
            Ray ray = new Ray(position[i], //始点
                               direction   //方向
                               );

            // Rayを可視化
            Debug.DrawRay(ray.origin, //始点
                ray.direction * 100f, //方向と長さ
                Color.red);           //色

            //何かに当たった = 太陽まで届かなかったのでfalseを返す
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,layerMask))
            {
                count_cover++;
            }
        }

        //すべての光が遮蔽物に当たったら、もしくは最大の数より多く当たったら（エラー）falseを返す
        if (count_cover >= position.Length)
            return false;
        else 
            return true; //1つでも光が太陽に届いていたらtrueを返す
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
