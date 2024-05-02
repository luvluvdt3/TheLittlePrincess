namespace PixelCrushers
{

    /// <summary>
    /// Allows classes to have a reference to an EventSystem. 
    /// Useful for local multiplayer games that have more than one EventSystem.
    /// </summary>
    public interface IEventSystemUser
    {

        UnityEngine.EventSystems.EventSystem eventSystem { get; set; }
    }
}
