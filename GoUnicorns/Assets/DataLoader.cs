using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {

    // Array of items, pull the "aws_url" for direct link to image
    public string url = "http://stereo.nypl.org/gallery/popular/1.json";
    public GameObject lightPreFab;
    private int ballcount = 0;

    // Use this for initialization
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        Debug.Log(www.text);
        InvokeRepeating("CreateLightBall", 0, 3.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

    void CreateLightBall()
    {
        ballcount++;
        string ballname = "test-" + ballcount;
        
        Debug.Log("Making Light Ball:" + ballname);
        Transform cameraTransform = Camera.main.gameObject.transform;
        Vector3 ballVector = cameraTransform.position + new Vector3(1, 1, 6);
        GameObject lightBall = Instantiate(lightPreFab, ballVector, cameraTransform.rotation) as GameObject;
        GameObject ballText = lightBall.transform.Find("BallText").gameObject;
        ballText.GetComponent<TextMesh>().text = ballname;

    }
}
