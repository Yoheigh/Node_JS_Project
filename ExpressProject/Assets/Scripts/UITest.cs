using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITest : MonoBehaviour
{
    public Text text;
    public Image image;

    public void UpdateUI(int count, int total)
    {
        text.text = $"에셋을 불러오는 중입니다 ... {count}/{total}";
        image.fillAmount = count / total;
    }
}
