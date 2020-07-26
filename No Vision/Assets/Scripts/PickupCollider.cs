using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickupCollider : MonoBehaviourPunCallbacks
{
    // Variables
    [SerializeField]
    private float pickupTime;
    private float currentTime = 0f;
    [SerializeField]
    private GameManager gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            photonView.RPC("OnPickup", RpcTarget.All);
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            currentTime += Time.deltaTime;
            if(currentTime >= pickupTime)
            {
                if (PhotonNetwork.LocalPlayer == PhotonNetwork.MasterClient)
                {
                    Debug.Log("Destroying");
                    PhotonNetwork.Destroy(other.gameObject);
                    photonView.RPC("OnPickup", RpcTarget.All);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            currentTime = 0f;
        }
    }

    [PunRPC]
    private void OnPickup()
    {
        Debug.Log("Collecting");
        currentTime = 0f;
        gameManager.CollectPickup();
    }
}
