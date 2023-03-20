using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class basicMain : MonoBehaviour
{
    public Button Hello;                //Hello 버튼 선언
    public string host;                 //주소 변수 선언
    public int port;                    //포트 번호 선언 (0 ~ 25000);

    private void Start()
    {
        this.Hello.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/", host, port);    //string.format 함수로 URL 생성
            Debug.Log(url);
            StartCoroutine(this.GetBasic(url, (raw) =>          //코루틴으로 GetBasic 호출 후에 raw 파일을 콜백에 넣는다.
            {
                Debug.LogFormat("{0}", raw);                    //디버그 로그로 raw 파일 설정
            }));
        });
    }

    private IEnumerator GetBasic(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);          //Get에 대한 웹 요청 URL 함수
        yield return webRequest.SendWebRequest();           //요청한 것이 돌아올 때까지 대기

        Debug.Log("----->" + webRequest.downloadHandler.text);      //다운로드 핸들러 텍스트 로깅

        if(webRequest.result == UnityWebRequest.Result.ConnectionError //접속 커넥션에 문제가 있을 때
           || webRequest.result == UnityWebRequest.Result.ProtocolError) // 프로토콜에 문제가 있을 때
        {
            Debug.LogError("네트워크 환경이 좋지 않아 통신 불가능");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }
}
