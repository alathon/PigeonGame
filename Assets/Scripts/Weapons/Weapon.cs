using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// List of possible emission patterns for the weapon.
    /// </summary>
    public enum EmissionPattern
    {
        Line,       // One straight line.
        DoubleLine, // Two straight lines.
        TripleLine, // Three straight lines.
        Cone,       // n straight lines forming a cone of fire.
        Omni        // n straight lines from the center dispersed around all 360 degrees.
    }

    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float cooldown = 5f;
        private float nextShotAt = 0f;

        public void Fire()
        {
            nextShotAt = Time.time + this.cooldown;
            StartCoroutine(this.Act());
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        protected virtual IEnumerator Act()
        {
            yield return null;
        }

        public bool CanFire()
        {
            return Time.time >= this.nextShotAt;
        }
    }
}