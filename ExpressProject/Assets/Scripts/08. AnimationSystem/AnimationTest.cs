using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTest : MonoBehaviour
{
    public Button button01;
    public Button button02;
    public Button button03;

    public AnimationActions animationActions;

    private void Start()
    {
        button01.onClick.AddListener(() =>
        animationActions.TakeAction("FishingCast"));

        button02.onClick.AddListener(() =>
        animationActions.TakeAction("FishingReel"));

        button03.onClick.AddListener(() =>
        animationActions.TakeAction("FishingFinish"));
    }
}
