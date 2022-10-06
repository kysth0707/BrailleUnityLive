using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    int BallCount = 32;
    public GameObject PrefabObj;
    public GameObject ParentObj;
    public bool Generate = false;

    // Update is called once per frame
    void Update()
    {
        if (Generate)
        {
            Generate = false;

            
        }
    }

    public List<GameObject> CreateBalls()
    {
        List<GameObject> Balls = new List<GameObject>();

        for (int x = 0; x < BallCount; x++)
        {
            for (int z = 0; z < BallCount; z++)
            {
                Vector3 Pos = new Vector3(x * 0.5f - BallCount * 0.5f / 2f + 0.25f, 0, z * 0.5f - BallCount * 0.5f / 2f + 0.25f);

                GameObject Copy = Instantiate(PrefabObj, Pos, PrefabObj.transform.rotation);
                Copy.transform.parent = ParentObj.transform;
                Balls.Add(Copy);
            }
        }

        return Balls;
    }
}
