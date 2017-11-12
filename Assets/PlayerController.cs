using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameObject cam; //This is the main camera that is a child of the player
    float speed;//Value to change walking speed

    public BoxCollider buttonCol; //the collider on button1
    public BoxCollider buttonCol2; //the collider on button2

    int buttonPushTimer = 0;
    int tracker = 0;
    bool didVisit1;
    bool didVisit2;

    public Image backgroundCredits;

    public Collider viewWall1;
    public Collider viewWall2;

    public Text credits;
    public Text title;
    bool fadeTitle;
    float titleAlpha;

    int button1Timer;
    int button2Timer;

    int button1Rest;
    int button2Rest;

    bool button1Pushed;
    bool button2Pushed;

    public Text button1Timer_text;
    public Text button2Timer_text;

    public Text end_text;

    public Text button1Timer_world;
    public Text button2Timer_world;

    public AudioSource crash;
    public AudioSource swing;

    bool swinging;

    float totalTime;

    bool collide;

   public AudioSource but1;
    public AudioSource but2;

    public Camera cam2;//the backwards facing camera

    //Test Monsters
    //public GameObject testMonster;

    public GameObject floor;
    public GameObject wallCap1;
    public GameObject ceiling;
    public GameObject plant;
    public GameObject timer1;
    public GameObject button1_platform;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject ceilingLightModel2;
    public Material bigFloor;
    public Material bigCeiling;
    public Material bigWall;
    public GameObject pedestal;

    public Material smallFloor;
    public Material smallCeiling;
    public Material smallWall;

    public Light light1_ceiling;
    public Material greenLightMat;
    public MeshRenderer light1_model;
    public GameObject light1_ceiling_obj;

    public GameObject timer1_obj;
    public GameObject timer2_obj;

    public GameObject plant2;

    public Material lightMaterial;

    public GameObject fakeTimer;
    Quaternion orig_rot;

    public GameObject buttonExplosion;

    bool backTo1;
    bool backTo2;

    int monsterCounter;

    bool noMove;//can the player move
    float endingFlip;

    //Buttons
    public Animator button1;
    public Animator button2;

    bool playerMoved;

    public AudioSource footsteps;

   /* void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "wall")
        {
            //prevent bounce
            speed = 0;
            collide = true;
        }
     
    }*/

    void resetMonsters()
    {
        Debug.Log("resetting");
        //This function resets the hallway and is called after each monster
        switch (monsterCounter-1)
        {
            case 1:
                {
                    timer2_obj.transform.localScale = new Vector3(timer2_obj.transform.localScale.x, timer2_obj.transform.localScale.y * -1, timer2_obj.transform.localScale.z);
                    break;
                }
            case 2:
                {
                    timer1_obj.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                    break;
                }
            case 3:
                {
                    Renderer[] allChildren = plant2.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer child in allChildren)
                    {
                        child.enabled = false;
                    }
                    break;
                }
            case 4:
                {
                    //rotate back
                    timer1_obj.gameObject.GetComponent<Image>().enabled = true;
                    timer1_obj.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
                    fakeTimer.gameObject.GetComponent<Image>().enabled = false;
                    fakeTimer.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    swinging = false;
                    break;
                }
            case 5:
                {
                    light1_ceiling.color = Color.white;
                    light1_model.material = lightMaterial;
                    break;
                }
            case 6:
                {
                    //sound
                    break;
                }
            case 7:
                {
                    //hallway return
                    //player
                    gameObject.transform.position = new Vector3(0, 0.93f, 0.87f);

                    floor.transform.localScale = new Vector3(floor.transform.localScale.x, floor.transform.localScale.y, 1);
                    floor.GetComponent<MeshRenderer>().material = smallFloor;
                    //move the end cap back
                    wallCap1.transform.position = new Vector3(wallCap1.transform.position.x, wallCap1.transform.position.y, 4.96f);
                    //move plant, button, and timer
                    plant.transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y, 4.528f);
                    button1_platform.transform.position = new Vector3(button1_platform.transform.position.x, button1_platform.transform.position.y, 4.52f);
                    timer1.transform.position = new Vector3(timer1.transform.position.x, timer1.transform.position.y, 4.92f);
                    pedestal.transform.position = new Vector3(pedestal.transform.position.x, pedestal.transform.position.y, 4.34f);
                    //move light
                    ceilingLightModel2.transform.position = new Vector3(ceilingLightModel2.transform.position.x, ceilingLightModel2.transform.position.y, 3.68f);
                    light1_ceiling_obj.transform.position = new Vector3(light1_ceiling_obj.transform.position.x, light1_ceiling_obj.transform.position.y, 1.12f);
                    //extend ceiling
                    ceiling.transform.localScale = new Vector3(ceiling.transform.localScale.x, ceiling.transform.localScale.y, 1);
                    ceiling.GetComponent<MeshRenderer>().material = smallCeiling;
                    //scale walls
                    wall1.transform.localScale = new Vector3(wall1.transform.localScale.x, wall1.transform.localScale.y, 1);
                    wall2.transform.localScale = new Vector3(wall2.transform.localScale.x, wall2.transform.localScale.y, 1);
                    wall1.GetComponent<MeshRenderer>().material = smallWall;
                    wall2.GetComponent<MeshRenderer>().material = smallWall;
                    break;
                }
            case 8:
                {
                    Renderer[] allChildren = buttonExplosion.gameObject.GetComponentsInChildren<Renderer>();
                    foreach (Renderer child in allChildren)
                    {
                        child.enabled = false;
                        Renderer[] chils = child.gameObject.GetComponentsInChildren<Renderer>();
                        foreach (Renderer o in chils)
                        {
                            o.enabled = false;
                        }
                    }
                    break;
                }
            case 9:
                {
                    Vector3 rotationVector = transform.rotation.eulerAngles;
                    rotationVector.z = 0f;
                    transform.rotation = Quaternion.Euler(rotationVector);
                    break;
                }
            case 10:
                {
                    speed = 4f;
                    break;
                }
        }
    }

    void CheckMonster()
    {
        Debug.Log(monsterCounter);
        //This function keeps track of what monster to display and when monster appearances are triggered
        //Test Case: Purple Sphere
        if (backTo2 && monsterCounter == 6)
        {
            // 7 Stretch wall
            //You start facing button 2
            floor.transform.localScale = new Vector3(floor.transform.localScale.x, floor.transform.localScale.y, 100);
            floor.GetComponent<MeshRenderer>().material = bigFloor;
            //move the end cap back
            wallCap1.transform.position = new Vector3(wallCap1.transform.position.x, wallCap1.transform.position.y, 25);
            //move plant, button, and timer
            plant.transform.position = new Vector3(plant.transform.position.x, plant.transform.position.y, 24.57f);
            button1_platform.transform.position = new Vector3(button1_platform.transform.position.x, button1_platform.transform.position.y, 24.268f);
            timer1.transform.position = new Vector3(timer1.transform.position.x, timer1.transform.position.y, 24);
            pedestal.transform.position = new Vector3(pedestal.transform.position.x, pedestal.transform.position.y, 24.1f);
            //move light
            ceilingLightModel2.transform.position = new Vector3(ceilingLightModel2.transform.position.x, ceilingLightModel2.transform.position.y, 24);
            light1_ceiling_obj.transform.position = new Vector3(light1_ceiling_obj.transform.position.x, light1_ceiling_obj.transform.position.y, 24);
            //extend ceiling
            ceiling.transform.localScale = new Vector3(ceiling.transform.localScale.x, ceiling.transform.localScale.y, 100);
            ceiling.GetComponent<MeshRenderer>().material = bigCeiling;
            //scale walls
            wall1.transform.localScale = new Vector3(wall1.transform.localScale.x, wall1.transform.localScale.y, 100);
            wall2.transform.localScale = new Vector3(wall2.transform.localScale.x, wall2.transform.localScale.y, 100);
            wall1.GetComponent<MeshRenderer>().material = bigWall;
            wall2.GetComponent<MeshRenderer>().material = bigWall;
            //fixing textures on floor and cieling and walls
            //reassign the material?

            monsterCounter++;
        }
        if (backTo2 && monsterCounter == 4)
        {
            // 5 change light color
            light1_ceiling.color = Color.green;
            light1_model.material = greenLightMat;
            monsterCounter++;
        }
        if (backTo2 && monsterCounter == 0)
        {
            // 1 Flip over 
            timer2_obj.transform.localScale = new Vector3(timer2_obj.transform.localScale.x, timer2_obj.transform.localScale.y * -1, timer2_obj.transform.localScale.z);
            monsterCounter++;
        }

        if (backTo1 && monsterCounter == 1)
        {
            // 2 Change timer color
            //timer1_obj.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.magenta;
            timer1_obj.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255,79,0);
            monsterCounter++;
        }

        if (backTo2 && monsterCounter == 2)
        {
            // 3 two plants
            Renderer[] allChildren = plant2.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer child in allChildren)
            {
                child.enabled = true;
            }
            monsterCounter++;
        }

        if (backTo1 && monsterCounter == 3)
        {
            // 4 Turn the timer
            timer1_obj.gameObject.GetComponent<Image>().enabled = false;
            timer1_obj.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
            fakeTimer.gameObject.GetComponent<Image>().enabled = true;
            fakeTimer.gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
            swinging = true;
            monsterCounter++;
        }

        if (backTo1 && monsterCounter == 5)
        {
            // 6 Crash Sound
            crash.Play();
            monsterCounter++;
        }

        if (backTo2 && monsterCounter == 8)
        {
            // 9 Tilted player
            //z = -34.241
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 66.028f;
            transform.rotation = Quaternion.Euler(rotationVector);

            monsterCounter++;
        }

        if (backTo1 && monsterCounter == 7)
        {
            // 8 buttons explosions
            Renderer[] allChildren = buttonExplosion.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer child in allChildren)
            {
                child.enabled = true;
                Renderer[] chils = child.gameObject.GetComponentsInChildren<Renderer>();
                foreach(Renderer o in chils)
                {
                    o.enabled = true;
                }
            }
            monsterCounter++;
        }

        if (backTo1 && monsterCounter == 9)
        {
            // 10 reduced speed
            speed = 2f;
            monsterCounter++;
        }

        if (backTo2 && monsterCounter == 10)
        {
            // 11 double vision
            //move the main display
            cam.GetComponent<Camera>().rect = new Rect(0, 0, .5f, 1f);
            //move the second display
            cam2.GetComponent<Camera>().enabled = true;
            cam2.GetComponent<Camera>().rect = new Rect(0.5f, 0, .5f, 1f);
            monsterCounter++;
        }
    }

    // Use this for initialization
    void Start () {

        titleAlpha = 255f;
        cam2.GetComponent<Camera>().enabled = false;
        endingFlip = 0f;
        speed = 4f;

        button1Timer = 1800;
        button2Timer = 1800;

        button1Rest = 0;
        button2Rest = 0;

        button1Pushed = false;
        button2Pushed = false;

        //All Monsters start invisible
       // testMonster.GetComponent<MeshRenderer>().enabled = false;

        backTo2 = false;
        backTo1 = true;

        monsterCounter = 0;

        Renderer[] allChildren = plant2.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in allChildren)
        {
            child.enabled = false;
        }

        orig_rot = timer1_obj.gameObject.transform.rotation;

        Renderer[] allChildren1 = buttonExplosion.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer child in allChildren1)
        {
            child.enabled = false;
            Renderer[] chils = child.gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer o in chils)
            {
                o.enabled = false;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked;
        ///Restart Scene on Escape Press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
        //Handle player movement ->See Camera Controller
        //They did this way way way better than I did
        //////////////////////////////////////////////////

        if (Input.GetKey(KeyCode.W))
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            footsteps.Stop();
        }

        //Get player's input for what direction they want to go in
        if (!noMove)
        {
            float z = Input.GetAxis("Vertical") * Time.deltaTime;
            gameObject.transform.position += z * cam.transform.forward * speed;
        }
        ///PUT THIS BACK IN
        else if(endingFlip > -96)
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            //rotationVector.z = -96;
            rotationVector.z = endingFlip;
            endingFlip -= 2;
            transform.rotation = Quaternion.Euler(rotationVector);
        }


        if (Input.anyKey)
        {
            playerMoved = true;
            //fade out the title
            fadeTitle = true;
            title.CrossFadeAlpha(0.0f,1f, false);
            backgroundCredits.CrossFadeAlpha(0.0f, 1f, false);
        }
        

        if (swinging)
        {
            if (!swing.isPlaying)
            {
                swing.Play();
            }
        }
       
        /* float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
         float z = Input.GetAxis("Vertical") * Time.deltaTime;

         //Get where the mouse is pointing
         float h = 1f * Input.GetAxis("Mouse X");
         float v = 2f * Input.GetAxis("Mouse Y");

         //Rotate the camera based on the player's rotation and rotate the player object
        /* cam.transform.Rotate(0, h, 0);
         cam.transform.Rotate(0, 0, v);//test
         gameObject.transform.Rotate(0, h, 0);

         //Move the player in the direction the camera is facing
         gameObject.transform.position += z * cam.transform.forward;
        // gameObject.transform.position += x * cam.transform.forward;

         //Move the player left or right without using the camera THIS IS BUGGY (not cam dependent)
         //gameObject.transform.position += new Vector3(x, 0, 0);*/
        //Freeze Y movement
        this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.93f, gameObject.transform.position.z);

        //Debug.Log("back to 1:" + backTo1);
        //  Debug.Log("back to 2:" + backTo2);
        //Check to see if the player is looking at a button
        //////////////////////////////////////////////////
        //checking position
        //Cast a ray from screen center into space
       /* Ray ray1 = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);//show the debug ray
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit1, 100f)) //the 10f is the length the ray extends in distance
        {
            if (hit1.collider == viewWall1)
            {
                //You aren't looking at button 2
                backTo1 = false;
                backTo2 = true;
            }
            if(hit1.collider == viewWall2)
            {
                Debug.Log("Looking at button 2");
                //You aren't looking at button 2
                backTo1 = true;
                backTo2 = false;
            }
            }*/
            //Cast a ray from screen center into space
            Ray ray = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);//show the debug ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.5f)) //the 10f is the length the ray extends in distance
        {
            //A collision occured between the ray and a thing
            if (hit.collider == buttonCol)
            {
                //You aren't looking at button 2
                backTo1 = false;
                backTo2 = true;
                //Was the collision on Button 1
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!didVisit1)
                    {
                        tracker++;
                        if (tracker >= 3)
                        {
                            resetMonsters();
                        }
                        CheckMonster();
                    }
                    didVisit1 = true;
                    didVisit2 = false;
                    //If the player clicks while looking at the button:
                    //Register the button click
                    button1Pushed = true;
                    button1Rest = 0;
                 
                    Debug.Log("Hite button 1");
                    //play the animation
                    button1.SetBool("isPushed",true);
                    if (!but1.isPlaying)
                    {
                        but1.Play();
                    }
                    //reset

                    //Check for monsters
                   // CheckMonster();

                }
                else
                {
                    button1.SetBool("isPushed", false);
                    if (backTo1)
                    {
                       // didVisit1 = false;
                    }
                    
                }
            }
            if (hit.collider == buttonCol2)
            {
                Debug.Log("LOOKED AT BUTTON 2");
                //You aren't looking at button 1
                backTo1 = true;
                backTo2 = false;
                //Was the collision on Button 2
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!didVisit2)
                    {
                        tracker++;
                        if (tracker >= 3)
                        {
                            resetMonsters();
                        }
                        CheckMonster();
                    }
                    didVisit2 = true;
                    didVisit1 = false;
                    //If the player clicks while looking at the button:
                    //Register the button click
                    button2Pushed = true;
                    button2Rest = 0;
                   
                    Debug.Log("Hite button 2");
                    //play the animation
                    button2.SetBool("isPushed", true);
                    if (!but2.isPlaying)
                    {
                        but2.Play();
                    }
                    //reset
                   // CheckMonster();
                    // resetMonsters();//the problem
                    //Check for monsters

                }
                else
                {
                    button2.SetBool("isPushed", false);
                    if (backTo2)
                    {
                        //didVisit = false;
                    }
                }

            }
        }
      
        //Handle Timers
        //////////////////////////////////////////////////
        //A timer will decrement as long as the player is not pressing that button
        

        if (!button1Pushed && playerMoved)
        {
            button1Timer--;
        }
        else
        {
            //See if the player has pushed this button recently
            button1Rest++;
            if(button1Rest > 15)
            {
                //if X frames have passed, start the count again
                button1Rest = 0;
                button1Pushed = false;

            }
        }
        if (!button2Pushed && playerMoved)
        {
            button2Timer--;
        }
        else
        {
            //See if the player has pushed this button recently
            button2Rest++;
            if (button2Rest > 15)
            {
                //if X frames have passed, start the count again
                button2Rest = 0;
                button2Pushed = false;

            }
        }
        // Debug.Log(button1Timer + " " + button2Timer);
     


        //Check for ending
        if(button1Timer <= 0 || button2Timer <= 0)
        {
            //If either timer hits zero, game is over
            //Display the player's time as a score
            button2.SetBool("isPushed", false);
            button1.SetBool("isPushed", false);
            end_text.text = "You've got a one track mind.\nTime worked: " + Mathf.Round(totalTime / 30) + " seconds.\nPress P to work again.";
            button1Timer_text.text = "00:00";
            button2Timer_text.text = "00:00";

            //tump over the player
         
            //AD THIS BACK IN
           noMove = true;
            

        }
        else
        {
            
            button1Timer_text.text = "Button 1 Timer: " + button1Timer / 30;
            button2Timer_text.text = "Button 2 Timer: " + button2Timer / 30;
            if(button2Timer/30 < 10)
            {
                button2Timer_world.text = "00:0" + button2Timer / 30;
            }
            else
            {
                button2Timer_world.text = "00:" + button2Timer / 30;
            }
            if (button1Timer/30 < 10)
            {
                button1Timer_world.text = "00:0" + button1Timer / 30;
                //fakeTimer.GetComponent<Text>().text = "00:0" + button1Timer / 30;
            }
            else
            {
                button1Timer_world.text = "00:" + button1Timer / 30;
                //fakeTimer.GetComponent<Text>().text = "00:" + button1Timer / 30;
            }

            if (playerMoved)
            {
                totalTime++;
            }
            
        }

        //Handle Creepy Events when player's back is turned
       

    }
}
