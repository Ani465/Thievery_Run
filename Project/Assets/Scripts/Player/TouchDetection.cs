using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    private Touch _touch;
    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    _startPos = _touch.position;
                    break;

                case TouchPhase.Ended:
                    _endPos = _touch.position;

                    var xDifference = _endPos.x - _startPos.x;
                    var yDifference = _endPos.y - _startPos.y;

                    if (Mathf.Abs(xDifference) > Mathf.Abs(yDifference))
                    {
                        switch (xDifference)
                        {
                            // Horizontal swipe
                            case > 0:
                                transform.Rotate(0, 90, 0); // Rotate right
                                break;
                            case < 0:
                                transform.Rotate(0, -90, 0); // Rotate left
                                break;
                        }
                    }
                    else
                    {
                        // Vertical swipe
                        if (yDifference < 0)
                        {
                            transform.Rotate(0, 180, 0); // Turn Around
                        }
                    }
                    break;
            }
        }
    }
}
