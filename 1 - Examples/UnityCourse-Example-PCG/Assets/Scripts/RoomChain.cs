using System;
using UnityEngine;


public class RoomChain : MonoBehaviour
{

    [SerializeField] private RoomStereotype _startRoom;

    private RoomStereotype _currentRoom;

    private void Start()
    {
        _currentRoom = _startRoom;
    }

    private void OnTriggerEnter(Collider other)
    {
        NextRoom();
    }

    private void NextRoom()
    {
        _currentRoom = _currentRoom.NextRoom();
        Debug.Log(_currentRoom.name);
        // Chargement de la salle
        // Spawn des squelettes
    }

}
