using UnityEngine;

namespace PixelCrushers
{

    /// <summary>
    /// Sets EventSystem to use for self and all children that
    /// implement IEventSystemUser interface.
    /// </summary>
    [AddComponentMenu("")] // Use wrapper.
    public class SetEventSystem : MonoBehaviour
    {

        public UnityEngine.EventSystems.EventSystem eventSystem;

        private void Start()
        {
            AssignEventSystemToHierarchy(eventSystem);
        }

        public void AssignEventSystemToHierarchy(UnityEngine.EventSystems.EventSystem eventSystem)
        { 
            this.eventSystem = eventSystem;
            UIUtility.SetEventSystemInChildren(transform, eventSystem);
        }

    }
}
