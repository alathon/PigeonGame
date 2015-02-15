using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Weapons;

public class BulletWeapon : Weapon
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected int shots = 20;
    [SerializeField] protected float timeBetweenShots = 0f;

    protected override IEnumerator Act()
    {
        for (var i = 0; i < this.shots; i++)
        {
            var bullet = BulletPoolManager.Instance.GetGameObject(this.bulletPrefab.name);
            if (bullet == null) continue;

            bullet.transform.rotation = this.transform.rotation;
            bullet.transform.position = this.transform.position;
            bullet.gameObject.SetActive(true);
            if (this.timeBetweenShots > float.Epsilon) yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}