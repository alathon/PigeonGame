using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
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
            Destroy(this.gameObject);
        }        
    }

	void FixedUpdate ()
	{
	    var dir = this.transform.up;
	    var dest = this.transform.position + (dir * speed);
        this._rb2D.MovePosition(dest);
	}
}