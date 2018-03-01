using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {
    [SerializeField] float mouseSens = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSens);
	}
}
