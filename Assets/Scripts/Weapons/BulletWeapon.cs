using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Weapons;

public class BulletWeapon : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int shots = 20;
    [SerializeField] private float rotationPerShot = -10f;
    [SerializeField] private bool rotateOnShot = false;
    [SerializeField] private float timeBetweenShots = 0f;

    // TODO: Use bullet cache instead of instantiating new bullets.
    protected override IEnumerator Act()
    {
        Quaternion current = this.transform.rotation;

        for (int i = 0; i < this.shots; i++)
        {
            Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            if(this.rotateOnShot) this.transform.Rotate(Vector3.forward * this.rotationPerShot);

            if (this.timeBetweenShots > float.Epsilon) yield return new WaitForSeconds(timeBetweenShots);
        }

        if(this.rotateOnShot) this.transform.rotation = current;
    }
}