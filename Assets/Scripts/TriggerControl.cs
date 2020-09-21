using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class TriggerControl : MonoBehaviour {
	[SerializeField] GameObject player;
	// Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerContoreller>().onGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<PlayerContoreller>().onGround = false;
    }
    
}
