using UnityEngine;

public class HandTester : MonoBehaviour
{
    public float speed = 2.0f;
    public float radius = 0.5f;
    public bool clockwise = true;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float direction = clockwise ? -1f : 1f;
        
        // Calculate circular movement using Sine and Cosine
        // $$ x = \cos(t), y = \sin(t) $$
        float x = Mathf.Cos(Time.time * speed) * radius;
        float y = Mathf.Sin(Time.time * speed) * radius * direction;

        transform.position = startPos + new Vector3(x, y, 0);
    }
}