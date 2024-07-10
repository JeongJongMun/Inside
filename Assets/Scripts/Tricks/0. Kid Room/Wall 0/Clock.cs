using UnityEngine;
using UnityEngine.UI;
/* Clock.cs
 * 0. Kid Room - Wall 0
 */
public class Clock : Observer
{
#region Private Variables
    private float angle;
    private Transform clockHand;
#endregion

#region Public Variables
#endregion

#region Private Methods
#endregion

#region Public Methods
    public override void OnNotify(Define.TrickName _trickName)
    {
        if (_trickName != this.trickName) {
            return;
        }
        // SoundManager.instance.SFXPlay("clock");
        angle -= 30f;
        clockHand.localEulerAngles = new Vector3(0f, 0f, angle);
        if (angle == -330f) {
            IsComplete = true;
        }

        if (!IsComplete) {
            return;
        }
        angle = -360f;
        clockHand.localEulerAngles = new Vector3(0f, 0f, angle);

        GetComponent<Image>().raycastTarget = false;

        gameObject.transform.position += Vector3.down * 70;
        gameObject.transform.Rotate(0, 0, 30);
    }
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        angle = -90f;
        clockHand = transform.GetChild(0);
    }
#endregion
}