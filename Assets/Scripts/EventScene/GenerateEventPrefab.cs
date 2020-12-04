using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GenerateEventPrefab : MonoBehaviour
{
    public GameObject areaObject;
    public GameObject eventObject;
    public GameObject exhibitObject;
    public TextMeshPro tm1;
    [SerializeField]
    private int orgsPerArea;
    private IEnumerator GetData;
    void Start()
    {
        CurrentEventObject.AddOrg("test2");
        CurrentEventObject.AddOrg("test3");
        CurrentEventObject.AddOrg("test4");

        GameObject ev = Instantiate(eventObject, Vector3.zero, Quaternion.identity);
        int numOfAreas = (CurrentEventObject.orgs.Count - 1) / orgsPerArea + 1;
        for (int i = 0; i < numOfAreas; i++)
        {
            GameObject a = Instantiate(areaObject, CurrentEventObject.positions[i], Quaternion.identity);
            for (int j = 0; j < orgsPerArea; j++)
            {
                int currentIndex = i * orgsPerArea + j;
                // Debug.Log("currentIndex: " + currentIndex);
                // Debug.Log("orgs.Count: " + CurrentEventObject.orgs.Count);
                if (currentIndex >= CurrentEventObject.orgs.Count)
                {
                    break;
                }
                string currentOrgName = CurrentEventObject.orgs[currentIndex];
                getElements(currentOrgName, snapshot =>
                {
                    Debug.Log("Snapshot retrieved!");
                    UnityMainThreadDispatcher.Instance().Enqueue(CreateExhibit(snapshot, a, j));


                });
            }
            a.transform.SetParent(ev.transform);
        }
    }

    public void getElements(string currentOrgName, System.Action<DataSnapshot> onSnapshotRetrieved)
    {
        Debug.Log("current orgName: " + currentOrgName);
        Query q = FirebaseDatabase.DefaultInstance.GetReference("orgs").OrderByChild("orgName").EqualTo(currentOrgName);
        q.KeepSynced(true);
        q.GetValueAsync().ContinueWith(task =>
        {

            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("number of children: " + task.Result.GetRawJsonValue());
                onSnapshotRetrieved(task.Result);
            }
        });
    }

    private IEnumerator CreateExhibit(DataSnapshot snapshot, GameObject area, int j)
    {
        string ss = snapshot.GetRawJsonValue().ToString();
        Debug.Log("Raw Json: " + ss);
        dynamic data = JsonConvert.DeserializeObject(ss);
        dynamic data2 = data.First.Value;
        Debug.Log("data2 orgDesc: " + data2["orgDesc"].Value.ToString());
        string t = data2["orgName"].Value.ToString();
        string d = data2["orgDesc"].Value.ToString();
        string u = data2["imgUrl"].Value.ToString();
        Debug.Log(t);
        Debug.Log(j);
        Debug.Log("position: " + CurrentAreaObject.positions[j]);
        GameObject _exhibit = Instantiate(exhibitObject, CurrentAreaObject.GetNextPos(), Quaternion.identity);
        _exhibit.GetComponent<ExhibitMetadata>().SetupMetaData(t, d, u);
        _exhibit.transform.SetParent(area.transform, false);
        yield return null;
    }




    // Update is called once per frame
    void Update()
    {

    }
}
