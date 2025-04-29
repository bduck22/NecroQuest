using UnityEngine;

public class CameraMove : MonoBehaviour
{
    bool XMove;
    bool YMove;
    public float CameraSpeed;
    [SerializeField] float MaxZoom;
    [SerializeField] float MinZoom;
    public float ZoomSpeed;
    void Start()
    {
        
    }
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (mousePosition.x < 3 || mousePosition.x > Screen.width - 3)
        {
            XMove = true;
        }
        else
        {
            XMove= false;
        }
        if (mousePosition.y < 3 || mousePosition.y > Screen.height - 3)
        {
            YMove = true;
        }
        else
        {
            YMove = false;
        }
        if (XMove)
        {
            transform.Translate(Input.GetAxisRaw("Mouse X") * CameraSpeed*Time.deltaTime, 0, 0);
        }
        if (YMove)
        {
            transform.Translate(0, Input.GetAxisRaw("Mouse Y") * CameraSpeed * Time.deltaTime, 0);
        }
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0 && Camera.main.orthographicSize > MinZoom)
        {
            Camera.main.orthographicSize -= scroll*ZoomSpeed;
        }
        if(scroll < 0&&Camera.main.orthographicSize < MaxZoom)
        {
            Camera.main.orthographicSize -= scroll * ZoomSpeed;
        }
    }
}
