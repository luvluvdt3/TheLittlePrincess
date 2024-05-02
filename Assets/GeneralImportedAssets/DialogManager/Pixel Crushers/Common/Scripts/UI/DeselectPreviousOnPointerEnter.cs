// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PixelCrushers
{

    /// <summary>
    /// This script deselects the previous selectable when the pointer enters this one.
    /// </summary>
    [AddComponentMenu("")] // Use wrapper.
    [RequireComponent(typeof(Selectable))]
    public class DeselectPreviousOnPointerEnter : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IEventSystemUser
    {

        private UnityEngine.EventSystems.EventSystem m_eventSystem = null;
        public UnityEngine.EventSystems.EventSystem eventSystem
        {
            get
            {
                if (m_eventSystem != null) return m_eventSystem;
                return UnityEngine.EventSystems.EventSystem.current;
            }
            set { m_eventSystem = value; }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!eventSystem.alreadySelecting)
            {
                eventSystem.SetSelectedGameObject(this.gameObject);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            GetComponent<Selectable>().OnPointerExit(null);
        }
    }
}