using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A bullet. Need to use method apple force for give force!
/// </summary>
public class Bullet : MonoBehaviour
{
    const float StartImpulse = 1.15f;
    const float TimeOfLife = 2f;

    private void Start()
    {
        Destroy(gameObject, TimeOfLife);
    }

    /// <summary>
    /// Give forse to the bullet with given direction
    /// </summary>
    /// <param name="thrustDirection"></param>
    public void ApllyForce(Vector2 thrustDirection)
    {
        var rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForce(thrustDirection * StartImpulse, ForceMode2D.Impulse);
    }

}
