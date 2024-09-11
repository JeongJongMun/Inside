using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Laptop : NewTrick
{
#region Private Variables
    private const string ANSWER_LOWERCASE = "idol";
    private const string ANSWER_UPPERCASE = "IDOL";
    private const string ANSWER_PASCALCASE = "Idol";
    private RectTransform[] eyesPos;
    private float timer = 0f;
    private const float EYES_MOVE_TIME = 3.0f;
    private const float EYES_MOVE_DISTANCE = 10.0f;
    private Vector2[] startVector;
    private Vector2[] destinationVector;
    private bool isEyesMoving = false;
#endregion

#region Public Variables
    public Button enterButton;
    public TMP_InputField passwordInputField;
    public GameObject kakaotalk;
    public GameObject bear;
    public GameObject[] eyes;
#endregion

#region Private Methods
    private void Update()
    {
        if (isEyesMoving) {
            timer += Time.deltaTime;
            if (timer <= EYES_MOVE_TIME) {
                eyesPos[0].anchoredPosition = Vector2.Lerp(startVector[0], destinationVector[0], timer / EYES_MOVE_TIME);
                eyesPos[1].anchoredPosition = Vector2.Lerp(startVector[1], destinationVector[1], timer / EYES_MOVE_TIME);
            }
            if (timer > EYES_MOVE_TIME + 1) {
                eyes[0].GetComponent<Image>().color = Color.white;
                eyes[1].GetComponent<Image>().color = Color.white;
            }
            if (timer > EYES_MOVE_TIME + 2) {
                isEyesMoving = false;
                kakaotalk.SetActive(true);
            }
        }
    }
#endregion

#region Public Methods
#endregion

#region Protected Methods
    protected override void Start()
    {
        base.Start();
        enterButton.onClick.AddListener(() => IsComplete = CheckComplete(null));

        startVector = new Vector2[2];
        destinationVector = new Vector2[2];
        eyesPos = new RectTransform[2];

        startVector[0] = eyes[0].GetComponent<RectTransform>().anchoredPosition;
        startVector[1] = eyes[1].GetComponent<RectTransform>().anchoredPosition;

        destinationVector[0] = startVector[0] + Vector2.left * EYES_MOVE_DISTANCE;
        destinationVector[1] = startVector[1] + Vector2.right * EYES_MOVE_DISTANCE;

        eyesPos[0] = eyes[0].GetComponent<RectTransform>();
        eyesPos[1] = eyes[1].GetComponent<RectTransform>();
    }
    protected override bool CheckComplete(NewItem _currentClickedItem)
    {
        if (passwordInputField.text != ANSWER_LOWERCASE && passwordInputField.text != ANSWER_UPPERCASE && passwordInputField.text != ANSWER_PASCALCASE) {
            Managers.Sound.Play("laptopFail");
            return false;
        }
        Managers.Sound.Play("laptopSuccess");
        return true;
    }
    protected override void OnComplete()
    {
        base.OnComplete();
        bear.SetActive(true);
        isEyesMoving = true;
    }
#endregion
}