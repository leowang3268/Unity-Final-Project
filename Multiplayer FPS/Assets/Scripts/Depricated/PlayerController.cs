using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1300f;

    [Header("Spring Settings:")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        setJointSettings(jointSpring);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal"); // x vector will be either -1, 0 or 1. Change will apply immediately.
        float zMov = Input.GetAxisRaw("Vertical"); // z vector will be either -1, 0 or 1.

        Vector3 moveHorizontal = transform.right * xMov; //(1, 0 ,0) when go right and (-1, 0, 0) when go left
        Vector3 moveVertical = transform.forward * zMov;

        // Final movement vector
        Vector3 _velocity = (moveHorizontal + moveVertical).normalized * speed;

        // Apply movement 
        motor.setVelocity(_velocity);

        // Calculate rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X"); // rotate the y-axis when moving the mouse in x-axis direction

        Vector3 _rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        // Apply rotation 
        motor.setRotation(_rotation);

        // Calculate camera rotation as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y"); // rotate the x-axis when moving the mouse in y-axis direction
        xRot = Mathf.Clamp(xRot, -85f, 85f);
        Vector3 _cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        // Apply camera rotation 
        motor.setCameraRotation(_cameraRotation);



        // Calculate the thruster force based on player's input
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            setJointSettings(0f);
        }
        else
        {
            setJointSettings(jointSpring);
        }

        // Apply the thruster force
        motor.SetThrusterForce(_thrusterForce);
    }

    private void setJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
