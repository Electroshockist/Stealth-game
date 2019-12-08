using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotationSpeed = 8.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 9.81f;

    private bool bulletOut = false;
    private int bulletTimer = 0;

    private float inputH;
    private float inputV;
    Vector3 movDirection = Vector3.zero;


    //public Text winText;
    public Text Playerhealthtext;
    public Text Briefcasetext;
    public Text itemText;
    public bool alive = true;


    //player health
    public int health = 3;


    public GameObject[] pickups;
    public Transform[] spawnPointsTransform;


    public EnemyRandomMove enemy;

    //game objects 
    public int Briefcase = 0;
    public int item = 0;


    Vector3 moveDirection = Vector3.zero;

    public bool sMoveType;

    //animate
    public Animator anim;


    CharacterController cc;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public enum PlayerState { Idle, Walking, Attacking };
    public PlayerState state = PlayerState.Idle;


   public  Scenemanager scenemanager;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        //animate
        anim = GetComponent<Animator>();

        // Set the text property of our death UI to an empty string, making the 'Your Dead' (game over message) blank
        Playerhealthtext.text = "";
        Briefcasetext.text = "";
        itemText.text = "";

        // Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
        // winText.text = "";
        SetCountText();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sMoveType = !sMoveType;
        }

        if (health >= 3)
        {
            health = 3;
            SetCountText();
        }

        if (item >= 3)
        {
            item = 3;
            SetCountText();
        }

        if (Briefcase >= 3)
        {
            Briefcase = 3;
            SetCountText();
        }

        //animate
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("walk", -1, 0f);
            //anim.Play("WAIT00", -1, 0f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("crouch", true);
        }
        else
        {
            anim.SetBool("crouch", false);
        }


        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("run", true);
            speed = 4;
        }
        else
        {
            anim.SetBool("run", false);
            speed = 1;
        }

        if (Input.GetKeyDown(KeyCode.J) && !bulletOut)
        {
            Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bulletOut = true;
            bulletTimer = 0;
            anim.SetBool("shoot", true);
        }
        else
        {
            anim.SetBool("shoot", false);
        }


        if (bulletOut)
        {
            if(bulletTimer == 5)
            {
                bulletOut = false;
            }
            bulletTimer += 1;
        }


        if (Input.GetKey(KeyCode.Space))
        {

            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);

        }



        if (Input.GetKeyDown("u") && item > 0)
        {

            item = item - 1;
            health = health + 1;
            SetCountText();
        }


        if (sMoveType)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

            float curSpeed = Input.GetAxis("Vertical") * speed;

            cc.SimpleMove(transform.forward * curSpeed);
        }
        else
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

                transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

                moveDirection = transform.TransformDirection(moveDirection);

                moveDirection *= speed;

                if (Input.GetButtonDown("Jump"))

                    moveDirection.y = jumpSpeed;

            }

            moveDirection.y -= gravity * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);
        }

        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 2.0f, Color.red);


        //animate
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);
    }

    void SetCountText()
    {       
        Playerhealthtext.text = "Health" + health + "/3";
        Briefcasetext.text = " Briefcase " + Briefcase + "/3";
        itemText.text = "Item" + item + "/3";



        // if health equal to 0 player is dead 
        if (health <= 0)
        {   
            anim.SetTrigger("Death");
            // End Game Text
            Playerhealthtext.text = "You're dead";
            alive = false;
             GameOver();

        }


      
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp("y") && other.gameObject.CompareTag("itemMach"))
        {
            Debug.Log("hit");
            int randomDrop = Random.Range(0, 3);
            int randomPickup = Random.Range(0, pickups.Length );

            if (randomDrop < 2)
            {
                Debug.Log("drop");
                Instantiate(pickups[randomPickup], spawnPointsTransform[randomPickup].position, spawnPointsTransform[randomPickup].rotation);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "eyes")
        {
           other.transform.parent.GetComponent<EnemyRandomMove>().see = true;
            Debug.Log("sees you exit");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "eyes")
        {
            other.transform.parent.GetComponent<EnemyRandomMove>().see = true;
            Debug.Log("sees you enter ");
        }

      

        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("Case"))
        {
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            Briefcase = Briefcase + 1;
            Debug.Log(Briefcase);

            // Run the 'SetCountText()' function (see below)
            SetCountText();

            if (Briefcase == 3)
            {
                scenemanager.Load();
            }
        }



        if (other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            item = item + 1;
            SetCountText();            
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
     
            health -= 1;
            SetCountText();
        }


        if (other.gameObject.CompareTag("EBullet"))
        {

            health -= 1;
            SetCountText();
        }

    }

    // Create Projectiles
    void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bulletOut = true;
        bulletTimer = 0;
    }

    //// Resets game 
    void GameOver()
    {
        SceneManager.LoadScene("Start");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




}
