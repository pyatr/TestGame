using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquishyBalloon : AbstractBalloon
{
    [SerializeField]
    private Vector2 maxDelta = new Vector2(16, 16);

    [SerializeField]
    private float maxTouchDistance = 16f;

    protected bool IsTouchNear(Touch touch)
    {
        Vector2 bp = Camera.main.WorldToScreenPoint(transform.position);
        float mag = (touch.position - bp).magnitude;
        return mag <= maxTouchDistance;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.touchCount >= 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];
            if (IsTouchNear(touch1) && IsTouchNear(touch2))
            {
                Vector2 delta = touch1.deltaPosition - touch2.deltaPosition;
                if (Mathf.Abs(delta.x) < Mathf.Abs(maxDelta.x) && Mathf.Abs(delta.y) < Mathf.Abs(maxDelta.y) && delta != Vector2.zero)
                {
                    DestroySelf();
                }
            }
        }
    }
}
