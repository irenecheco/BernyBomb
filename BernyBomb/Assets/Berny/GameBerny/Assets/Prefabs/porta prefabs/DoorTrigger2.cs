﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger2 : MonoBehaviour
{
    [SerializeField] private Door _door;
    public AudioSource doorOpen;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 othersPositionRelativeToDoor = (other.transform.position - transform.position).normalized;
        
        //the result of the dot product returns > 0 if relative position 
        float dotResult = Vector3.Dot(othersPositionRelativeToDoor, transform.forward);
        

        float doorRotation = dotResult > 0 ? 90f : -90f;

        if (_door != null)
            _door.OpenDoor(doorRotation);
        doorOpen.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_door != null)
            _door.CloseDoor();
    }
}
