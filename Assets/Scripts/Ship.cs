using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Drive the chip
/// </summary>
public class Ship : MonoBehaviour
{
    //Support with fire
    [SerializeField]
    GameObject BulletPrefab;
    Renderer rend;


    //Support with move
    [SerializeField]
    float thrustForce = 1;
    [SerializeField]
    int rotateDegreesPerSecond = 1;
    private Vector2 thrustDirection = new Vector2(1, 0);
    private float startRotationZ;

    private Rigidbody2D rb2D;

    //Support with stoping timer
    [SerializeField]
    GameObject HUG; //For the future: better use an event system

    void Start()
    {
        rend = GetComponent<Renderer>();
        rb2D = GetComponent<Rigidbody2D>();
        startRotationZ = 360 - transform.eulerAngles.z;
    }
    private void Update()
    {
        Rotation();
        Fire();
    }

    /// <summary>
    /// Move the ship with help the thrust
    /// </summary>
    private void FixedUpdate()
    {
        var inputThrust = Input.GetAxis("Thrust"); //space button
        if (inputThrust != 0)
        {
            rb2D.AddForce(thrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BigAsteroid" || collision.gameObject.tag == "SmallAsteroid")
        {
            Destroy(this.gameObject);
            HUG.GetComponent<HUG>().StopGame();// For the future: better use an event system
            AudioManager.DontPlayFon();//For the future: better use an event system
            AudioManager.Play(AudioClipName.PlayerDeath);
        }
       Debug.Log($"Ship: detect collision with {collision.gameObject.tag}");
    }

    /// <summary>
    /// Check rotate input and apply rotate if it need
    /// </summary>
    private void Rotation()
    {
        float rotationInput = Input.GetAxis("Rotate");
        // calculate rotation amount and apply rotation and calculate thrustDirection
        if (rotationInput != 0)
        {
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime * rotationInput;
            transform.Rotate(Vector3.forward, rotationAmount);
            var currentRotationZinRadians = (transform.eulerAngles.z + startRotationZ) * Mathf.Deg2Rad;
            thrustDirection = new Vector2(Mathf.Cos(currentRotationZinRadians), Mathf.Sin(currentRotationZinRadians));
        }
    }

    /// <summary>
    /// Check LeftControl and do shot if it need
    /// </summary>
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Find front coordinators of ship
            var edgeShip = rend.bounds.center + (Vector3)(thrustDirection * rend.bounds.extents);
            //Create new bullet
            var currentObject = Instantiate(BulletPrefab, edgeShip, transform.rotation);
            //Give force and direction to the bullet
            currentObject.GetComponent<Bullet>().ApllyForce(thrustDirection+rb2D.velocity);
            //Sound of fire
            AudioManager.Play(AudioClipName.PlayerShot);
        }
    }
}
