using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public const float impactPower = 200f;
    public const float recoilPower = 0.5f;


    private void OnCollisionEnter(Collision collision)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "Player")
        {
            Vector3 oldVelocity = this.rb.velocity;
            Debug.Log("Impulse: " + collision.impulse);
            Debug.Log("Collided with " + collision.gameObject.name);


            //rb.velocity = oldVelocity * recoilPower;
            //collision.rigidbody.velocity = (collision.impulse * impactPower);

            Vector3 colPos = collision.transform.position;
            Vector3 direction = new Vector3(colPos.x - transform.position.x, 0f, colPos.z - transform.position.z);


            //rb.AddForce(-direction + (impactPower * collision.relativeVelocity));

            Vector3 veloDifference = collision.impulse - rb.velocity;
            rb.AddForce(veloDifference * -impactPower);


//            rb.AddForce(-collision.impulse * impactPower);

        }
    }
}
