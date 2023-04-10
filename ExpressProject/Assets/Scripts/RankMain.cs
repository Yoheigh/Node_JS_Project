using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;           //코이츠 맨날 있는
using Newtonsoft.Json;
using System.Text;

public class RankMain : MonoBehaviour
{
    public string host;
    public int port;
    public string top3Uri;
    public string idUri;
    public string postUri;
    public string id;
    public string pw;               // 추가
    public int score;

    public string registerIDUri;    // 추가

    public Button BtnGetTop3;
    public Button BtnGetId;
    public Button BtnPost;
    public Button BtnRegisterID;    // 추가

    Dictionary<string, int> scoreDic = new Dictionary<string, int>();

    private void Start()
    {
        this.BtnRegisterID.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, registerIDUri);
            Debug.Log(url);

            var req = new Protocols.Packets.req_registerid();
            req.cmd = 1000;
            req.userid = new Protocols.Packets.userid();
            req.userid.id = id;
            req.userid.pw = pw;

            var Json = JsonUtility.ToJson(req);

            StartCoroutine(this.PostRegister(url, Json, (raw) =>
            {
                var res = JsonUtility.FromJson<Protocols.Packets.res_registerid>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });

        this.BtnGetId.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, idUri + "/" + id);
            Debug.Log(url);

            StartCoroutine(this.GetId(url, (raw) =>
            {
                var res = JsonUtility.FromJson<Protocols.Packets.res_scores_id>(raw);
                Debug.LogFormat("{0}, {1}", res.result.id, res.result.score);
            }));
        });

        this.BtnGetTop3.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, top3Uri);
            Debug.Log(url);

            StartCoroutine(this.GetTop3(url, (raw) =>
            {
                //var res = JsonConvert.DeserializeObject<Protocols.Packets.res_scores_top3>(raw);
                Protocols.Packets.res_scores_top3 res = JsonUtility.FromJson<Protocols.Packets.res_scores_top3>(raw);

                Debug.LogFormat("{0}, {1}", res.cmd, res.result.Length);
                foreach (var user in res.result)
                {
                    Debug.LogFormat("{0} : {1}", user.id, user.score);
                }

            }));
        });

        this.BtnPost.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, postUri);
            Debug.Log(url);

            var req = new Protocols.Packets.req_scores();
            req.cmd = 1000;
            req.id = id;
            req.score = score;
            var json = JsonUtility.ToJson(req);
            Debug.Log(json);

            StartCoroutine(this.PostScore(url, json, (raw) =>
            {
                Protocols.Packets.res_scores res = JsonUtility.FromJson<Protocols.Packets.res_scores>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });
    }

    private IEnumerator PostScore(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);                         //직렬화 (문자열 -> 바이트 배열)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();                           //Node.js로 req 전송

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크가 너무 구립니다");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}\n", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
        webRequest.uploadHandler.Dispose();
    }

    private IEnumerator GetId(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("----->" + webRequest.downloadHandler.text);

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크가 너무 구려서 id도 못 가져오겠습니다");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator GetTop3(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||                  //각종 네트워크 에러 사항 채킹
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않음");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostRegister(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);                         //직렬화 (문자열 -> 바이트 배열)

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();                           //Node.js로 req 전송

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
           webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크가 너무 구립니다");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}\n", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
        webRequest.uploadHandler.Dispose();
    }
}
