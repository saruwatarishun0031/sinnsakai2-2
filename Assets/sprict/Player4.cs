using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player4
    : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField, Tooltip("空のイメージ")]
    Image[] point1;
    [SerializeField, Tooltip("親オブジェクト")]
    GameObject pointParent;
    [SerializeField, Tooltip("弾")]
    GameObject _bullet;
    [SerializeField, Tooltip("ゲットメーター")]
    Slider _pointSlider;
    [SerializeField, Tooltip("Player1スピード")]
    int XYspeed;
    [SerializeField, Tooltip("Playerの正面")]
    float rayDistance;
    [SerializeField, Tooltip("負け")]
    public bool _Death;
    [SerializeField, Tooltip("銃口")]
    Transform muzzle;
    [SerializeField, Tooltip("敗北UI")]
    Canvas _over;
    [SerializeField, Tooltip("弾数")]
    Text _text;
    public float _getTime;
    public float _MaxGetTime;
    public float speed = 1000;
    public float _interval = 3;
    public int NumberOfBullets4;
    const int winNum = 5;
    public int p;
    int i;
    public bool p3;
    public bool p4;
    //シングルトンパターン（簡易型、呼び出される）
    public static Player4 Instance;

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
    //シングルトン（ここまで）

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        point1 = pointParent.GetComponentsInChildren<Image>();
        p = 0;
        _getTime = 0;
        NumberOfBullets4 = 6;
        _Death = false;
        i = 6;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attke();
        Point();
        _interval -= Time.deltaTime;
        _text.text = i + "/6";
    }

    void Move()
    {

        float mousex = Input.GetAxisRaw("Axis 4444");
        transform.RotateAround(transform.position, transform.up, Input.GetAxis("Axis 4444"));

        float x = Input.GetAxis("Axis 1111");
        float z = Input.GetAxis("Axis 3333");
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
        if (Input.GetButtonDown("shooting 4") && NumberOfBullets4 >= 1 && _interval <= 0)
        {

            // 弾丸の複製
            GameObject bullets = Instantiate(_bullet);

            Vector3 force;

            force = this.gameObject.transform.forward * speed;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整
            bullets.transform.position = muzzle.position;
            NumberOfBullets4 -= 1;
            _interval = 2;
            i -= 1;
        }
        else if (Input.GetButtonDown("reload4"))
        {
            NumberOfBullets4 = 6;
            _interval = 4;
            i = 6;
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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Get4") && other.gameObject.tag == "Point")
        {
            _pointSlider.gameObject.SetActive(true);
            _getTime += Time.deltaTime;
            _pointSlider.value = (float)_getTime / (float)_MaxGetTime;
            if (_getTime > 5)
            {
                point1[p].color = new Color(0, 255, 237, 255);
                p++;
                Destroy(other.gameObject);
                reset();
            }

            if (p >= winNum)
            {
                GameManager.Instance.Winner();//シングルトン（呼び出し用）
            }
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
        gameObject.GetComponent<Player4>().enabled = false;//動いて欲しくない
        Destroy(gameObject, 1.7f);
        _over.gameObject.SetActive(true);
        _Death = true;
    }
}
