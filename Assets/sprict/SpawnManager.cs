using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 生成するプレハブ格納用
    public GameObject PrefabCube;

    public int timeOut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Randam());
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private IEnumerator Randam()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeOut);

            // プレハブの位置をランダムで設定
            float x = Random.Range(0f, 140f);
            float z = Random.Range(-70f, 70f);
            float y = Random.Range(1f, 7f);
            Vector3 pos = new Vector3(x, y, z);

            // プレハブを生成
            Instantiate(PrefabCube, pos, Quaternion.identity);
        }

    }
}