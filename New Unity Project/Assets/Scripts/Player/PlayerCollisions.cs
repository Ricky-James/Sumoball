using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public const float impactPower = 60f;
    public const float recoilPower = 25f;


    private void OnCollisionEnter(Collision collision)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "Player" && this.GetComponentInParent<Player>().PV.IsMine)
        {
            
            Vector3 oldVelocity = this.rb.velocity;
            Debug.Log("Impulse: " + collision.impulse);
            Debug.Log("Contact point: " + collision.GetContact(0).point);
            Debug.Log("RB Pos: " + rb.position);
            Debug.Log("Col Pos: " + collision.transform.position);


            //rb.velocity = oldVelocity * recoilPower;
            //collision.rigidbody.velocity = (collision.impulse * impactPower);

            //  Vector3 colPos = collision.transform.position;
            //Vector3 direction = new Vector3(colPos.x - transform.position.x, 0f, colPos.z - transform.position.z);


            //rb.AddForce(-direction + (impactPower * collision.relativeVelocity));

            //  rb.AddForce(veloDifference * -impactPower);

            //  rb.AddForce(collision.impulse * -impactPower);
            //Vector3 direction = rb.position - collision.transform.position;
            //Debug.DrawLine(rb.position, collision.rigidbody.position, Color.red, 10.0f);
            //
            //rb.velocity = rb.velocity + (direction * -rb.velocity.magnitude);
            //

            //           

            Vector3 direction = collision.GetContact(0).point - rb.position;
            collision.rigidbody.velocity = collision.rigidbody.velocity + direction.normalized * impactPower;
            direction = -direction.normalized;
            rb.velocity = rb.velocity + (direction * recoilPower);

            
        }
    }
}
