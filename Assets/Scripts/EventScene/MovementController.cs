using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject ARCamera;
    // Start is called before the first frame update
    void Start()
    {
        ARCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = ARCamera.transform.position;
    }
}
