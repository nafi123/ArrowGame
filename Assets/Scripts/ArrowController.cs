using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {

    [SerializeField] GameObject effect;
    [SerializeField] Text scoreValueText;

   
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if(!(colision.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
            if (colision.gameObject.CompareTag("Enemy"))
            {
                Destroy(colision.gameObject);
                Instantiate(effect, colision.gameObject.transform.position, Quaternion.identity);
                GameObject.Find("Level Menager").GetComponent<LevelManager>().AddScore(100);
                
               
            }
        }
        

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
