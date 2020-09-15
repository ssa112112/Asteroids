using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen wrap for evrythink with CapsuleCollider2D
/// </summary>
public class ScreenWrapper : MonoBehaviour
{
    //Support with wrap
    private float colliderHight;
    private float colliderWidth;

    [SerializeField]
    bool withCoagulation = true;

    void Start()
    {
        //Save shape's collider
        CapsuleCollider2D cc2D = GetComponent<CapsuleCollider2D>();
        colliderHight = cc2D.size.x;
        colliderWidth = cc2D.size.y;
    }

    void Update()
    {
        //Screen wrapping
        transform.position = ScreenUtils.CheсkBorders(transform.position, colliderWidth, colliderHight, withCoagulation: withCoagulation);
    }
}
