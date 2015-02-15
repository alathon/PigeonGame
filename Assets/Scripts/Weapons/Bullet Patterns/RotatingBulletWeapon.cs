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
    [SerializeField] private float bulletSpeed = 5f;

    protected override IEnumerator Act()
    {
        var current = this.transform.rotation;

        for (var i = 0; i < this.shots; i++)
        {
            var bullet = BulletPoolManager.Instance.GetGameObject(this.bulletPrefab.name);
            if (bullet == null) continue;

            bullet.transform.rotation = this.transform.rotation;
            bullet.transform.position = this.transform.position;
            bullet.GetComponent<Bullet>().modifiedSpeed = this.bulletSpeed;
            if (this.rotateOnShot) this.transform.Rotate(Vector3.forward * this.rotationPerShot);
            bullet.gameObject.SetActive(true);
            if (this.timeBetweenShots > float.Epsilon) yield return new WaitForSeconds(timeBetweenShots);
        }
        if (this.rotateOnShot && this.resetRotationAfterShooting) this.transform.rotation = current;
    }
}