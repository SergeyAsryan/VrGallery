using UnityEngine;

public class FormWordCloud : MonoBehaviour
{
    public GameObject childObject;
    public float size = 10.0f;
    public float objects = 10.0f;

    void Start()
    {
        Sphere(objects);
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

    private void Sphere(float points)
    {
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
        }
    }
}