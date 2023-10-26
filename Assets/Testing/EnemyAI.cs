using Arr.EventsSystem;
using UnityEngine;

namespace Testing
{
    public class EnemyAI : MonoBehaviour, IEventListener<EventOnPlayerDamaged>
    {
        public void OnEvent(EventOnPlayerDamaged data)
        {
            
        }
    }
}