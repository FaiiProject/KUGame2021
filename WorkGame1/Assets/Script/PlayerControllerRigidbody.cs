using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRigidbody : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 2f;
    public float rotSpeed = 30f;
    float newRotY = 0;
    public float jumpPower = 10f;
    public GameObject prefabBullet;
    public GameObject gunPosition;

    public bool hasGun = false;
    public float gunPower = 15f;
    public float gunCooldown = 1f;
    float gunCooldownCount = 0;
    public int bulletCount = 0;

    public int coinCount = 0;
    //public Text uiTextCoin;
    //public Text uiTextBullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;
        float vertical = Input.GetAxis("Vertical") * speed;
        
        if(horizontal > 0)
        {
            newRotY = 90;
        }else if (horizontal < 0)
        {
            newRotY = -90;
        }
        if (horizontal > 0)
        {
            newRotY = 0;
        }else if (horizontal < 0)
        {
            newRotY = 180;
        }

        rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }

        if(Input.GetButtonDown("Fire1") && bulletCount > 0 && (gunCooldownCount >= gunCooldown) )
        {
            gunCooldownCount = 0;
            bulletCount--;
            GameObject bullet = Instantiate(prefabBullet,gunPosition.transform.position,gunPosition.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * gunPower, ForceMode.Impulse);
            Destroy(bullet, 3f);
        }
        gunCooldownCount += Time.fixedDeltaTime;

        transform.rotation = Quaternion.Lerp(
                                                Quaternion.Euler(0, newRotY, 0), 
                                                transform.rotation, 
                                                Time.deltaTime* rotSpeed
                                            );
   } 

   private void OnCollisionEnter(Collision collision)
   {
       if(collision.gameObject.tag == "Collectable")
       {
           Destroy(collision.gameObject);
       }
   }

   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Collectable");
       {
           Destroy(other.gameObject);
           coinCount++;
           //uiTextCoin.text = coinCount.ToString();
           //uiTextBullet.text = bulletCount.ToString();
       }

       if(other.gameObject.name == "GunTrigger")
       {
           hasGun = true;
           bulletCount += 10;
           Destroy(other.gameObject);
       }
   }
}
