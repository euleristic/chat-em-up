using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextMessageHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip plingeling;
    [SerializeField] private UnityEvent _messageIn;
    [SerializeField] private UnityEvent _messageOut;
    [SerializeField] private GameObject Yes;
    [SerializeField] private GameObject No;
    [SerializeField] private int YesMax, NoMax;
    private float scale;
    private int YesHP, NoHP;
    [SerializeField] private SpriteRenderer YesSprite, NoSprite;
    [SerializeField] private Sprite YesWhole, YesHalfCrack, YesCrack, NoWhole, NoHalfCrack, NoCrack;

    float counter;
    private bool waitingToHide;
    private Animator _animator;

    [SerializeField] private TextMeshProUGUI _senderTextBox, _messageTextBox;

    //[SerializeField] private string _sender;
    //[SerializeField] private string _message;

    public bool visible;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(plingeling, 1f);
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
            Yes.SetActive(false);
            waitingToHide = true;
        }
        else if (NoHP <= 0)
        {
            GameObject.FindObjectOfType<WaveSystem>().AnswerHit("No");
            No.SetActive(false);
            waitingToHide = true;
        }
    }
    private void Update()
    {
        switch (YesHP)
        {
            case 1:
                YesSprite.sprite = YesCrack;
                break;
            case 2:
                YesSprite.sprite = YesHalfCrack;
                break;
            case 3:
                YesSprite.sprite = YesWhole;
                break;
            default:
                YesSprite.sprite = null;
                break;
        }
        switch (NoHP)
        {
            case 1:
                NoSprite.sprite = NoCrack;
                break;
            case 2:
                NoSprite.sprite = NoHalfCrack;
                break;
            case 3:
                NoSprite.sprite = NoWhole;
                break;
            default:
                NoSprite.sprite = null;
                break;
        }
        if (waitingToHide)
        {
            counter += Time.deltaTime;

            if (counter > 3f)
            {
                Yes.SetActive(true);
                No.SetActive(true);
                YesHP = YesMax;
                NoHP = NoMax;
                waitingToHide = false;
                counter = 0f;
            }
        }
    }
}
