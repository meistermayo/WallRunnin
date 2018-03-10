using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject baseProjectile;
    Collider myCol;
    float timer;
    bool canShoot = true;
    void Start()
    {
        myCol = GetComponent<Collider>();
    }
    void Update()
    {
        Shoot();
        Timer();
    }

    void Shoot()
    {
        if (isLocalPlayer)
        {
            if (canShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    CmdShoot();
                }
            }
        }
    }

    [Command]
    void CmdShoot()
    {
        GameObject temp = Instantiate(baseProjectile, Camera.main.transform.position, Camera.main.transform.rotation);
        Physics.IgnoreCollision(myCol, temp.GetComponent<Collider>());
        Projectile projectile = temp.GetComponent<Projectile>();
        projectile.SetBullet(bullet);

        NetworkServer.Spawn(temp);

        canShoot = false;
        timer = weapon.cooldown;
    }
    void Timer()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            canShoot = true;
    }
}
