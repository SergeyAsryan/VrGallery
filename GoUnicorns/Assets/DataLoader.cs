using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Threading;

public class DataLoader : MonoBehaviour {

    // Array of items, pull the "aws_url" for direct link to image
    public string url = "https://www.bitstamp.net/api/v2/transactions/btcusd/";
    public GameObject lightPreFab;
    private int ballcount = 0;
    private JSONNode json;
    private AudioClip sellAudio;
    private AudioClip buyAudio;
    private AudioClip buyAudioAlt;

    private WWW theInternet;

    // Use this for initialization
    //IEnumerator Start()
    //{
    //    sellAudio = (AudioClip)Resources.Load("Sound/sell", typeof(AudioClip));
    //    buyAudio = (AudioClip)Resources.Load("Sound/buy1", typeof(AudioClip));
    //    buyAudioAlt = (AudioClip)Resources.Load("Sound/buy2", typeof(AudioClip));

    //    WWW www = new WWW(url);
    //    yield return www;
    //    Debug.Log(www.text);
    //    json = JSON.Parse(www.text);
    //    Invoke("CreateLightBall", 1.0f);
    //    Debug.Log("Count is: " + json.Count);
    //}

    private bool isFirst = false;
    void Start()
    {
        sellAudio = (AudioClip)Resources.Load("Sound/sell", typeof(AudioClip));
        buyAudio = (AudioClip)Resources.Load("Sound/buy1", typeof(AudioClip));
        buyAudioAlt = (AudioClip)Resources.Load("Sound/buy2", typeof(AudioClip));
        isFirst = true;
        theInternet = new WWW(url);
    }

    void ReloadData()
    {
        Debug.Log(theInternet.text);
        json = JSON.Parse(theInternet.text);
        Invoke("CreateLightBall", 1.0f);
        Debug.Log("Count is: " + json.Count);
        theInternet.Dispose();
        theInternet = null;
    }

    // Update is called once per frame
    void Update()
    {
        if( theInternet != null)
        {
            if (theInternet.isDone)
            {
                ReloadData();
            }
        }

        if (ballcount > 0 && (ballcount == (json.Count - 1)))
        {
            ballcount = 0;
            Debug.Log("Reloading...");
            theInternet = new WWW(url);

        }
    }

    void CreateLightBall()
    {
        ballcount++;
        string ballname = "test-" + ballcount;
        ballname = json[ballcount]["amount"].Value.Substring(0, 5);

        Transform cameraTransform = Camera.main.gameObject.transform;
        Vector3 ballVector = cameraTransform.position + new Vector3(1, 1, 15);
        GameObject lightBall = Instantiate(lightPreFab, ballVector, cameraTransform.rotation) as GameObject;
        GameObject ballText = lightBall.transform.Find("BallText").gameObject;

        // Scale based off the size of the transaction
        float ballscale = float.Parse(ballname) * 10;
        lightBall.transform.localScale += new Vector3(ballscale, ballscale, ballscale);
        ballText.GetComponent<TextMesh>().text = ballname;

        // Play either a buy or sell sound
        AudioClip playSound;
        if (json[ballcount]["type"].Value == "1")
        {
            playSound = sellAudio;
        } else
        {
            playSound = buyAudio;
            if (Random.value <= 0.5f)
            {
                playSound = buyAudioAlt;
            }

        }
        float soundVolume = Mathf.Clamp(ballscale, 4.0F, 10F);
        AudioSource.PlayClipAtPoint(playSound, transform.position, soundVolume);

        // Invoke when the next item should be displayed
        if (ballcount < (json.Count -1))
        {
            int intTime = int.Parse(json[ballcount]["date"].Value) - int.Parse(json[ballcount +1]["date"].Value);
            float nextTime = intTime / 60;
            Invoke("CreateLightBall", nextTime);
        } 
        

        Destroy(lightBall, 8);
    }
}
