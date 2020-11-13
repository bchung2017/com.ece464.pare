using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GenerateRoomPrefab : MonoBehaviour
{
    public GameObject desk;
     public TextMeshPro tm1;
    // Start is called before the first frame update
    void Start()
    {
        CurrentEventObject.AddOrg("org1");
        List<OrganizationClass> organizations = new List<OrganizationClass>();
        foreach (string orgName in CurrentEventObject.orgs)
        {
            Debug.Log(orgName);
            FirebaseDatabase.DefaultInstance.GetReference("organizations").OrderByChild("orgName").EqualTo(orgName).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("error");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string ss = snapshot.GetRawJsonValue().ToString();
                    Debug.Log("Raw Json: " + ss);
                    dynamic data = JsonConvert.DeserializeObject(ss);
                    dynamic data2 = data.First.Value;
                    Debug.Log("data2 orgDesc: " + data2["orgDesc"].Value.ToString());
                    // OrganizationClass currentOrg = new OrganizationClass();
                    // currentOrg.orgName = data2["orgName"].Value.ToString();
                    // currentOrg.orgDesc = data2["orgDesc"].Value.ToString();
                    // tm1 = desk.GetComponentInChildren<TextMeshPro>();
                    tm1.SetText(data2["orgDesc"].Value.ToString());
                    // Instantiate(desk, new Vector3(0, 0, 0), Quaternion.identity);
                }
            });
        }





    }

    // Update is called once per frame
    void Update()
    {

    }
}
