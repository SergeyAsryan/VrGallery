using UnityEngine;

public class WordSelector : MonoBehaviour
{
    public Color highlighted = Color.black;
    public Color unhighlighted = Color.white;

    private GameObject currentWord;

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.rotation * Vector3.forward);
        RaycastHit hit;
        GameObject hitWord = null;

        if (Physics.Raycast(ray, out hit) && (hit.transform.gameObject.tag == "Word"))
        {
            hitWord = hit.transform.gameObject;
            if (currentWord != hitWord)
            {
                if (currentWord != null)
                {
                    // Unhighlight
                    TextMesh phraseText = currentWord.transform.GetComponent<TextMesh>();
                    phraseText.color = unhighlighted;
                }
                currentWord = hitWord;
                if (currentWord != null)
                {
                    // Highlight
                    TextMesh phraseText = hit.transform.GetComponent<TextMesh>();
                    phraseText.color = highlighted;
                }
            }
        }
        else
        {
            if (currentWord != null)
            {
                // Unhighlight
                TextMesh phraseText = currentWord.transform.GetComponent<TextMesh>();
                phraseText.color = unhighlighted;
                currentWord = null;
            }
        }
    }
}