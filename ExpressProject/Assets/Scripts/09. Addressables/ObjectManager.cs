using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Threading;

public class ObjectManager
{
    public HashSet<BoxController> Box { get; } = new HashSet<BoxController>();

    public Manager Manager => Manager.Instance;

    public ObjectManager()
    {
        Init();
    }

    public void Init()
    {

    }

    public void Clear()
    {

    }

    public T Spawn<T>(Vector3 position) where T : BoxController
    {
        System.Type type = typeof(T);

        if(type == typeof(BoxController))
        {
            GameObject go = Manager.Resource.Instantiate("Box", pooling: true);
            BoxController bc = go.GetComponent<BoxController>();
            go.transform.position = position;
            Box.Add(bc);
            return bc as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : BoxController
    {
        System.Type type = typeof(T);
        if(type == typeof(BoxController))
        {
            Box.Remove(obj as BoxController);
            Manager.Resource.Destroy(obj.gameObject);
        }
    }
}
