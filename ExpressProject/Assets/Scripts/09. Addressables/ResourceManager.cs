using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class ResourceManager
{
    Dictionary<string, Object> _resources = new Dictionary<string, Object>();       // 리소스 리스트 목록 관리

    public Manager Manager => Manager.Instance;                                     // 매니저를 통해 Object 관리를 하기 위해 선언

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if(prefab == null)
        {
            Debug.LogError($"Faild to Load Prefab : {key}");
            return null;
        }

        if (pooling)
            return Manager.Pool.Pop(prefab);

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        if (Manager.Pool.Push(go))
            return;

        Object.Destroy(go);
    }

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            if(typeof(T) == typeof(Sprite))
            {
                Texture2D tex = resource as Texture2D;
                Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                return spr as T;
            }

            return resource as T;
        }
        return Resources.Load<T>(key);
    }

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))                       
        {                                                                           // 키 값 검사
            callback?.Invoke(resource as T);
            return;                                                                 // 콜백 검사 후 반환
        }

        var asyncOperation = Addressables.LoadAssetAsync<T>(key);                   // 1번째 핸들 : 각 리소스 호출이 완료되었을 때 호출
        asyncOperation.Completed += (op) =>                                         // 리소스 로딩
        {                                                                           // 완료되면 콜백 검사 후 반환
            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : Object
    {
        var OpHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));   // 2번째 핸들 : 모든 리소스 호출이 완료되었을 때 호출

        OpHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    Manager.UI.UpdateUI(loadCount, totalCount);
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
}
