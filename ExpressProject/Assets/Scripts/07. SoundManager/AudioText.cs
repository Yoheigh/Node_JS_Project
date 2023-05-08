using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioText : MonoBehaviour
{
    [SerializeField] protected SoundProfileData soundProfileData;
    [SerializeField] protected SoundProfileData soundProfileBGMData;

    private AudioManager audioManager => AudioManager.Instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlayMusic(soundProfileData.GetRandomClip());
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            audioManager.PlayMusic(soundProfileBGMData.GetRandomClip());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            audioManager.FadeInMusic(soundProfileBGMData.GetClipIndex(0), 3.0f);
        }
    }
}
