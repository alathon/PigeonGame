using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Weapons;

public class RotatingBulletWeapon : BulletWeapon
{
    [SerializeField] private float rotationPerShot = -10f;
    [SerializeField] private bool rotateOnShot = false;
    [SerializeField] private bool resetRotationAfterShooting = true;
    

    protected override IEnumerator Act()
    {
        var current = this.transform.rotation;

        for (var i = 0; i < this.shots; i++)
        {
            var bullet = SpawnDefaultBullet();
            if (this.rotateOnShot) this.transform.Rotate(Vector3.forward * this.rotationPerShot);
            bullet.gameObject.SetActive(true);
            if (this.timeBetweenShots > float.Epsilon) yield return new WaitForSeconds(timeBetweenShots);
        }
        if (this.rotateOnShot && this.resetRotationAfterShooting) this.transform.rotation = current;
    }
}