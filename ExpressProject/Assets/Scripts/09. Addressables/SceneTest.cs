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

            if (count == totalCount)
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
            /* 비동기 처리는 지연시간이 있는 처리 방법이라고 나 혼자 오해해서 스터디하다가 넣은 함수 */

            // 함수 하나가 모든 처리를 다 하는 경우   -> 순차적으로 전부 직접 수행하기 때문에 동기 처리
            // 콜백으로 다른 함수를 부르는 경우       -> A 함수에서 B 함수로 역할을 분담할 수 있기 때문에 비동기 처리
            // 따라서 콜백은 비동기 처리 방법 중 하나

            /* JavaScript 강의에서 왜 그렇게 콜백 얘기가 많이 나오나 했더니... */
            Manager.UI.UpdateLoadingUI(i + 1, 50);

            Vector3 RVector3 = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            BoxController bx = Manager.Object.Spawn<BoxController>(RVector3);
            bxList.Add(bx);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < 50; i++)
        {
            Manager.UI.UpdateLoadingUI(i + 1, 50);
            Manager.Object.Despawn<BoxController>(bxList[i]);
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(StartGen());
    }
}
