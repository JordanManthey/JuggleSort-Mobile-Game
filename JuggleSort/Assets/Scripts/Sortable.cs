using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sortable : MonoBehaviour
{
    public enum SortColor
    {
        Blue,
        Red,
        Purple
    }

    public SortColor Color { get; set; }
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private bool _moveAllowed;

    bool lockedOn = false;

    public static SortColor GetRandomSortColor()
    {
        Array colors = Enum.GetValues(typeof(SortColor));
        int randomIndex = UnityEngine.Random.Range(0, colors.Length);
        SortColor randomColor = (SortColor) colors.GetValue(randomIndex);
        return randomColor;
    }

    public void SetColor(SortColor sortColor)
    {
        Color = sortColor;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (sortColor)
        {
            case SortColor.Blue:
                spriteRenderer.color = UnityEngine.Color.blue;
                break;
            case SortColor.Red:
                spriteRenderer.color = UnityEngine.Color.red;
                break;
            case SortColor.Purple:
                spriteRenderer.color = UnityEngine.Color.magenta;
                break;
        }
    }

    public void HandleTouch()
    {
        float touchTimeStart = 0f, touchTimeEnd, touchTimeInterval;
        Vector2 startPos = Vector2.zero, endPos, direction;
        //float maxThrowForce;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

                if (_collider == touchedCollider)
                {
                    _moveAllowed = true;
                    touchTimeStart = Time.time;
                    startPos = touch.position;
                    lockedOn = true;
                }
            }

            // might need to remove this
            /*
            if (touch.phase == TouchPhase.Moved)
            {
                if (_moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }
            */

            if (touch.phase == TouchPhase.Ended && lockedOn)
            {
                _moveAllowed = false;
                touchTimeEnd = Time.time;
                touchTimeInterval = touchTimeEnd - touchTimeStart;
                endPos = touch.position;
                direction = startPos - endPos;
                _rigidbody.AddForce(-direction / touchTimeInterval);
            }
        }
    }

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleTouch();
    }

}
