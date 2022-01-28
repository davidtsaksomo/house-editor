using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Singleton
    public static CameraManager instance;

    [SerializeField]
    float cameraSpeed = 10f;

    float cameraOffset = 5f;
    float rotationSpeed = 0.5f;
    float rotationTimeCounter = 0f;
    int currentCameraRotation = 0;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void MoveUp()
    {
        Move(0);
    }

    public void MoveRight()
    {
        Move(1);
    }

    public void MoveDown()
    {
        Move(2);
    }

    public void MoveLeft()
    {
        Move(3);
    }

    // 0 = Up, 1 = Right, 2 = Down, 3 = Left
    void Move(int direction)
    {
        // Adjust direction according to current camera rotation
        direction = (direction + currentCameraRotation) % 4;

        switch (direction)
        {
            case 0:
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + (cameraSpeed * Time.deltaTime), 5f, GameConstants.worldWidth - cameraOffset), transform.position.y, transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldLength - 5f));
                break;
            case 2:
                transform.position = new Vector3(Mathf.Clamp(transform.position.x - (cameraSpeed * Time.deltaTime), 5f, GameConstants.worldWidth - cameraOffset), transform.position.y, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldLength - 5f));
                break;
        }
    }

    public void RotateRight()
    {
        currentCameraRotation = (currentCameraRotation + 1) % 4;
        rotationTimeCounter = 0f;
    }

    public void RotateLeft()
    {
        currentCameraRotation = (currentCameraRotation - 1) % 4;
        rotationTimeCounter = 0f;
    }

    private void FixedUpdate()
    {
        if (rotationTimeCounter < 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, 45f + (90f * currentCameraRotation), transform.eulerAngles.z)), rotationTimeCounter);
            rotationTimeCounter += Time.fixedDeltaTime / rotationSpeed;
        }

    }
}
