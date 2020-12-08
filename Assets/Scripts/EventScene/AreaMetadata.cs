using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMetadata : MonoBehaviour
{
    public Vector3 position { get; set; }
    public GameObject floor;
    private int posIndex { get; set; }

    public static Vector3[] positions = new Vector3[] {
        new Vector3 (-1,0,1),
        new Vector3 (1,0,1),
        new Vector3 (1,0,-1),
        new Vector3 (-1,0,-1)
    };

    public Vector3 GetNextPos()
    {
        Debug.Log("posIndex: " + posIndex);
        Vector3 nextPos = positions[posIndex];
        posIndex++;
        if (posIndex == 4)
            posIndex = 0;
        return nextPos;
    }

    void Start()
    {
        this.position = gameObject.transform.position;
        this.posIndex = 0;
    }
}
