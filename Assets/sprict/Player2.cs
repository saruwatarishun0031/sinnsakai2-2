using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField, Tooltip("��̃C���[�W")]
    Image[] point1;
    [SerializeField, Tooltip("�e�I�u�W�F�N�g")]
    GameObject pointParent;
    [SerializeField, Tooltip("�e")]
    GameObject _bullet;
    [SerializeField, Tooltip("�Q�b�g���[�^�[")]
    Slider _pointSlider;
    [SerializeField, Tooltip("Player1�X�s�[�h")]
    int XYspeed;
    [SerializeField, Tooltip("Player�̐���")]
    float rayDistance;
    [SerializeField, Tooltip("����")]
    public bool _Death;
    [SerializeField, Tooltip("�e��")]
    Transform muzzle;
    public float _getTime;
    public float _MaxGetTime;
    public float speed = 1000;
    public float _interval = 3;
    public int NumberOfBullets2;
    const int winNum = 5;
    public int p;
    public bool p3;
    public bool p4;
    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
    public static Player2 Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //�V���O���g���i�����܂Łj

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        point1 = pointParent.GetComponentsInChildren<Image>();
        p = 0;
        _getTime = 0;
        NumberOfBullets2 = 6;
        _Death = false;

    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attke();
        Point();
        _interval -= Time.deltaTime;
    }

    void Move()
    {

        float mousex = Input.GetAxisRaw("Axis 44");
        transform.RotateAround(transform.position, transform.up, Input.GetAxis("Axis 44"));

        float x = Input.GetAxis("Axis 11");
        float z = Input.GetAxis("Axis 33");
        Debug.Log(z);

        var direction = transform.forward;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.red);
        Vector3 directionn = _rb.transform.forward * z + _rb.transform.right * x;
        directionn *= XYspeed * Time.deltaTime;
        _rb.AddForce(directionn, ForceMode.Impulse);
    }

    void Attke()
    {
        if (Input.GetButtonDown("shooting 2") && NumberOfBullets2 >= 1 && _interval <= 0)
        {

            // �e�ۂ̕���
            GameObject bullets = Instantiate(_bullet) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * speed;

            // Rigidbody�ɗ͂������Ĕ���
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // �e�ۂ̈ʒu�𒲐�
            bullets.transform.position = muzzle.position;
            NumberOfBullets2 -= 1;
            _interval = 2;
        }
        else if (Input.GetButtonDown("reload2"))
        {
            NumberOfBullets2 = 6;
            _interval = 4;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Get2") && other.gameObject.tag == "Point")
        {
            _pointSlider.gameObject.SetActive(true);
            _getTime += Time.deltaTime;
            _pointSlider.value = _getTime / _MaxGetTime;
            if (_getTime > 5)
            {
                point1[p].color = new Color(0, 255, 237, 255);
                p++;
                Destroy(other.gameObject);
                reset();
            }

            if (p >= winNum)
            {
                GameManager.Instance.Winner();//�V���O���g���i�Ăяo���p�j
            }
        }
    }
    void Point()
    {
        if (p == 3)
        {
            p3 = true;
        }
        else if (p == 4)
        {
            p4 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            reset();
        }
    }
    void reset()
    {
        _getTime = 0;
        _pointSlider.value = (float)_getTime / (float)_MaxGetTime;
        _pointSlider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Death();
        }
    }

    public void Death()
    {
        gameObject.GetComponent<Player2>().enabled = false;//�����ė~�����Ȃ�
        Destroy(gameObject, 1.7f);
        _Death = true;
    }
}
