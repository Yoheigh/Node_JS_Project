using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Debug.Log("LobbyScene Init");
    }

    protected override void Clear()
    {
        base.Clear();
        Debug.Log("LobbyScene Clear");
    }
}
