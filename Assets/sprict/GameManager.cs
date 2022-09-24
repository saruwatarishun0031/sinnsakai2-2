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
    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;
    Player Player1;
    Player2 Player2;
    Player3 Player3;
    Player4 Player4;

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
        player1 = GameObject.Find("Player1");
        Player1 = player1.GetComponent<Player>();
        player2 = GameObject.Find("Player2");
        Player2 = player2.GetComponent<Player2>();
        player3 = GameObject.Find("Player3");
        Player3 = player3.GetComponent<Player3>();
        player4 = GameObject.Find("Player4");
        Player4 = player4.GetComponent<Player4>();
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
        Cursor.lockState = CursorLockMode.Locked;
    }
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

    void notice()
    {
        Observer observer1 = new Observer("Player1");
        Observer observer2 = new Observer("Player2");
        Observer observer3 = new Observer("Player3");
        Observer observer4 = new Observer("Player4");
        Observable observable = new Observable();
        IDisposable disposable1 = observable.Subscribe(observer1);
        if (player1.GetComponent<Player>().p == 3)
        {
            observable.SendNotice();
            _Text.enabled = true;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable2 = observable.Subscribe(observer2);
        if (player2.GetComponent<Player2>().p == 3)
        {
            observable.SendNotice();
            _Text.enabled = true;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable3 = observable.Subscribe(observer3);
        if (player3.GetComponent<Player3>().p == 3)
        {
            observable.SendNotice();
            _Text.enabled = true;
            StartCoroutine(Tuuti());
        }
        IDisposable disposable4 = observable.Subscribe(observer4);
        if (player4.GetComponent<Player4>().p == 3)
        {
            observable.SendNotice();
            _Text.enabled = true;
            StartCoroutine(Tuuti());
        }

    }
    void Main()
    {
        int PlayerCount = PlayerList.Count;
        Debug.Log(PlayerCount);
        if (player1.GetComponent<Player>()._Death == true)
        {
            PlayerList.Remove("Player1");
        }
        else if (player2.GetComponent<Player2>()._Death == true)
        {
            PlayerList.Remove("Player2");
        }
        else if (player3.GetComponent<Player3>()._Death == true)
        {
            PlayerList.Remove("Player3");
        }
        else if (player4.GetComponent<Player4>()._Death == true)
        {
            PlayerList.Remove("Player4");
        }

        if (PlayerCount ==1)
        {
            if (player1.GetComponent<Player>()._Death == false)
            {
                _WinPlayer.text = "Player1";
            }
            else if (player2.GetComponent<Player2>()._Death == false)
            {
                _WinPlayer.text = "Player2";
            }
            else if (player3.GetComponent<Player3>()._Death == false)
            {
                _WinPlayer.text = "Player3";
            }
            else if (player4.GetComponent<Player4>()._Death == false)
            {
                _WinPlayer.text = "Player4";
            }
            Winner();
        }
    }
    private IEnumerator Tuuti()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeOut);
            _Text.enabled = false;
        }
    }

    public void Winner()

    {
        Debug.Log("����");
        _Win.enabled = true;
    }
}
