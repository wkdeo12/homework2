using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Manager.man.Addsome();
        Manager.man.click();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
