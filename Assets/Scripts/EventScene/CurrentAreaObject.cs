using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentAreaObject
{
    public static Vector3 pos { get; set; }
    public static bool isBuilt { get; set; }
    private static Vector3 _orgPos;
    private static int posIndex { get; set; }

    public static Vector3[] positions = new Vector3[] {
        new Vector3 (-1,0,1),
        new Vector3 (1,0,1),
        new Vector3 (1,0,-1),
        new Vector3 (-1,0,-1)
    };

    public static Vector3 orgPos
    {
        get
        {
            return _orgPos;
        }
        set
        {
            if (!isBuilt)
                _orgPos = value;
        }
    }

    static CurrentAreaObject()
    {
        pos = Vector3.zero;
        orgPos = Vector3.zero;
        posIndex = 0;
    }

    public static Vector3 GetNextPos()
    {
        Vector3 nextPos = positions[posIndex++];
        if(posIndex == 4)
            posIndex = 0;
        return nextPos;
    }
}
