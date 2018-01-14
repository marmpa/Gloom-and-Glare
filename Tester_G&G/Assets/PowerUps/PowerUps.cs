using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    float multiplier = 0.5f;
    float slower = 0.5f;
    float seconds = 8f;

    public GameObject pickupEffect;
    public GameObject power;

    void FixedUpdate()
    {

    }
    void OnTriggerEnter (Collider other)
    { 
        if(power.gameObject.CompareTag("good"))
        {
            StartCoroutine(BecomeSmaller(other));
        }
        if (power.gameObject.CompareTag("evil"))
        {
            StartCoroutine(SlowMotion());
        }
    }
    IEnumerator BecomeSmaller(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        player.transform.localScale *= multiplier;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(seconds);
        player.transform.localScale *= 1/multiplier;
        Destroy(gameObject);
    }
    IEnumerator SlowMotion()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        Time.timeScale = slower;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(seconds);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
