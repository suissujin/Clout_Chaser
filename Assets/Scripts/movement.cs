using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class movement : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    private List<Transform> cutSegments = new List<Transform>();
    public Transform segmentPrefab;
    public Transform sprite;
    public Vector2 direction = Vector2.zero;
    private Vector2 input;
    public int initialSize;
    private int rotAngle;
    private int loopCatch;
    public Collider2D gridArea;
    public HUD hud;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        ResetState();
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        if(hud.endscreen.activeSelf == false)
        {
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Debug.Log("UP");
                input = Vector2.up;
                rotAngle = 270;
                spriteRenderer.flipX = false;
                sprite.Rotate(0, 0, rotAngle);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Debug.Log("DOWN");
                input = Vector2.down;
                rotAngle = 90;
                spriteRenderer.flipX = false;
                sprite.Rotate(0, 0, rotAngle);
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log("RIGHT");
                input = Vector2.right;
                sprite.Rotate(0,0,-rotAngle);
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Debug.Log("LEFT");
                input = Vector2.left;
                sprite.Rotate(0,0,-rotAngle);
                spriteRenderer.flipX = false;
            }
        }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetState();
        }
         if (segments.Count <= 4 && cutSegments.Count == 0 && hud.score < 5)
        {
           hud.endscreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if(hud.endscreen.activeSelf == false)
        {
        // Set the new direction based on the input
        if (input != Vector2.zero)
        {
            direction = input;
        }

        // Set each segment's position to be the same as the one it follows. We
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other.
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(x, y);
        }
    }

    public void ResetState()
    {
        hud.endscreen.SetActive(false);
        hud.score = 0;
        sprite.Rotate(0, 0, -rotAngle);
        direction = Vector2.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            
            if (loopCatch < 100000) 
            {
                Bounds bounds = gridArea.bounds;
                do
                {
                    loopCatch += 1;
                    segment.position = segments[i - 1].position;
                    if (Random.Range(0, 2) == 0)
                    {
                    	segment.position = new Vector2(segment.position.x + Random.Range(-1, 2), segment.position.y);
                	}
                	else
                	{
                    	segment.position = new Vector2(segment.position.x, segment.position.y + Random.Range(-1, 2));
                	}
            	} while (segments.Any(s => s.position == segment.position) && !bounds.Contains(segment.position));
            }
            else 
            {
                segment.position = segments[i - 1].position;
                segment.position = new Vector2(-5,-5);
            }
            segments.Add(segment);
        }

        foreach (var cs in cutSegments)
        {
            Destroy(cs.gameObject);
        }
        cutSegments.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Segment"))
        {
            int segmentIndex = segments.FindIndex(s => s.gameObject == other.gameObject);
            Destroy(segments[segmentIndex].gameObject);
            cutSegments.AddRange(segments.TakeLast(segments.Count - segmentIndex - 1).Select(cs => { cs.gameObject.tag = "cutSegment"; return cs; }));
            segments = segments.Take(segmentIndex).ToList();
            hud.updateScore();

            //Debug.Log(segments.Count);
        }
        else if (other.gameObject.CompareTag("cutSegment"))
        {
            int cutSegmentIndex = cutSegments.FindIndex(cs => cs.gameObject == other.gameObject);
            Destroy(cutSegments[cutSegmentIndex].gameObject);
            cutSegments.RemoveAt(cutSegmentIndex);
            hud.updateScore();
        }
        else if (other.gameObject.CompareTag("WallX"))
        {
            transform.position = new Vector2(transform.position.x * (-1), transform.position.y);
        }
        else if (other.gameObject.CompareTag("WallY"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y * (-1));
        }
    }
}