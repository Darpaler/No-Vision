﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Variables
    [Header("UI"), SerializeField]
    private GameObject uiInformPanelGameObject;
    [SerializeField]
    private TextMeshProUGUI uiInformText;
    [SerializeField]
    private GameObject searchForGamesButtonGameObject;
    [SerializeField]
    private GameObject visionBlocker;

    [Header("Game Data")]
    public bool isControllingCamera = false;
    private int pickupsColllected = 0;
    [SerializeField]
    private int pickupsNeeded = 20;
    [SerializeField]
    private PickupSpawner pickupSpawner;

    #region Unity Callback Methods;
    // Start is called before the first frame update
    void Start()
    {
        visionBlocker.SetActive(false);
        uiInformPanelGameObject.SetActive(true);
        searchForGamesButtonGameObject.SetActive(true);
        uiInformText.text = "Click the button to search for a game.";
    }
    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        uiInformText.text = "Searching for available rooms...";
        PhotonNetwork.JoinRandomRoom();
        searchForGamesButtonGameObject.SetActive(false);
    }
    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        uiInformText.text = message;
        CreateAndJoinRoom();
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            uiInformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + "\nWaiting for player 2...";
            isControllingCamera = true;
            visionBlocker.SetActive(true);
            SpawnPickup();
        }
        else
        {
            uiInformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name;
            StartCoroutine(DeactivateAfterSeconds(uiInformPanelGameObject, 2.0f));
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uiInformText.text = "Other player joined to " + PhotonNetwork.CurrentRoom.Name + ".";

        StartCoroutine(DeactivateAfterSeconds(uiInformPanelGameObject, 2.0f));
    }
    #endregion

    #region Public Methods
    public void CollectPickup()
    {
        pickupsColllected++;
        if(pickupsColllected >= pickupsNeeded)
        {
            // TODO: Load victory screen.
        }
        else
        {
            ChangeGameMode();
            SpawnPickup();
        }
    }
    #endregion

    #region Private Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0, 1000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    IEnumerator DeactivateAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
    private void ChangeGameMode()
    {
        isControllingCamera = !isControllingCamera;
        visionBlocker.SetActive(isControllingCamera);
    }
    private void SpawnPickup()
    {
        if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
        {
            pickupSpawner.SpawnRandomPickup();
        }
    }

    #endregion
}
