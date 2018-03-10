using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SomeMovement : NetworkBehaviour {
    float h, v;
    float turnAngle;
    Rigidbody body;
    [SerializeField] float minAngle = 90f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxGroundSpeed = 20f;
    [SerializeField] float maxAirSpeed = 10f;
    [SerializeField] float airDrag = 0.5f;
    [SerializeField] float airReduc = 0.5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float gravity = 1f;
    [SerializeField] float mouseSens = 1f;
    [SerializeField] GameObject camBoomLerpTarget;
    [SerializeField] GameObject camBoom;
    //[SerializeField] float rotLerpTime = 0.1f;

    Quaternion cameraTargetRotation;
    float camAngle;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        turnAngle = transform.localEulerAngles.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        if (isLocalPlayer)
        {
            Camera.main.transform.parent = 
                camBoom.transform;
            Camera.main.transform.localPosition = Vector3.zero;
            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled=false;// layer = LayerMask.NameToLayer("LocalModel");
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        // if (Camera.main.transform.rotation.eulerAngles.y != transform.localEulerAngles.y)
        //     transform.Rotate(Vector3.up,Mathf.DeltaAngle(Camera.main.transform.rotation.eulerAngles.y,transform.localEulerAngles.y));
        float mx, my;
        mx = Input.GetAxisRaw("Mouse X") * mouseSens;
        my = -Input.GetAxisRaw("Mouse Y") * mouseSens;
        turnAngle = (turnAngle + mx) % 360; ;
        transform.Rotate(Vector3.up, mx);

        if (camAngle + my < 90f && camAngle + my > -90f)
        {
            camAngle += my;
            camBoomLerpTarget.transform.localRotation =  ( Quaternion.Euler(Vector3.right* camAngle));
            camBoom.transform.localRotation *= (Quaternion.Euler(Vector3.right * my));
        }

        if (!Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            body.velocity += Vector3.down * gravity;
            body.velocity += (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")) * moveSpeed * airReduc;

            float saveY = body.velocity.y;
            body.velocity -= Vector3.up * saveY;
            body.velocity = Vector3.Lerp(body.velocity, Vector3.ClampMagnitude(body.velocity, maxAirSpeed),airDrag) + Vector3.up * saveY;

            Quaternion camRot = camBoomLerpTarget.transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)), 0.4f);
            camBoomLerpTarget.transform.rotation = camRot;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
                body.velocity += transform.up * jumpSpeed;
            else
                body.velocity = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
         {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        //camBoom.transform.rotation = Quaternion.Lerp(camBoom.transform.rotation, cameraTargetRotation, rotLerpTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        float angle = (Vector3.Dot(transform.up, collision.contacts[0].normal)*Mathf.Rad2Deg);

        //Debug.Log(angle);

        if (angle > minAngle/90f && angle > 0)
        {
            Quaternion camRot = camBoomLerpTarget.transform.rotation;
            Quaternion camBoomRot = camBoom.transform.rotation;

            Vector3 normal = collision.contacts[0].normal;
            Vector3 oldForward = transform.forward;
            
            transform.up = normal;
            //transform.Rotate(Vector3.up, -transform.localEulerAngles.y);
            transform.Rotate(Vector3.up, turnAngle);

            camBoomLerpTarget.transform.rotation = (camRot);
            camBoom.transform.rotation = (camBoomRot);
        }

    }
    /*
  There is still one edge case to take care of --
  when landing a certain way, teh player is tilted for a second
  Also, running 90 degress up rotates teh player slightly, but fixes on landing.

  TODO:

      still some turning once we go upside down -- up normal default up rotation is different somehow
      falling 180 degrees flips the player

      Edge Case rotation
      Cam rotation smoothing
      Guns
      Networking
      Think about WallCling
  */
}
