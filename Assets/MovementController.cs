using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController: MonoBehaviour {
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private float moveX;
    public float jumpAmount = 10;
    public List<Moon> moons = new();
    private bool enoughMoons = false;
    public TextMeshProUGUI doorText;
    private bool isGrounded;

    // Start is called before the first frame update
    void Awake() {
        doorText.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);

        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GameOver"))
        {
            transform.position = new Vector3(-8.5f, -2.0f, 0);
        }

        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Moon")) {
            moons.Add(col.gameObject.GetComponent<Moon>());
            Destroy(col.gameObject);

            if (moons.Count == 10) enoughMoons = true;
        }
        if (col.gameObject.CompareTag("Door")) {
            if (enoughMoons) {
                SceneManager.LoadScene("SecretRoom");
            }
            else {
                doorText.enabled = true;
            }
        }
    }
}