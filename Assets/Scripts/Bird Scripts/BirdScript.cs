using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private Animator anim;
    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;
    public bool isAlive;
    private bool didFlap;
    private Button flapButton;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flapClip, pointClip, diedClip;

    public int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;

        /*  
         *  Esto es necesario para ligar el botón a cada pájaro (rojo, verde y azul); si lo hiciésemos desde Unity 
         *  (OnClic, +, add the gameObject, select the function, etc.), sólo podríamos agregar un pájaro cada vez.
        */
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => didFlap = true);

        SetCamerasX();

        score = 0;

    }

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (isAlive)
        {
            MoveForward();
            RotateTheFallingBird();

            if (didFlap)
            {
                FlapTheBird();
            }
        }
    }

    public void FlapTheBird()
    {
        myRigidBody.velocity = new Vector2(0, bounceSpeed);
        audioSource.PlayOneShot(flapClip);
        anim.SetTrigger("Flap");
        didFlap = false;
    }

    private void MoveForward()
    {
        Vector3 temp = transform.position;
        temp.x += forwardSpeed * Time.deltaTime;
        transform.position = temp;
    }

    private void RotateTheFallingBird()
    {
        if (myRigidBody.velocity.y >= 0)
        {
            float angle = Mathf.Lerp(0, 35, myRigidBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            float angle = Mathf.Lerp(0, -45, -myRigidBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void SetCamerasX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
                GameplayController.instance.PlayerDiedShowScore(score);
                isAlive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PipeHolder")
        {
            score++;
            GameplayController.instance.SetScore(score);
            //audioSource.PlayOneShot(pointClip);
        }
    }

}
