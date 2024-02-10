using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

namespace YuzuValentine
{
    public class UI_Abilities : MonoBehaviour
    {
        [SerializeField] private List<BaseAbility> abilities = new();

        void Start()
        {
            foreach (BaseAbility ability in abilities)
            {
                ShowAbilityEnabled(ability);
            }
        }

        void Update()
        {
            // should handle ability input in another script
            foreach (BaseAbility ability in abilities)
            {
                if (Input.GetKeyDown(ability.key) && ability.canUse)
                {
                    ability.canUse = false;
                    ability.currentCooldown = ability.cooldown;
                }

                AbilityCooldown(ability);
            }
        }
        private void AbilityCooldown(BaseAbility ability)
        {
            if (!ability.canUse)
            {
                ability.currentCooldown -= Time.deltaTime;

                if (ability.currentCooldown <= 0f)
                {
                    ability.canUse = true;
                    ability.currentCooldown = 0f;
                    ShowAbilityEnabled(ability);
                }
                else
                {
                    ability.cdImage.enabled = true;
                    ability.cdText.text = Mathf.Ceil(ability.currentCooldown).ToString();
                }
            }
        }

        private void ShowAbilityEnabled(BaseAbility ability)
        {
            ability.cdImage.enabled = false;
            ability.cdText.text = "";
        }
    }
}
