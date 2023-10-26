using Arr.ViewModuleSystem;
using TMPro;
using UnityEngine;

namespace Testing
{
    public class TestView : View
    {
        [SerializeField] private TMP_InputField input;

        public string GetInput() => input.text;
    }
}