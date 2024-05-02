using UnityEngine;
using System.Linq;


public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceToTarget = 30;

    private const int RequiredTicksWithoutMovementToDisplayWeather = 100;

    public GameObject sideBar;
    public GameObject downBar;
    public GameObject disasterBar;

    private Vector3 _previousMousePosition;
    private Vector3 _focusedPosition;
    private int _ticksWithoutMovement = 0;   
    [SerializeField]    
    private WeatherAPI weatherAPI;
    
    [SerializeField]
    private Calculator calculator;

    private void RotateWithMouse() {
        if (Input.GetMouseButtonDown(0)) {
            _previousMousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
            return;
        }

        else if (!Input.GetMouseButton(0))
            return;

        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = _previousMousePosition - newPosition;

        float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
        float rotationAroundXAxis = direction.y * 180; // camera moves vertically

        cam.transform.position = target.position;

        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        _previousMousePosition = newPosition;
    }

    private void ZoomWithMouse()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (!sideBar.activeSelf && !downBar.activeSelf && !disasterBar.activeSelf) {
            distanceToTarget -= scroll * TargetRadius / 3; // adjust the zoom speed
        }
    }

    private float TargetRadius => target.GetComponent<Collider>().bounds.size.x / 2;

    private void Update()
    {
        ZoomWithMouse();
        RotateWithMouse();
    }
    
    private Vector3? TargetHitPoint() {
        var direction = target.position - cam.transform.position;
        if (!Physics.Raycast(cam.transform.position, direction, out var hit))
            return null;

        if (hit.collider.gameObject != target.gameObject)
            return null;
        
        return hit.point;
    }

    private void ReloadWeather(Vector3 hitPoint) {
        Debug.Log("Reloading weather");
        var latLon = calculator.CalculateLatAndLonFromPosition(hitPoint);
        var countries = calculator.GetCloseCountries(latLon);
        var dict = countries.ToDictionary(c => c.Coordinates, c => c);
        weatherAPI.GetAllVisibleCountriesWeatherData(dict);
    }
    
    private void UpdateTicksWithoutMovement(Vector3 frameFocusedPosition) {
        if (frameFocusedPosition == this._focusedPosition) 
            _ticksWithoutMovement++;
        else
            _ticksWithoutMovement = 0;
    }
    
    void DisplayWeatherIfNoMovement() {
        var hitPoint = TargetHitPoint();
        if (hitPoint == null)
            return;
        
        UpdateTicksWithoutMovement((Vector3)hitPoint);
        _focusedPosition = (Vector3)hitPoint;
        
        if (_ticksWithoutMovement != RequiredTicksWithoutMovementToDisplayWeather)
            return;
        
        ReloadWeather((Vector3)hitPoint);
    }

    private void FixedUpdate()
    {
        DisplayWeatherIfNoMovement();
    }
}
