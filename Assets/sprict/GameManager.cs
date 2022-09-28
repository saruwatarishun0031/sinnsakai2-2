using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<string> PlayerList = new List<string>();
    [SerializeField, Tooltip("�X�^�[�g")]
    Image start;
    [SerializeField, Tooltip("�J�E���g�e�L�X�g")]
    public Text startText;
    [SerializeField, Tooltip("�ʒm�e�L�X�g")]
    public Text _Text;
    [SerializeField, Tooltip("���������l")]
    Text _WinPlayer;
    [SerializeField, Tooltip("����UI")]
    Canvas _Win;
    public float s ;
    public int timeOut;

    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
    public static GameManager Instance;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //�V���O���g���i�����܂Łj
    // Start is called before the first frame update

    void Start()
    {
        startText = startText.GetComponent<Text>();
        _WinPlayer = _WinPlayer.GetComponent<Text>();
        PlayerList.Add("Player1");
        PlayerList.Add("Player2");
        PlayerList.Add("Player3");
        PlayerList.Add("Player4");
    }

    // Update is called once per frame
    void Update()
    {
        StartCount();
        notice();
        Main();
    }
    /// <summary>
    /// 
    /// </summary>
    public void StartCount()
    { 
        startText.text = s.ToString("0");
        if (s <= 3)
        {
            s -= Time.deltaTime;
        }
        if (s < 0.6)
        {
            start.enabled = false;
            startText.text = "�X�^�[�g!";
        }
        if (s < 0.2)
        {
            startText.enabled = false;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    void notice()
    {
        Observer observer1 = new Observer("Player1");
        Observer observer2 = new Observer("Player2");
        Observer observer3 = new Observer("Player3");
        Observer observer4 = new Observer("Player4");
        Observable observable = new Observable();
        IDisposable disposable1 = observable.Subscribe(observer1);
        if (Player.Instance.p == 3 && Player.Instance.Notification == true)
        {
            observable.SendNotice();
            _Text.enabled = true;
            Player.Instance.Notification = false;
            StartCoroutine(Tuuti());
        }
        else if (Player.Instance.p == 4 && Player.Instance.Notification2 == true)
        {
            observable.OnCheck();
            _Text.enabled = true;
            Player.Instance.Notification2 = false;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable2 = observable.Subscribe(observer2);
        if (Player2.Instance.p == 3 && Player2.Instance.Notification == true)
        {
            observable.SendNotice();
            _Text.enabled = true;
            Player2.Instance.Notification = false;
            StartCoroutine(Tuuti());
        }
        else if (Player2.Instance.p == 4 && Player2.Instance.Notification2 == true)
        {
            observable.OnCheck();
            _Text.enabled = true;
            Player2.Instance.Notification2 = false;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable3 = observable.Subscribe(observer3);
        if (Player3.Instance.p == 3 && Player3.Instance.Notification == true)
        {
            observable.SendNotice();
            _Text.enabled = true;
            Player3.Instance.Notification = false;
            StartCoroutine(Tuuti());
        }
        else if (Player3.Instance.p == 4 && Player3.Instance.Notification2 == true)
        {
            observable.OnCheck();
            _Text.enabled = true;
            Player3.Instance.Notification2 = false;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable4 = observable.Subscribe(observer4);
        if (Player4.Instance.p == 3 && Player4.Instance.Notification == true)
        {
            observable.SendNotice();
            _Text.enabled = true;
            Player4.Instance.Notification = false;
            StartCoroutine(Tuuti());
        }
        else if (Player4.Instance.p == 4 && Player4.Instance.Notification2 == true)
        {
            observable.OnCheck();
            _Text.enabled = true;
            Player4.Instance.Notification2 = false;
            StartCoroutine(Tuuti());
        }

    }

    /// <summary>
    /// 
    /// </summary>
    void Main()
    {
        int PlayerCount = PlayerList.Count;
        Debug.Log(PlayerCount);
        if (Player.Instance._Death == true)
        {
            PlayerList.Remove("Player1");
        }
        else if (Player2.Instance._Death == true)
        {
            PlayerList.Remove("Player2");
        }
        else if (Player3.Instance._Death == true)
        {
            PlayerList.Remove("Player3");
        }
        else if (Player4.Instance._Death == true)
        {
            PlayerList.Remove("Player4");
        }

        if (PlayerCount == 1)
        {
            if (Player.Instance._Death == false)
            {
                _WinPlayer.text = "����" + "Player1";
            }
            else if (Player2.Instance._Death == false)
            {
                _WinPlayer.text = "����" + "Player2";
            }
            else if (Player3.Instance._Death == false)
            {
                _WinPlayer.text = "����" + "Player3";
            }
            else if (Player4.Instance._Death == false)
            {
                _WinPlayer.text = "����" + "Player4";
            }
            Winner();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Tuuti()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeOut);
            _Text.enabled = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Winner()

    {
        Debug.Log("����");
        _Win.gameObject.SetActive(true);
        if (Player.Instance.p == 5)
        {
            _WinPlayer.text = "����"+"Player1";
        }
        else if (Player2.Instance.p == 5)
        {
            _WinPlayer.text = "����" + "Player2";
        }
        else if (Player3.Instance.p == 5)
        {
            _WinPlayer.text = "����" + "Player3";
        }
        else if (Player4.Instance.p == 5)
        {
            _WinPlayer.text = "����" + "Player4";
        }
    }
}
