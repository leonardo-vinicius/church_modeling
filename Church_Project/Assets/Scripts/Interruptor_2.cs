using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightonoff : MonoBehaviour
{

    //public GameObject txtToDisplay;             //display the UI text
    
    private bool PlayerInZone;                  //check if the player is in trigger

    public GameObject lightorobj;

    public AudioClip somBotao;

    private AudioSource aud;

    private void Start()
    {
        lightorobj.SetActive(!lightorobj.activeSelf);
        // PlayerInZone = false;                   //player not in zone       
        //txtToDisplay.SetActive(false);
    }

    private void Update()
    {   
        if (!GetComponent<AudioSource>())
        {
            aud = gameObject.AddComponent<AudioSource>();
        }


        if (Input.GetKeyDown(KeyCode.F) && PlayerInZone)           //if in zone and press F key
        {
            lightorobj.SetActive(!lightorobj.activeSelf);
            aud.clip = somBotao;
            aud.PlayOneShot(aud.clip);
            //gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("switch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")     //if player in zone
        {
            //txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
     }
    

    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            //txtToDisplay.SetActive(false);
        }
    }
}