using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SelectRoom : MonoBehaviour
{
    public Camera cam;
    List<FloorBehavior> floors = new List<FloorBehavior>();
    int layerMask;
    public GameObject button;
    int select = 0;
    bool unselect = false;

    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Button>().onClick.AddListener(ToggleSelect);
        floors = FindObjectsOfType<FloorBehavior>().ToList();
        layerMask = 1 << 9;


        GameObject[] areas = GameObject.FindGameObjectsWithTag("area");
        foreach (GameObject area in areas)
        {
            Room room = new Room(area, area.transform.position);
            CurrentEventObject.roomPos.Add(room);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            floors = FindObjectsOfType<FloorBehavior>().ToList();
        }

        Vector3 cameraPoint = cam.transform.position;

        if (Physics.Raycast(cameraPoint, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("hit detected");
            GameObject obj = hit.collider.gameObject;
            if (obj.CompareTag("floor"))
            {
                TurnOn(obj.GetComponent<FloorBehavior>());

                if (select == 1)
                {
                    foreach (Room r in CurrentEventObject.roomPos)
                    {
                        Debug.Log("instance id of object: " + obj.transform.parent.parent.gameObject.GetInstanceID());
                        Debug.Log("r instance id: " + r.room.GetInstanceID());
                        if (r.room.GetInstanceID() == obj.transform.parent.parent.gameObject.GetInstanceID())
                        {
                            r.room.transform.position = CurrentEventObject.origin;
                            r.room.transform.localScale = new Vector3(7, 7, 7);
                        }
                        else
                        {
                            r.room.SetActive(false);
                        }
                    }
                }


                if (select == 2)
                {
                    foreach (Room r in CurrentEventObject.roomPos)
                    {
                        r.room.transform.position = r.ogPos;
                        r.room.transform.localScale = new Vector3(1, 1, 1);
                        r.room.SetActive(true);
                    }    
                    select = 0;
                }


                // if (unselect)
                // {
                //      foreach (GameObject area in areafloors)
                //     {
                //         CurrentEventObject.roomPos.Add(area.GetInstanceID(), area.transform.parent.gameObject.transform.parent.gameObject.transform.position);
                //         if (area.GetInstanceID() == obj.GetInstanceID())
                //         {
                //             // int value; 
                //             // CurrentEventObject.roomPos.TryGetValue(obj.GetInstanceID(), out value);
                //             CurrentRoomObject.locationKey = obj.GetInstanceID();
                //         }
                //         else 
                //         {
                //             area.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
                //         }
                //     }    
                //     obj.transform.parent.gameObject.transform.parent.gameObject.transform.position = CurrentEventObject.origin;
                //     obj.transform.parent.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(7, 7, 7);
                //     select = false;
                // }
                // else {
                //     obj.transform.parent.gameObject.transform.parent.gameObject.transform.position = CurrentOrganizationObject.orgPos;
                //     CurrentOrganizationObject.isBuilt = false;
                //     obj.transform.parent.gameObject.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1);
                //     foreach (GameObject area in areafloors)
                //     {
                //         area.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
                //     }  
                // }
            }
        }
        else
        {
            TurnOffAll();
        }

    }

    void TurnOn(FloorBehavior floor)
    {
        floor.TurnOn();
    }

    void TurnOff(FloorBehavior floor)
    {
        floor.TurnOff();
    }

    void TurnOffAll()
    {
        foreach (FloorBehavior floor in floors)
        {
            TurnOff(floor);
        }
    }

    void ToggleSelect()
    {
        select++;
        Debug.Log(select);
    }



    // public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    // {
    //     transform.localScale = Vector3.one;
    //     transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    // }
}
