using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;
    public float screenWidth;

    private float tileSizeX;
    private Vector2 startPosition;

	void Start () {
        //get the width of the scrollable object
        SpriteRenderer[] srs = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        Bounds bounds = new Bounds(this.transform.position, Vector3.zero);

        foreach (SpriteRenderer renderer in srs)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        
        tileSizeX = bounds.size.x - screenWidth;
        //get the startposition
        Transform startTransform = GetComponent<Transform>();
        startPosition.x = startTransform.position.x;
        startPosition.y = startTransform.position.y;

    }
	
	void Update () {
        //scroll the object according to the defined speed, loop when running out of width
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + Vector2.left * newPosition;
	}
}
