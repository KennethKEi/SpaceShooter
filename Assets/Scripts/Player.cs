using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
    All variable need 
    public or private reference 
    data type (int, float, bool or string)
    every variable has a name
    (optional) value assigned
    */

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefabs;

    private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private Spawn_Manager _Spawn_Manager;
    
    [SerializeField]
    private GameObject tripleShotPrefabs;
    
    [SerializeField]
    private bool _tripleShotActive = false;

    [SerializeField]
    private bool _speedBoostActive = false;



    // Start is called before the first frame update
    void Start()
    {
        //Setting new position of a game object, ALWAYS use Vector3, and when using vector3, ALWAYS use new
        transform.position = new Vector3(0, 0, 0);
        _Spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

        if (_Spawn_Manager == null)
        {
            Debug.LogError("the spawn manager is not called");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        //Spawn gameobject when "Space" key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            SpawnGameObject();


    }

    void SpawnGameObject()
    {
       
        {
            _canFire = Time.time + _fireRate;

            if (_tripleShotActive == true)
                Instantiate(tripleShotPrefabs, transform.position, Quaternion.identity);
            else
                Instantiate(_laserPrefabs, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);


            //time.time is the total time the game has been running, by adding a -1 in _canFire, 
            //the if statement is true, then by setting _canfire = time.time plus 0.5f (firerate),
            //the if statement is not ture, until time.time increase to a point where it covered time.time + firerate


            // Just spawn
            //Instantiate(_laserPrefabs, transform.position, Quaternion.identity);

            //spawn with offset




        }
    }

    public void Damage()
    {
        //all means the same thing, take one live of the player when this method is called
        _lives--;
        //_lives = _lives - 1;
        //_lives -= 1;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _Spawn_Manager.OnPlayerDeath();
        }
    }
    void CalculateMovement ()
    {
        //*********************** Game Object Movement************************************
        //using Unity default input manager setting to control game object, horizontal is defined in 
        //edit --> Project Settings --> Input manager --> Horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //time.deltaTime = real time 
        // vector3.right = new vector3(1, 0, 0) * 0 * 3.5f * real time
        //the 1 in vector 3 represent ritht, if it is -1, then it represent left

        if(_speedBoostActive == false)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * (_speed * 2) * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * (_speed * 2) * Time.deltaTime);
        }
            
        
    
 

        /* Other cleaner ways to do the above script 
         * 1. Use new vector3 and specify x and y with horizontalInput and verticalInput
         
        transform.Translate(new Vector3 (horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        
         2. adding a new varible called "direction" and set x and y to horizontalInput and VerticalInput

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        */

        //****************** Set Boundary for Game Object****************************

        // If Game object move to the end of the scene horizontally, snap it back to the other side
        if (transform.position.x >= 9.184124)
        //it is transform.position becuase in the inspector panel, 
        //transform is a category, positon is one of the varibale within it
        {
            transform.position = new Vector3(-9.184124f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.184124)
        {
            transform.position = new Vector3(9.184124f, transform.position.y, 0);
        }

        // Set Ceiling and floor

               
        if (transform.position.y >= 5.318352)
        {
            transform.position = new Vector3(transform.position.x, 5.318352f, 0);
        }
        else if (transform.position.y <= -3.18679)
        {
            transform.position = new Vector3(transform.position.x, -3.18679f, 0);
        }
        //OR use clamping (mathf.clamp) to set min and max value of y
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, - 3.18f, 3.18f), 0);

    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
   
    IEnumerator TripleShotPowerDownRoutine()
    {
        while (_tripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _tripleShotActive = false;

        }
    }

    public void SpeedBoostActive()
    {
        _speedBoostActive = true;
        _speed = +_speed * 2;
        StartCoroutine (speedboostRoutine());
    }
    IEnumerator speedboostRoutine()
    {
        while (_speedBoostActive == true)
        {
            yield return new WaitForSeconds(5f);
            _speedBoostActive = false;
            _speed = 3.5f;
        }
                
    }

}


