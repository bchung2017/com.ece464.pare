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


public class EventCodeHandler : MonoBehaviour
{
    public static EventObject eventObject;
    public InputField input;

    void Start() {
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://pare-58bd5.firebaseio.com/");
        var se = new InputField.SubmitEvent();
        
        }

    public void onCodeSubmit() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //FIX THIS LIMITTOFIRST() LINE!!!:
        FirebaseDatabase.DefaultInstance.GetReference("events/" + input.text).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.Log("error");
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                string ss = snapshot.GetRawJsonValue().ToString();
                Debug.Log("Raw Json: " + ss);
                // dynamic data = JsonConvert.DeserializeObject<dynamic>(ss);
                dynamic data = JsonConvert.DeserializeObject(ss);
                // Debug.Log(data);
                // CurrentEventObject.numOfOrgs = data["numOfOrgs"];

                Debug.Log(data["numOfOrgs"].Value.GetType());
                CurrentEventObject.eventId = input.text;
                CurrentEventObject.numOfOrgs = data["numOfOrgs"].Value;
                CurrentEventObject.uid = data["uid"].Value;
                Debug.Log(data["orgs"].Count);
                foreach (JProperty org in (JToken)data["orgs"]) {
                    CurrentEventObject.AddOrg(org.Value.ToString());
                }
                CurrentEventObject.Display();
                SceneManager.LoadScene("EventScene");


            }
        });

    }
}

