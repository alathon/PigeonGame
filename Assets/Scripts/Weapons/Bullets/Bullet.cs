using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float baseSpeed = 0.01f;
    [HideInInspector] public float modifiedSpeed = 1f;
    private Rigidbody2D _rb2D;

    void Awake()
    {
        this._rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // WTF! MAGIC NUMBERS!
        if (Vector2.Distance(this.transform.position, Vector2.zero) > 10)
        {
            this.gameObject.SetActive(false);
        }        
    }

	void FixedUpdate ()
	{
	    var dir = -this.transform.up;
	    var dest = this.transform.position + (dir * baseSpeed * modifiedSpeed);
        this._rb2D.MovePosition(dest);
	}
}