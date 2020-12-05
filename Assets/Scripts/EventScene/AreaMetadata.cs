using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMetadata : MonoBehaviour
{
    public Vector3 position {get; set;}
    public GameObject floor;

    void Start() {
        this.position = gameObject.transform.position;
    }
}
