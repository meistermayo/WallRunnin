using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {
    [SerializeField] float mouseSens = 1f;
    [SerializeField] float lerpTime;
    Quaternion targetRotation;
	// Use this for initialization
	void Start () {
        targetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        targetRotation *= Quaternion.Euler(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSens);

	}

    public void SetTargetRotation(Quaternion rotation)
    {
        targetRotation = rotation;
        Quaternion.Lerp(transform.rotation, targetRotation, lerpTime);
    }
}
