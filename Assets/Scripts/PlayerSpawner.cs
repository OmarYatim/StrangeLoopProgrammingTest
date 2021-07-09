using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform PlayerParent;


    private void Start()
    {
        GameObject Player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity, PlayerParent);
        Player.transform.localPosition = Vector3.zero;
    }
}
