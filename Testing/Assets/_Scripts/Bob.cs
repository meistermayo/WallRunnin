using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] float lerpSpot;
    [SerializeField] Vector3 offset;
    private void Update()
    {
        transform.position = offset + Camera.main.transform.position + Camera.main.transform.forward * lerpSpot;
    }
}
