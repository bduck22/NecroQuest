using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float CameraSpeed;
    [SerializeField] float MaxZoom;
    [SerializeField] float MinZoom;
    public float ZoomSpeed;
    [SerializeField] Vector2 MaxVector;
    [SerializeField] Vector2 MinVector;
    void Update()
    {
        if(GameManager.instance.GameStatus == GameStatus.Result)
        {
            return;
        }

        Vector2 mousePosition = Input.mousePosition;

        if (mousePosition.x < 3 || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x >= MinVector.x + Camera.main.orthographicSize * (16 / 9))
            {
                transform.Translate(-CameraSpeed * Time.deltaTime, 0, 0);
            }
        }
        else if (mousePosition.x > Screen.width - 3 || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x <= MaxVector.x - Camera.main.orthographicSize * (16 / 9))
            {
                transform.Translate(CameraSpeed * Time.deltaTime, 0, 0);
            }
        }

        if (mousePosition.y < 3 || Input.GetKey(KeyCode.S))
        {
            if (transform.position.y >= MinVector.y + Camera.main.orthographicSize)
            {
                transform.Translate(0, -CameraSpeed * Time.deltaTime, 0);
            }
        }
        else if (mousePosition.y > Screen.height - 3 || Input.GetKey(KeyCode.W))
        {
            if (transform.position.y <= MaxVector.y - Camera.main.orthographicSize)
            {
                transform.Translate(0, CameraSpeed * Time.deltaTime, 0);
            }
        }

        if (Time.timeScale != 0)
        {
            float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
            if ((scroll < 0 || Input.GetKey(KeyCode.E)) && Camera.main.orthographicSize < MaxZoom)
            {
                Camera.main.orthographicSize += (-scroll) + ZoomSpeed * Time.deltaTime;
                transform.localScale = new Vector3(Camera.main.orthographicSize / 5, Camera.main.orthographicSize / 5, 1);
            }
            if ((scroll > 0 || Input.GetKey(KeyCode.Q)) && Camera.main.orthographicSize > MinZoom)
            {
                Camera.main.orthographicSize -= scroll + ZoomSpeed * Time.deltaTime;
                transform.localScale = new Vector3(Camera.main.orthographicSize / 5, Camera.main.orthographicSize / 5, 1);
            }
        }
    }
}
