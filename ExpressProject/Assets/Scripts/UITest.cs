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
        text.text = $"������ �ҷ����� ���Դϴ� ... {count}/{total}";
        image.fillAmount = count / total;
    }
}
