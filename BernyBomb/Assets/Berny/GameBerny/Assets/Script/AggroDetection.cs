using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggroDetection : MonoBehaviour
{
    [SerializeField]
    private AudioSource fightSong;
    [SerializeField]
    private AudioSource defaultSong;
    public event Action<Transform> OnAggro = delegate { };
     
    private void OnTriggerEnter(Collider other)
    {

        var player = other.GetComponent<Player>();
        
        if(player != null)
        {
            OnAggro(player.transform);
            Debug.Log("Aggro");
            if(defaultSong.isPlaying == true)
            {
                fightSong.Play();
                defaultSong.Stop();
            }
            

            
        }
    }

}

