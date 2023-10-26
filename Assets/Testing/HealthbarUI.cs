using Arr.EventsSystem;
using TMPro;
using UnityEngine;

namespace Testing
{
    public class HealthbarUI : MonoBehaviour, IEventListener<EventOnPlayerDamaged>
    {
        [SerializeField] private TextMeshProUGUI healthText;

        private void Start()
        {
            GlobalEvents.Register(this);
        }

        public void OnEvent(EventOnPlayerDamaged data)
        {
            healthText.text = data.damage.ToString();
        }
    }
}