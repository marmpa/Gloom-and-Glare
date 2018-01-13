using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeetCollition : MonoBehaviour {

    public GameObject player;       


    private Vector3 offset;
    void Start ()
    {
        offset = transform.position - player.transform.position;
        
    }
	
	
	void LateUpdate ()
    {
       transform.position = player.transform.position + offset;
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("yes "+ collider.gameObject.tag);
        if(collider.gameObject.CompareTag("end"))
        {
            if(PlayerPrefs.GetInt("levelReached")<SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex);
            }
            SceneManager.LoadScene("Menu");
        }
        
    }

    
}
