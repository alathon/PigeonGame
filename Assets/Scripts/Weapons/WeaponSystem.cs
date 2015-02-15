using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    /// <summary>
    /// Emulates a collection of weapons for a given enemy.
    /// </summary>
    public class WeaponSystem : MonoBehaviour
    {
        public Weapon[] Weapons;

        public void Engage()
        {
            this.gameObject.SetActive(true);            
        }

        public void Disengage()
        {
            for (int i = 0; i < Weapons.Length; i++)
            {
                Weapons[i].Stop();
            }
            this.gameObject.SetActive(true);            
        }

        void Update () {
            for (int i = 0; i < Weapons.Length; i++)
            {
                if(Weapons[i].CanFire()) Weapons[i].Fire();
            }
        }
    }
}
