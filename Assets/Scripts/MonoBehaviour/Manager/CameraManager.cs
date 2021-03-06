using UnityEngine;

// Manage camera movement and rotation
public class CameraManager : MonoBehaviour
{
    // Singleton
    public static CameraManager instance;

    // Speed of camera movement. Set in the inspector.
    [Tooltip("Speed of camera movement.")]
    [SerializeField]
    float cameraSpeed = 10f;

    // Offset so that camera do not go far from the edge of the land
    readonly float cameraOffset = 5f;

    // Speed of camera rotation
    readonly float rotationSpeed = 0.5f;
    // Keep track of camera rotation animation
    float rotationTimeCounter = 0f;

    // State of camera rotation. 0 - 3
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
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldWidth - cameraOffset), transform.position.y, transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldLength - cameraOffset));
                break;
            case 2:
                transform.position = new Vector3(Mathf.Clamp(transform.position.x - (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldWidth - cameraOffset), transform.position.y, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + (cameraSpeed * Time.deltaTime), cameraOffset, GameConstants.worldLength - cameraOffset));
                break;
        }
    }

    public void RotateRight()
    {
        currentCameraRotation = MathMod((currentCameraRotation + 1), 4);
        rotationTimeCounter = 0f;
    }

    public void RotateLeft()
    {
        currentCameraRotation = MathMod((currentCameraRotation - 1), 4);
        rotationTimeCounter = 0f;
    }

    private void FixedUpdate()
    {
        // Rotate animation
        if (rotationTimeCounter < 2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, 45f + (90f * currentCameraRotation), transform.eulerAngles.z)), rotationTimeCounter);
            rotationTimeCounter += Time.fixedDeltaTime / rotationSpeed;
        }

    }

    // Method to get positive number from mod-ing a negative number
    int MathMod(int a, int b)
    {
        return (Mathf.Abs(a * b) + a) % b;
    }
}
