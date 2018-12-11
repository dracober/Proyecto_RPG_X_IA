using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    [SerializeField] GameObject msgPanel;
    

	// Use this for initialization
	void Start () {
        GetComponent<Collider2D>().isTrigger = true;
        msgPanel.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            print("Hit NPC");
            msgPanel.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            
            msgPanel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
