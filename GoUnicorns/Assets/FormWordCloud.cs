using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using LitJson;

public class Phrase
{
    public string term;
    public float occurrences;
}

public class FormWordCloud : MonoBehaviour
{
    public GameObject childObject;
    public float size = 10.0f;
    private List<Phrase> phrases = new List<Phrase>();

    public string urlGoogle = "https://ajax.googleapis.com/ajax/services/feed/load?v=1.0&num=20&q=http://www.google.com/trends/hottrends/atom/feed?pn=p1";
    private string jsonGoogle = "{\"responseData\": {\"feed\":{\"feedUrl\":\"http://www.google.com/trends/hottrends/atom/feed?pn\u003dp1\",\"title\":\"Hot Trends\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1\",\"author\":\"\",\"description\":\"Recent hot searches\",\"type\":\"rss20\",\"entries\":[{\"title\":\"Anthony Kiedis\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160515-Anthony+Kiedis\",\"author\":\"\",\"publishedDate\":\"Sun, 15 May 2016 04:00:00 -0700\",\"contentSnippet\":\"Red Hot Chili Peppers\",\"content\":\"Red Hot Chili Peppers\",\"categories\":[]},{\"title\":\"Manchester United\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160515-Manchester+United\",\"author\":\"\",\"publishedDate\":\"Sun, 15 May 2016 08:00:00 -0700\",\"contentSnippet\":\"Old Trafford\",\"content\":\"Old Trafford\",\"categories\":[]},{\"title\":\"Bay to Breakers 2016\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160515-Bay+to+Breakers+2016\",\"author\":\"\",\"publishedDate\":\"Sun, 15 May 2016 08:00:00 -0700\",\"contentSnippet\":\"Bay to Breakers\",\"content\":\"Bay to Breakers\",\"categories\":[]},{\"title\":\"Pentecost\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160515-Pentecost\",\"author\":\"\",\"publishedDate\":\"Sun, 15 May 2016 04:00:00 -0700\",\"contentSnippet\":\"\",\"content\":\"\",\"categories\":[]},{\"title\":\"Formula 1\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160515-Formula+1\",\"author\":\"\",\"publishedDate\":\"Sun, 15 May 2016 10:00:00 -0700\",\"contentSnippet\":\"F1\",\"content\":\"F1\",\"categories\":[]},{\"title\":\"UFC 198\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160514-UFC+198\",\"author\":\"\",\"publishedDate\":\"Sat, 14 May 2016 10:00:00 -0700\",\"contentSnippet\":\"UFC, UFC 198 results, Stipe Miocic, Stipe Miocic Vs Fabricio Werdum, UFC 198 fight card, Werdum vs Miocic\",\"content\":\"UFC, UFC 198 results, Stipe Miocic, Stipe Miocic Vs Fabricio Werdum, UFC 198 fight card, Werdum vs Miocic\",\"categories\":[]},{\"title\":\"Barcelona\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160514-Barcelona\",\"author\":\"\",\"publishedDate\":\"Sat, 14 May 2016 09:00:00 -0700\",\"contentSnippet\":\"La Liga, FC Barcelona\",\"content\":\"La Liga, FC Barcelona\",\"categories\":[]},{\"title\":\"Eurovision 2016\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160514-Eurovision+2016\",\"author\":\"\",\"publishedDate\":\"Sat, 14 May 2016 13:00:00 -0700\",\"contentSnippet\":\"Eurovision\",\"content\":\"Eurovision\",\"categories\":[]},{\"title\":\"Rose Leslie\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160514-Rose+Leslie\",\"author\":\"\",\"publishedDate\":\"Sat, 14 May 2016 06:00:00 -0700\",\"contentSnippet\":\"Kit Harington\",\"content\":\"Kit Harington\",\"categories\":[]},{\"title\":\"OJ Simpson\",\"link\":\"http://www.google.com/trends/hottrends?pn\u003dp1#a\u003d20160514-OJ+Simpson\",\"author\":\"\",\"publishedDate\":\"Sat, 14 May 2016 01:00:00 -0700\",\"contentSnippet\":\"\",\"content\":\"\",\"categories\":[]}]}}, \"responseDetails\": null, \"responseStatus\": 200}";

    private string jsonString = "[{\"term\":\"the\", \"occurrences\":504},{\"term\":\"to\",\"occurrences\":447},{\"term\":\"rt\",\"occurrences\":433},{\"term\":\"a\",\"occurrences\":382},{\"term\":\"in\",\"occurrences\":299},{\"term\":\"of\",\"occurrences\":274},{\"term\":\"adventure\",\"occurrences\":236},{\"term\":\"and\",\"occurrences\":216},{\"term\":\"for\",\"occurrences\":166},{\"term\":\"is\",\"occurrences\":157},{\"term\":\"on\",\"occurrences\":154},{\"term\":\"cars\",\"occurrences\":136},{\"term\":\"it\",\"occurrences\":122},{\"term\":\"you\",\"occurrences\":116},{\"term\":\"with\",\"occurrences\":100},{\"term\":\"from\",\"occurrences\":87},{\"term\":\"at\",\"occurrences\":85},{\"term\":\"i\",\"occurrences\":85},{\"term\":\"this\",\"occurrences\":85},{\"term\":\"that\",\"occurrences\":83}]";


    // Use this for initialization
    IEnumerator Start()
    {
        WWW www = new WWW(urlGoogle);
        yield return www;
        Debug.Log(www.text);
        jsonGoogle = www.text;
        loadGoogle();
        Sphere();
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;

        // Tell each of the objects to look at the camera
        foreach (Transform child in transform)
        {
            child.LookAt(camera.position);
            child.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void loadGoogle()
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonGoogle);
        JsonData entries = jsonvale["responseData"]["feed"]["entries"];
        for (int i = 0; i < entries.Count; i++)
        {
            Phrase phrase = new Phrase();
            phrase.term = entries[i]["title"].ToString();
            phrase.occurrences = 1;
            phrases.Add(phrase);
        }
    }

    private void Sphere()
    {
        float points = phrases.Count;
        float increment = Mathf.PI * (3 - Mathf.Sqrt(5));
        float offset = 2 / points;
        for (float i = 0; i < points; i++)
        {
            float y = i * offset - 1 + (offset / 2);
            float radius = Mathf.Sqrt(1 - y * y);
            float angle = i * increment;
            Vector3 pos = new Vector3((Mathf.Cos(angle) * radius * size), y * size, Mathf.Sin(angle) * radius * size);

            // Create the object as a child of the sphere
            GameObject child = Instantiate(childObject, pos, Quaternion.identity) as GameObject;
            child.transform.parent = transform;
            TextMesh phraseText = child.transform.GetComponent<TextMesh>();
            phraseText.text = phrases[(int)i].term;
        }
    }

    private void ProcessWords(string jsonString)
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        for (int i = 0; i < jsonvale.Count; i++)
        {
            Phrase phrase = new Phrase();
            phrase.term = jsonvale[i]["term"].ToString();
            phrase.occurrences = float.Parse(jsonvale[i]["occurrences"].ToString());
            phrases.Add(phrase);
        }
    }
}