using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    public Manager Manager => Manager.Instance;

    void Start()
    {
        Manager.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if(count == totalCount)
            {
                StartCoroutine(StartGen());
            }
        });    
    }

    IEnumerator StartGen()
    {
        List<BoxController> bxList = new List<BoxController>();
        for (int i = 0; i < 50; i++)
        {
            Vector3 RVector3 = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            BoxController bx = Manager.Object.Spawn<BoxController>(RVector3);
            bxList.Add(bx);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < 50; i++)
        {
            Manager.Object.Despawn<BoxController>(bxList[i]);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(StartGen());
    }
}
