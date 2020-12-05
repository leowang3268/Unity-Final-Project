using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;

    private Rigidbody rb;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimit = 85f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
    }

    // Get a movement vector
    public void setVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Get a rotational vector
    public void setRotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void setCameraRotation(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    // Get a force vector for our thruster
    public void SetThrusterForce(Vector3 _thrusterforce)
    {
        thrusterForce = _thrusterforce;
    }

    // Perform movement based on velocity variable
    private void PlayerMovement()
    {
        if (velocity != Vector3.zero) 
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); // move rb to new position with making physics check
        }

        if (thrusterForce != Vector3.zero) 
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    // Perform rotation based on rotation variable
    private void PlayerRotation()
    {
        if (rotation != Vector3.zero) 
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation)); // parameter of Quaternion.Euler is of Vector3 type. It's the angle we're familiar to. 
        }

        cam.transform.Rotate(-cameraRotation); // we don't want to invert the y-axis
    }

    
}
