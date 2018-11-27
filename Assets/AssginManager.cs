using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AssginManager : MonoBehaviour {

    
    public Button Startbutton; 
    
    public GameObject e;

	void Start () {
	}

    void Update()
    {
	}

    public void startbutton()
    {
        Startbutton.gameObject.SetActive(false);
        e.gameObject.SetActive(true);
    }
    



}
