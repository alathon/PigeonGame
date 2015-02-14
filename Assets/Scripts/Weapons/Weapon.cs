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

    /// <summary>
    /// Enum representing weapon types, this determines what type of ammo can be shot.
    /// </summary>
    public enum WeaponType
    {
        Bullet,  // Simple bullet spewing, bbrrrrrt weapons.
        Missile, // Advanced homing weaponry.
        Beam     // LAZZO0OR based weaponry.
    }

    public class Weapon : MonoBehaviour
    {
        public WeaponType WType;          // What type of weapon is this.
        public EmissionPattern WPattern;  // What pattern are ammo emitted in.
        public float Cooldown = 1f;       // Time between each shot.


        private float timeToNextShot = 0f; // How long until we can shoot again.
    }
}