using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystemManagerV2 : MonoBehaviour
{
    private UISystemManagerV2() { }
    // 기존에 쓰던 방식은 필드 변수 선언 후 등록하는 방식
    // private UISystemManagerV2 instance

    public static UISystemManagerV2 Instance { get; private set; }

    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private UIPopup popupPrefabs;

    [SerializeField]
    private GameObject Canvas;

    [SerializeField]
    private GameObject Panels;

    private int currentPanelIndex;
    private Stack<UIPopup> popupStack = new Stack<UIPopup>();

    private bool UIShowingflag;         // UI 플래그 제어



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

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == index)
            {
                panels[i].Show();
            }
            else
            {
                panels[i].Hide();
            }
        }
    }

    public void NextPanel()
    {
        currentPanelIndex = (currentPanelIndex + 1) % panels.Count;
        ShowPanel(currentPanelIndex);
    }

    public void PreviousPanel()
    {
        currentPanelIndex--;
        if (currentPanelIndex < 0)
        {
            currentPanelIndex = panels.Count - 1;
        }
        ShowPanel(currentPanelIndex);
    }

    public void HidePanelAll()
    {
        panels[currentPanelIndex].Hide();
        Panels.SetActive(false);
        currentPanelIndex = 0;
    }

    public void ShowUIMenu()
    {
        Panels.SetActive(true);
        ShowPanel(currentPanelIndex);
        
    }

    void Start()
    {
        HidePanelAll();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ShowUIMenu();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPanel();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPanel();
        }
    }
}
