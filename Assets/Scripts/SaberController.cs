using System.Collections.Specialized;
using UnityEngine;

public class SaberController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 10f; // distance between mouse and camera
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
        transform.position = new Vector3(world.x, world.y, 0);
    }
}
