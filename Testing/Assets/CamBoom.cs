using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBoom : MonoBehaviour {
    [SerializeField] GameObject lerpTarget;
    [SerializeField] float lerpSpeed = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, lerpTarget.transform.rotation, lerpSpeed);
	}
}
