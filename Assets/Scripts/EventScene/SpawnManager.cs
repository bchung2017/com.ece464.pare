using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public string playerPrefabName;
    private bool isPlayerSpawned = false;
    public GameObject spawnIndicator;
    public GameObject spawnButton;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnIndicator, Vector3.zero, Quaternion.identity);
        spawnButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentEventObject.IsOriginSet() && !isPlayerSpawned)
        {
            spawnIndicator.transform.position = new Vector3(gameObject.transform.position.x, CurrentEventObject.origin.y, gameObject.transform.position.z);
        }
    }


    public void OnSpawnButtonPress()
    {
        PhotonNetwork.Instantiate(playerPrefabName, spawnIndicator.transform.position, Quaternion.identity);
        spawnIndicator.SetActive(false);
        
    }



    #region PHOTON Callback Methods
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Photon network connected and ready");
        }

    }
    #endregion
}
