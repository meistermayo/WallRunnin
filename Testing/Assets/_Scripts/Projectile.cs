using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
    Rigidbody body;
    [SerializeField] Bullet bullet;
    void Awake()
    {
        Destroy(gameObject,10f);
        body = GetComponent<Rigidbody>();
    }

    public void SetBullet(Bullet bullet)
    {
        body = GetComponent<Rigidbody>();
        this.bullet = bullet;
        Debug.Log(bullet.moveSpeed);
        body.velocity = transform.forward * bullet.moveSpeed;
        body.velocity = Quaternion.Euler(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*bullet.inaccuracy) * body.velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
