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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            currentTime += Time.deltaTime;
            if(currentTime >= pickupTime)
            {
                if (gameManager.isControllingCamera)
                {
                    Destroy(other.gameObject);
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
        gameManager.CollectPickup();
    }
}
