using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float speed=10;
    private GameObject player;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
}
