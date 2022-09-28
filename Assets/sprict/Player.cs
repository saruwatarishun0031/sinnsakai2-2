using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField,Tooltip("��̃C���[�W")] 
    Image[] point1;
    [SerializeField,Tooltip("�e�I�u�W�F�N�g")] 
    GameObject pointParent;
    [SerializeField, Tooltip("�e")]
    GameObject _bullet;
    [SerializeField, Tooltip("�Q�b�g���[�^�[")]
    Slider _pointSlider;
    [SerializeField,Tooltip("Player1�X�s�[�h")] 
    int Speed;
    [SerializeField,Tooltip("Player�̐���")] 
    float rayDistance;
    [SerializeField, Tooltip("����")]
    public bool _Death;
    [SerializeField, Tooltip("�e��")]
    Transform muzzle;
    [SerializeField, Tooltip("�s�kUI")]
    Canvas _over;
    [SerializeField, Tooltip("�e��")]
    Text _text;
    public float GetTime;
    public float MaxGetTime;
    public float BulletSpeed = 1000;
    public float Interval = 3;
    public int NumberOfBullets;
    const int winNum = 5;
    public int p;
    int i;
    public bool p3;
    public bool Notification;
    public bool p4;
    public bool Notification2;
    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
    public static Player Instance;

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
        GetTime = 0;
        NumberOfBullets = 6;
        i = 6;
        _Death = false;
        p3 = false;
        Notification = true;
        p4 = false;
        Notification2 = true;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attke();
        Interval -= Time.deltaTime;
        _text.text = i + "/6";
        Tuuti();
    }
    /// <summary>
    /// Player1�̓���
    /// </summary>
    void Move()
    {

        float mousex = Input.GetAxisRaw("Axis 4");
        transform.RotateAround(transform.position, transform.up, Input.GetAxis("Axis 4"));

        float x = Input.GetAxis("Axis 1");
        float z = Input.GetAxis("Axis 3");
        Debug.Log(z);

        var direction = transform.forward;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.red);
        Vector3 directionn = _rb.transform.forward * z + _rb.transform.right * x;
        directionn *= Speed * Time.deltaTime;
        _rb.AddForce(directionn, ForceMode.Impulse);
    }
    /// <summary>
    /// Player1�̍U���i�v���n�u���΂��j
    /// </summary>
    void Attke()
    {
        if (Input.GetButtonDown("shooting 1") && NumberOfBullets >= 1 && Interval <= 0)
        {

            // �e�ۂ̕���
            GameObject bullets = Instantiate(_bullet) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * BulletSpeed;

            // Rigidbody�ɗ͂������Ĕ���
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // �e�ۂ̈ʒu�𒲐�
            bullets.transform.position = muzzle.position;
            NumberOfBullets -= 1;
            Interval = 2;
            i -= 1;
        }
        else if (Input.GetButtonDown("reload1"))
        {
            NumberOfBullets = 6;
            Interval = 4;
            i = 6;
        }
    }
    /// <summary>
    /// ���������Q�i�|�C���g�����V�X�e���j
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Get1") && other.gameObject.tag == "Point")
        {
            _pointSlider.gameObject.SetActive(true);
            GetTime += Time.deltaTime;
            _pointSlider.value = (float)GetTime / (float)MaxGetTime;
            if (GetTime > 5)
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
    /// <summary>
    /// �I�[�u����o���烊�Z�b�g
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            reset();
        }
    }
    /// <summary>
    /// ���Z�b�g
    /// </summary>
    void reset()
    {
        GetTime = 0;
        _pointSlider.value = (float)GetTime / (float)MaxGetTime;
        _pointSlider.gameObject.SetActive(false);
    }
    /// <summary>
    /// bullet�ɓ����������̏���
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Death();
        }
    }

    /// <summary>
    /// 3�|�C���g�ȏ㎝���Ă��鎞�ɒm�点��
    /// </summary>
    void Tuuti()
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
    /// <summary>
    /// ������V�X�e��
    /// </summary>
    public void Death()
    {
        gameObject.GetComponent<Player>().enabled = false;//�����ė~�����Ȃ�
        Destroy(gameObject, 1.7f);
        _over.gameObject.SetActive(true);
        _Death = true;
    }
}
