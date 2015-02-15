using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// Emulates a collection of weapons for a given enemy.
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        private List<Weapon> currentlyFiring = new List<Weapon>();
        private Dictionary<Weapon, int> volleys = new Dictionary<Weapon, int>();

        public void Engage(List<Weapon> weapons, int volleys)
        {
            this.gameObject.SetActive(true);
            this.currentlyFiring = weapons;
            this.volleys.Clear();
            foreach (var weapon in currentlyFiring)
            {
                this.volleys.Add(weapon, volleys);
            }
        }

        public void Disengage()
        {
            foreach (var weapon in currentlyFiring)
            {
                weapon.Stop();
            }

            this.currentlyFiring.Clear();
            this.volleys.Clear();
            this.gameObject.SetActive(false);            
        }

        void Update ()
        {
            if (currentlyFiring.Count == 0) return;

            foreach (var weapon in currentlyFiring)
            {
                if (!weapon.CanFire()) continue;

                if (volleys[weapon] == 0)
                {
                    this.Disengage();
                    return;
                }

                weapon.Fire();
                volleys[weapon] -= 1;
            }
        }
    }
}
