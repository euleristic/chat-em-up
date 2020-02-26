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

    float counter;
    private bool waitingToHide;
    private Animator _animator;

    [SerializeField] private TextMeshProUGUI _senderTextBox, _messageTextBox;

    //[SerializeField] private string _sender;
    //[SerializeField] private string _message;

    public bool visible;


    private void Start()
    {
        SetVisible(false);
        _animator = GetComponent<Animator>();
        _messageOut.Invoke();
        counter = 0f;
        waitingToHide = false;
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

    private void Update()
    {
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
