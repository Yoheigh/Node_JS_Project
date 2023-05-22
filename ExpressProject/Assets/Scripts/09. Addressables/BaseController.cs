using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    bool _init = false;

    private void Awake()
    {
        Init();
    }

    // 한 번만 초기화하는 함수
    public virtual bool Init()
    {
        if (_init) return false;
        _init = true;
        return true;
    }
}
