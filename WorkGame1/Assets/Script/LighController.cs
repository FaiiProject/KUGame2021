using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighController : MonoBehaviour
{
    public Light Light;
    public string lightName = "Bed Room";

    private void Awake()
    {
        print("Hello Awake!");
    }

    private void OnMouseDown()
    {
        print("Mouse Down");
        if (Light.enabled == true)
        {
            Light.enabled = false;
        }
        else
        {
            Light.enabled = true;
        }
    }
    /*private void OnMouseDown()
    {
        print("Mouse Down!");
        light1.enabled = false;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        print("Hello " +lightName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
