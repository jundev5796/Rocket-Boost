using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput < 0)
        {
            RotateRight();
        }

        else if (rotationInput > 0)
        {
            RotateLeft();
        }

        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);

        if (!rightBooster.isPlaying)
        {
            leftBooster.Stop();
            rightBooster.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);

        if (!leftBooster.isPlaying)
        {
            rightBooster.Stop();
            leftBooster.Play();
        }
    }

    private void StopRotating()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }    

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
