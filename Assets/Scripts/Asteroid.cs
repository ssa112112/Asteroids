using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //Support with create smaller asteroids 
    //For future - better use a particle system
    const float MinimalScale = 0.25f; //[including]
    const float TrashScale = 0.1f;  
    const float TrashAmount = 10f;
    const float TrashLifeTime = 1f;


    //This is list of sprites for our rocks
    [SerializeField]
    Sprite[] customSprites;

    //This is will be random number for sprite
    private byte random;

    //Apply impulse force to get game object moving
    const float MinImpulseForce = 0.05f;
    const float MaxImpulseForce = 0.3f;

    //Apply torque force to get game object moving
    const float MinTorque = -0.01f;
    const float MaxTorque = 0.01f;

    private Rigidbody2D rb2D;

    public void Initialize (Direction direction)
    {
        //Get the shape of asteroid
        rb2D = GetComponent<Rigidbody2D>();

        //Generate new random number for sprite
        random = (byte)Random.Range(0, customSprites.Length);
        //And change sprite
        GetComponent<SpriteRenderer>().sprite = customSprites[random];

        //Random angle from 0 degrees to 30 degrees in radians and add an corrected angle for current direction
        float randomAngle = Random.Range(0, Mathf.PI / 6) + GetCorrectedAngle(direction);
        //Give random impulse and random vector
        Vector2 moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        rb2D.AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
        //Give random torque
        rb2D.AddTorque(Random.Range(MinTorque, MaxTorque), ForceMode2D.Impulse);
    }

    public void StartMoving()
    {
        //Get the shape of asteroid
        rb2D = GetComponent<Rigidbody2D>();

        //Give random impulse and random vector
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        rb2D.AddForce(Random.insideUnitCircle * magnitude, ForceMode2D.Impulse);
        //Give random torque
        rb2D.AddTorque(Random.Range(MinTorque, MaxTorque), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            if (transform.localScale.x > MinimalScale)
            {
                CreateTwoSmallerAsteroids();
            }
            else
                CreateTrash();
            
            //Sound of hit
            if (gameObject.tag != "Trash")
                AudioManager.Play(AudioClipName.AsteroidHit);


            Destroy(this.gameObject);
        }
    }

    private float GetCorrectedAngle(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                return -15f * Mathf.Deg2Rad;
            case Direction.Up:
                return 75f * Mathf.Deg2Rad;
            case Direction.Left:
                return 165f * Mathf.Deg2Rad;
            case Direction.Down:
                return 255f * Mathf.Deg2Rad;
            default:
                throw new KeyNotFoundException("Unknown direction");
        }
    }

    public float GetColliderHight()
    {
        //Return colliders's hight 
        return GetComponent<CapsuleCollider2D>().size.x;
    }

    public float GetColliderWidth()
    {
        //Return colliders's width 
        return GetComponent<CapsuleCollider2D>().size.y;
    }

    /// <summary>
    /// Create two asteroids with localScale devide on 2
    /// </summary>
    private void CreateTwoSmallerAsteroids()
    {
        //Create template
        var newScale = transform.localScale / 2;
        var newAsteroid = gameObject;
        newAsteroid.transform.localScale = newScale;

        //Instantiate two small asteroids
        for (var i=0;i<2;i++)
        {
            var currentObject = Instantiate(newAsteroid, transform.position, Quaternion.identity);
            currentObject.GetComponent<Asteroid>().StartMoving();
            currentObject.tag = "SmallAsteroid";
        }
    }

    /// <summary>
    /// Create trash
    /// </summary>
    private void CreateTrash()
    {
        //Create template
        var newScale = transform.localScale * TrashScale;
        var newAsteroid = gameObject;
        newAsteroid.transform.localScale = newScale;

        //Instantiate {TrashAmount} small asteroids
        for (var i = 0; i < TrashAmount; i++)
        {
            var currentObject = Instantiate(newAsteroid, transform.position, Quaternion.identity);
            currentObject.GetComponent<Asteroid>().StartMoving();
            currentObject.tag = "Trash";
            Destroy(currentObject, TrashLifeTime);
        }
    }
}
