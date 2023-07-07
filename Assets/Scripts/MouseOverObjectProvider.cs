using UnityEngine;
using UnityEngine.Events;

public class MouseOverObjectProvider : MonoBehaviour
{
    [SerializeField] private string objectTag;
        
    [Space(10)] 
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseExit;

    private bool _wasOnLastFrame;
    private Camera _mainCam;
        
    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            
        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.CompareTag(objectTag) && !_wasOnLastFrame)
            {
                onMouseEnter.Invoke();
                _wasOnLastFrame = true;
            }
                
            if (!hit.collider.CompareTag(objectTag) && _wasOnLastFrame)
            {
                onMouseExit.Invoke();
                _wasOnLastFrame = false;
            }
        } 
        else if (_wasOnLastFrame)
        {
            onMouseExit.Invoke();
            _wasOnLastFrame = false;
        }
    }
}