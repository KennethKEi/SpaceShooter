using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speedPowerUp = 3.5f;

    [SerializeField] // 0=Triple Shot, 1=speed 2=shields
    private int powerupID;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speedPowerUp * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            /*
            if (player != null)
            {
                if (powerupID == 0)
                player.TripleShotActive();
            }
            else if (powerupID == 1)
            {
                Debug.Log("collected speed");
            }
            else if (powerupID == 2)
            {
                Debug.Log("collected shields");
            }
            */
            switch(powerupID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
               case 1:
                    player.SpeedBoostActive();
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
           Destroy(this.gameObject);
        }
         
            


      



    }
    
}
