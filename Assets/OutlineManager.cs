using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YuzuValentine
{
    public class OutlineManager : MonoBehaviour
    {
        private Outline outline;

        private void Awake()
        {
            outline = GetComponent<Outline>();
            HideOutline();
        }
        private void HideOutline()
        {
            outline.enabled = false;
        }
        private void ShowOutline()
        {
            outline.enabled = true;
        }
        private void OnMouseEnter()
        {
            ShowOutline();
        }
        private void OnMouseExit()
        {
            HideOutline();
        }
    }
}
