using System;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class VictoryUIManager : MonoBehaviour
{
    // Variables
    [SerializeField]
    private TextMeshProUGUI victoryText;
    private TimeSpan timeSpan;

    // Start is called before the first frame update
    void Start()
    {
        timeSpan = TimeSpan.FromSeconds(Timer.Instance.time);
        victoryText.text = "Congratulations,\nYou Won!\n\nScore: " + string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Lobby");
        PhotonNetwork.Disconnect();
    }
}
