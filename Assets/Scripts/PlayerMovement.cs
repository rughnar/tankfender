using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody2D m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.

    private float currentAngle = 0f;
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";

        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        // Store the value of both input axes.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
        if (Input.GetKeyDown(KeyCode.W)) this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.A)) this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        if (Input.GetKeyDown(KeyCode.S)) this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        if (Input.GetKeyDown(KeyCode.D)) this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
    }


    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.PlayOneShot(m_EngineIdling);
                m_MovementAudio.Play();
            }
            else
            {
                if (!m_MovementAudio.isPlaying)
                {
                    m_MovementAudio.clip = m_EngineIdling;
                    m_MovementAudio.Play();

                }
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
            else
            {
                if (!m_MovementAudio.isPlaying)
                {
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.Play();
                }
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector2 movement = transform.up * Mathf.Clamp(Mathf.Abs(m_MovementInputValue) + Mathf.Abs(m_TurnInputValue), 0, 1) * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {

        if (m_TurnInputValue != 0)
        {
            // Calcula el nuevo ángulo basado en la entrada
            float newAngle = currentAngle + (m_TurnInputValue > 0 ? 90f : -90f);

            // Asegúrate de que el nuevo ángulo esté dentro de un rango válido (0-360 grados)
            newAngle = newAngle % 360;

            // Aplica la rotación
            transform.rotation = Quaternion.Euler(0, 0, newAngle);

            // Actualiza el ángulo actual
            currentAngle = newAngle;
        }
    }


}