using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public Image IMGboard;
//public Image ProgressBAR, LocationBAR, XPBAR;
//public Text textName, textPreogressBar;

 

public class UIscript : MonoBehaviour {



    public GameObject PauseUI;
    private bool Menu_pause = false;

	// Use this for initialization
	void Start () {
        PauseUI.SetActive(false);

        

	}
    
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetButtonDown("Start_Buttom"))
        {
            Menu_pause = !Menu_pause;
        }
        if (Menu_pause)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!Menu_pause)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
       

    }
   
    
    // creamos el menu y sus funciones
    public void Resume()
    {
        Menu_pause = false;
    }

    public void Restart()
    {
         
    }

    public void Menu()
    {
        
    }

    public void Exit()
    {

    }
}
