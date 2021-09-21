using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0; //public is shown in unity
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countLives;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private Rigidbody rb;
    
    private int count; // private isnt shown in unity
    private int lives;
    
    private float movementX; //light blue are Variables
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lives = 2;

        SetCountText();
        winTextObject.SetActive(false);
        
        SetCountLives();
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); //Function Body

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
        if (count == 8) //note that this number should be equal to the number of yellow pickups on the first playfield
        {
            transform.position = new Vector3(50.0f, 0.0f, 50.0f); 
        }
        
        else if(count >= 16) 
        {
            winTextObject.SetActive(true);
        }
    }

    void SetCountLives()
    {
        countLives.text = "Lives: " + lives.ToString();
        
        if(lives == 0) 
        {
            loseTextObject.SetActive(true);
            winTextObject.SetActive(false);
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
{
     if (other.gameObject.CompareTag("PickUp"))
     {
          other.gameObject.SetActive(false);
          count = count + 1; 
          SetCountText();
     }
          
     if (other.gameObject.CompareTag("Enemy"))
     {
          other.gameObject.SetActive(false);
          lives = lives - 1;  
          SetCountLives();
     }

     
} 
}
