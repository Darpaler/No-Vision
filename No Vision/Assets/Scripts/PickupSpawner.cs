using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickupSpawner : MonoBehaviourPunCallbacks
{
    // Variables
    [SerializeField]
    private GameObject pickup;
    [SerializeField]
    private float spawnDistance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomPickup();
        }
    }

    public void SpawnRandomPickup()
    {
        Vector3 spawnVector;
        do
        {
            spawnVector = Random.onUnitSphere * 7;
            spawnVector.y = Random.Range(1.5f, 3.5f);
            Debug.Log(spawnVector);
        }
        while (Vector3.Distance(Vector3.zero, spawnVector) < spawnDistance);
        pickup.transform.position = spawnVector;
        Instantiate(pickup, transform, false);
        //PhotonNetwork.Instantiate(pickup.name, );
    }

}
