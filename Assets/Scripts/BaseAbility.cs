using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YuzuValentine
{
    [Serializable]
    public class BaseAbility
    {
        public Image cdImage;
        public TextMeshProUGUI cdText;
        public KeyCode key;
        public float cooldown;
        public float currentCooldown;
        public bool canUse = true;
    }
}
