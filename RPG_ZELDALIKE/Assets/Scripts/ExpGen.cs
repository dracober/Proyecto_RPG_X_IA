using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class ExpGen : MonoBehaviour {
    public int nivel;
    //barra de reputacion
    public Slider REP_Slider;
    public int rep_xp;

    // barra de nivel
    public int lvl_xp;
    public Slider LVL_Slider;
    // Use this for initialization
    void Start () {
        LVL_Slider.maxValue = Needed_XP(nivel);
    }
    // vacio para  obtener la experiencia
    public void GetXP(int value)
    {
        LVL_Slider.maxValue = Needed_XP(nivel);
        lvl_xp += value;
        Debug.Log(value);

    }

    // Update is called once per frame
    void Update () {
		
	}
    // generador de experiencia
    int Needed_XP(int lvl)
    {
        return 50 + (lvl * lvl);
    }

}
