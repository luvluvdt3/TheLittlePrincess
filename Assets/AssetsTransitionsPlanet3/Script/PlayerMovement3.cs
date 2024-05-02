using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement3 : MonoBehaviour
{
    public static bool isBack = false;
    public static Vector3 savePos;
    public float turnSpeed = 10f; // In Unity, public member variables appear in the Inspector window and can therefore be tweaked.
    public float speed = 15f;
    //You�ve also used camelCase (rather than PascalCase, with its m_ prefix). This is because the variable is public, and the Unity naming convention uses this format for public member variables.  Naming conventions can be very useful, but there�s no technical reason for this.
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    public GameObject rick;

    Quaternion m_Rotation = Quaternion.identity;
    public GameObject pet;
    public GameObject circle;
    public Vector2 petOffset = new Vector2(1.5f, 2.7f);
    public Vector2 circleOffset = new Vector2(0.5f, 0.2f);
    private Quaternion _lastRotation;
    bool shouldBeMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        _lastRotation = transform.rotation;
        if (isBack)
        {
            isBack = false;
            transform.position = rick.transform.position + new Vector3(-1f, 0f, 1.6f);
            rick.SetActive(true);
        }
    }

    void FixedUpdate()
    //OnAnimatorMove is actually going to be called in time with physics, and not with rendering like your Update method.  
    //The movement vector and rotation are set in Update.If OnAnimatorMove gets called first then you will have a problem, because a Quaternion without a value set doesn�t make any sense.
    //To make sure the movement vector and rotation are set in time with OnAnimatorMove, change your Update method to a FixedUpdate
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement = transform.right * horizontal + transform.forward * vertical;

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        //hasHorizontalInput is true when horizontal is non-zero.
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //if hasHorizontalInput or hasVerticalInput are true then isWalking is true, and otherwise it is false. (Input means the user player pressing controls)


        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (shouldBeMoving)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _lastRotation, turnSpeed * Time.deltaTime);
                transform.position -= transform.forward * speed * Time.deltaTime;
                m_Animator.SetBool("IsWalking", false);
                if (!m_AudioSource.isPlaying && m_AudioSource.enabled && m_Rigidbody.useGravity)
                {
                    m_AudioSource.Play();
                }
            }
        }

        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (shouldBeMoving)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, m_Rotation, turnSpeed * Time.deltaTime);
                if (!m_AudioSource.isPlaying && m_AudioSource.enabled && m_Rigidbody.useGravity)
                {
                    m_AudioSource.Play();
                }
                m_Animator.SetBool("IsWalking", true);
            }
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);
            _lastRotation = transform.rotation;
        }

        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _lastRotation, turnSpeed * Time.deltaTime);
            m_Animator.SetBool("IsWalking", false);
            if (m_AudioSource.isPlaying)
            {
                m_AudioSource.Stop();
            }
        }


    }

    void OnAnimatorMove()
    {
        //m_Animator.ApplyBuiltinRootMotion();
        pet.transform.position = transform.position + transform.right * petOffset.x + transform.up * petOffset.y + transform.forward;
        pet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        circle.transform.position = transform.position + transform.up * circleOffset.x - transform.forward * circleOffset.y;
        circle.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

    }

    public void stopMovement()
    {
        shouldBeMoving = false;
    }

    public void startMovement()
    {
        shouldBeMoving = true;
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void goToNextSceneWithSavePos()
    {
        savePos = transform.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}