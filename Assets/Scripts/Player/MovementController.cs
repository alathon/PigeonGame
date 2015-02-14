using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
    private Rigidbody2D rb2d;
    
    [SerializeField]
    private float _baseMovement = 100f;

    void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveDir)
    {
        var final = new Vector2(moveDir.x * _baseMovement * Time.fixedDeltaTime, moveDir.y * _baseMovement * Time.fixedDeltaTime);
        this.rb2d.velocity = final;
    }
}