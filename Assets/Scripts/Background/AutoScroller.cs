using UnityEngine;
using System.Collections;

public class AutoScroller : MonoBehaviour {
    public Vector2 scrollPerUpdate = new Vector2(0, -2.5f);
    public float fudgeFactor = -2f;

    [SerializeField] private bool setInactiveOffscreen = true;

    [SerializeField] private Transform targetTransform;

    private Vector3 movement;

    void Awake()
    {
        this.SetScrollspeed(this.scrollPerUpdate);
    }

    public void SetScrollspeed(Vector2 speed)
    {
        this.scrollPerUpdate = speed;
        this.movement = new Vector3(scrollPerUpdate.x, scrollPerUpdate.y, 0f);
    }

    void Update()
    {
        targetTransform.Translate(movement * Time.deltaTime);
        
        if (!setInactiveOffscreen) return;

        if (Vector2.Distance(targetTransform.position, Vector2.zero) > fudgeFactor)
        {
            targetTransform.gameObject.SetActive(false);
        }
    }
}