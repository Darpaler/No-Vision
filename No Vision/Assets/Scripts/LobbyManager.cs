using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Variables
    [Header("Login UI")]
    public GameObject uiLoginGameObject;

    [Header("Lobby UI")]
    public GameObject uiLobbyGameObject;

    [Header("Connection Status UI")]
    public GameObject uiConnectionStatusGameObject;
    public TextMeshProUGUI connectionStatusText;
    public bool showConnectionStatus = false;

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            uiLobbyGameObject.SetActive(true);

            uiConnectionStatusGameObject.SetActive(false);
            uiLoginGameObject.SetActive(false);
        }
        else
        {
            uiLobbyGameObject.SetActive(false);
            uiConnectionStatusGameObject.SetActive(false);

            uiLoginGameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uiConnectionStatusGameObject.activeSelf)
        {
            connectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
        }
    }
    #endregion

    #region UI Callback Methods
    public void OnEnterGameButtonClicked()
    {
        uiLobbyGameObject.SetActive(false);
        uiLoginGameObject.SetActive(false);

        uiConnectionStatusGameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnQuickMatchButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Game");
    }
    #endregion

    #region PHOTON Callback Methods
    public override void OnConnected()
    {
        Debug.Log("We connected to the Internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to Photon Server");


        uiLoginGameObject.SetActive(false);
        uiConnectionStatusGameObject.SetActive(false);


        uiLobbyGameObject.SetActive(true);
    }
    #endregion

}
