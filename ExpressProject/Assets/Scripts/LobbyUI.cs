using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public Button BtnLogin;
    public Button BtnLobby;
    public Button BtnSceneCheck;

    private void Start()
    {
        BtnLogin.onClick.AddListener(() => StartCoroutine(SceneManager.Instance.LoadSceneButton("Login")));
        BtnSceneCheck.onClick.AddListener(() => GetSceneName());
    }

    void GetSceneName()
    {
        BaseScene Temp = SceneManager.Instance.GetScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        Debug.Log(Temp.sceneName + "/" + Temp.sceneIndex + "/" + Temp.sceneData0);
    }
}
