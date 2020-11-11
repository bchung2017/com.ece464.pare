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
    public EventObject eventObject;
    public InputField input;

    void Start() {
        // FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://pare-58bd5.firebaseio.com/");
        var se = new InputField.SubmitEvent();
        }

    public void onCodeSubmit() {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance.GetReference("events").LimitToFirst(1).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.Log("error");
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                string ss = snapshot.GetRawJsonValue().ToString();
                Debug.Log("Raw Json: " + ss);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(ss);
                var list = new List<EventObject>();
                foreach (var itemDynamic in data)
                {
                    EventObject temp_result = JsonConvert.DeserializeObject<EventObject>(((JProperty)itemDynamic).Value.ToString());
                    Debug.Log(input.text);
                    Debug.Log(temp_result.eventId); 
                    if(temp_result.eventId == input.text) {
                        Debug.Log("Loading scene...");  
                        eventObject = temp_result;
                        SceneManager.LoadScene("EventScene");
                    }
                }

            }
        });

    }
}

