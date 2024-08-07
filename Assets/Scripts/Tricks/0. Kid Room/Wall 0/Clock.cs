using UnityEngine;
using UnityEngine.UI;
/* Clock.cs
 * 0. Kid Room - Wall 0
 */
public class Clock : NewTrick
{
#region Private Variables
    private float currentAngle;
    private Transform clockHand;
    private const float INITIAL_ANGLE = -90f;
    private const float ANGLE_INCREMENT = -30f;
    private const float FINAL_ANGLE = -360f;
#endregion

#region Public Variables
#endregion

#region Private Methods
#endregion

#region Public Methods
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        currentAngle = INITIAL_ANGLE;
        clockHand = transform.GetChild(0);
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        GameManager.Instance.soundManager.Play($"Clock", SoundType.EFFECT);
        currentAngle += ANGLE_INCREMENT;
        clockHand.localEulerAngles = new Vector3(0f, 0f, currentAngle);
        if (currentAngle != FINAL_ANGLE) {
            return false;
        }
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();

        GetComponent<Image>().raycastTarget = false;
        gameObject.transform.position += Vector3.down * 70;
        gameObject.transform.Rotate(0, 0, 30);
    }
#endregion
}