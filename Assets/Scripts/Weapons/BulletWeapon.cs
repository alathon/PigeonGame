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

    protected override IEnumerator Act()
    {
        var current = this.transform.rotation;

        for (var i = 0; i < this.shots; i++)
        {
            var bullet = BulletPoolManager.Instance.GetGameObject(this.bulletPrefab.name);
            if (bullet == null) continue;

            bullet.transform.rotation = this.transform.rotation;
            bullet.transform.position = this.transform.position;
            if (this.rotateOnShot) this.transform.Rotate(Vector3.forward * this.rotationPerShot);
            bullet.gameObject.SetActive(true);
            if (this.timeBetweenShots > float.Epsilon) yield return new WaitForSeconds(timeBetweenShots);
        }
        if(this.rotateOnShot) this.transform.rotation = current;
    }
}