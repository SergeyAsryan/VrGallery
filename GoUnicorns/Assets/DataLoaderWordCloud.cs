using UnityEngine;
using System.Collections;
using SimpleJSON;

public class DataLoaderWordCloud : MonoBehaviour {

    // Array of items, pull the "aws_url" for direct link to image
    public string url = "https://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=10&q=http://www.google.com/trends/hottrends/atom/feed?pn=p1";
    public GameObject lightPreFab;
    private int ballcount = 0;
    private JSONNode json;

    // Use this for initialization
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        Debug.Log(www.text);
        json = JSON.Parse(www.text);
        InvokeRepeating("CreateLightBall", 0, 3.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void CreateLightBall()
    {
        ballcount++;
        string ballname = "test-" + ballcount;
        ballname = json[ballcount]["digitalid"].Value;


        Debug.Log("Making Light Ball:" + ballname);
        Transform cameraTransform = Camera.main.gameObject.transform;
        Vector3 ballVector = cameraTransform.position + new Vector3(1, 1, 6);
        GameObject lightBall = Instantiate(lightPreFab, ballVector, cameraTransform.rotation) as GameObject;
        GameObject ballText = lightBall.transform.Find("BallText").gameObject;
        ballText.GetComponent<TextMesh>().text = ballname;

    }
}
