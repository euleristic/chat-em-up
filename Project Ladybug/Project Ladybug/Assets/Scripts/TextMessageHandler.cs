using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextMessageHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _messageIn;
    [SerializeField] private UnityEvent _messageOut;
    [SerializeField] private GameObject Yes;
    [SerializeField] private GameObject No;
    [SerializeField] private int YesMax, NoMax;
    private float scale;
    private int YesHP, NoHP;
    [SerializeField] private SpriteRenderer YesSprite, NoSprite;

    float counter;
    private bool waitingToHide;
    private Animator _animator;

    [SerializeField] private TextMeshProUGUI _senderTextBox, _messageTextBox;

    //[SerializeField] private string _sender;
    //[SerializeField] private string _message;

    public bool visible;


    private void Start()
    {
        scale = Yes.transform.localScale.x;
        SetVisible(false);
        _animator = GetComponent<Animator>();
        _messageOut.Invoke();
        counter = 0f;
        waitingToHide = false;
        YesHP = YesMax;
        NoHP = NoMax;
    }

    
    public void GetMessage(string sender, string message)
    {
        _senderTextBox.text = sender;
        _messageTextBox.text = message;

        _messageIn.Invoke();
    }

    public void SetVisible(bool value)
    {
        if (_animator == null) return;
        _animator.SetBool("Visible", value);
    }

    public void GetOnlyMessage(string sender, string message)
    {
        _senderTextBox.text = sender;
        _messageTextBox.text = message;
        Yes.SetActive(false);
        No.SetActive(false);
        _messageIn.Invoke();
    }

    public void HideMessage()
    {
        _messageOut.Invoke();
    }

    public void takeDamage(string tag)
    {
        if (tag == "Yes")
            YesHP--;
        else
            NoHP--;
        if (YesHP <= 0)
        {
            GameObject.FindObjectOfType<WaveSystem>().AnswerHit("Yes");
            YesHP = YesMax;
        }
        else if (NoHP <= 0)
        {
            GameObject.FindObjectOfType<WaveSystem>().AnswerHit("No");
            NoHP = NoMax;
        }
    }
    private void Update()
    {
        Yes.transform.localScale = new Vector3(scale / YesMax * YesHP, scale / YesMax * YesHP, 1f);
        No.transform.localScale = new Vector3(scale / NoMax * NoHP, scale / NoMax * NoHP, 1f);
        if (waitingToHide)
        {
            counter += Time.deltaTime;

            if (counter > 3f)
            {
                Yes.SetActive(true);
                No.SetActive(true);
                waitingToHide = false;
                counter = 0f;
            }
        }
    }
}
