using System;
using Arr.EventsSystem;
using UnityEngine;

namespace Testing
{
    public class PlayerController : MonoBehaviour
    {
        public Action<float> OnDamaged;

        public void Damage(int damage)
        {
            GlobalEvents.Fire(new EventOnPlayerDamaged(){ damage = damage});
        }
    }
}