using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeMovement : MonoBehaviour {
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
    [SerializeField] GameObject camBoom;
    float camAngle;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        turnAngle = transform.localEulerAngles.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
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
            camBoom.transform.localRotation = Quaternion.Euler(Vector3.right * camAngle);
        }

        if (!Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            body.velocity += Vector3.down * gravity;
            body.velocity += (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")) * moveSpeed * airReduc;

            float saveY = body.velocity.y;
            body.velocity -= Vector3.up * saveY;
            body.velocity = Vector3.Lerp(body.velocity, Vector3.ClampMagnitude(body.velocity, maxAirSpeed),airDrag) + Vector3.up * saveY;

            Quaternion camRot = camBoom.transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)), 0.4f);
            camBoom.transform.rotation = camRot;
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
                body.velocity += transform.up * jumpSpeed;
            else
                body.velocity = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")) * moveSpeed;
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
    }


    private void OnCollisionEnter(Collision collision)
    {
        float angle = (Vector3.Dot(transform.up, collision.contacts[0].normal)*Mathf.Rad2Deg);

        Debug.Log(angle);

        if (angle > minAngle/90f && angle > 0)
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 oldForward = transform.forward;
            transform.up = normal;
            transform.Rotate(Vector3.up, turnAngle);
            //*/
            Quaternion camRot = camBoom.transform.rotation;
            camBoom.transform.rotation = camRot;
            //*/
            /*
            There is still one edge case to take care of --
            when landing a certain way, teh player is tilted for a second
            Also, running 90 degress up rotates teh player slightly, but fixes on landing.

            TODO:
                Edge Case rotation
                Cam rotation smoothing
                Guns
                Networking
                Think about WallCling
            */
        }

    }

}
