using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Manager() { }
    public static Manager Instance { get; private set; }

    public PoolManager Pool = new PoolManager();
    public ResourceManager Resource = new ResourceManager();
    public ObjectManager Object = new ObjectManager();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
