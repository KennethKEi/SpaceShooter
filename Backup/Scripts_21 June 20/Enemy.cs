using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //respawn at top with new random x position 

        if(transform.position.y <= -5f)
        {
            transform.position = new Vector3 (Random.Range(-9.3f, 9.3f), 5.7f, 0);

            //OR

            //float randomX = Random.Range(-9.3f, 9.3f);
            //transform.position = new Vector3(randomX, 5.7f, 0);
        }


    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            //other.transform.GetComponent<Player>().Damage();

            Player player = other.GetComponent<Player>();
            
            //null checking
            if (player != null)
            {
                player.Damage();
            }
            
          
            Destroy(this.gameObject);
            
        }
    
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
