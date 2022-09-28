using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField,Tooltip("空のイメージ")] 
    Image[] point1;
    [SerializeField,Tooltip("親オブジェクト")] 
    GameObject pointParent;
    [SerializeField, Tooltip("弾")]
    GameObject _bullet;
    [SerializeField, Tooltip("ゲットメーター")]
    Slider _pointSlider;
    [SerializeField,Tooltip("Player1スピード")] 
    int Speed;
    [SerializeField,Tooltip("Playerの正面")] 
    float rayDistance;
    [SerializeField, Tooltip("負け")]
    public bool _Death;
    [SerializeField, Tooltip("銃口")]
    Transform muzzle;
    [SerializeField, Tooltip("敗北UI")]
    Canvas _over;
    [SerializeField, Tooltip("弾数")]
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
    //シングルトンパターン（簡易型、呼び出される）
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
    //シングルトン（ここまで）

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
    /// Player1の動き
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
    /// Player1の攻撃（プレハブを飛ばす）
    /// </summary>
    void Attke()
    {
        if (Input.GetButtonDown("shooting 1") && NumberOfBullets >= 1 && Interval <= 0)
        {

            // 弾丸の複製
            GameObject bullets = Instantiate(_bullet) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * BulletSpeed;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整
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
    /// 勝利条件２（ポイントを取るシステム）
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
                GameManager.Instance.Winner();//シングルトン（呼び出し用）
            }
        }
    }
    /// <summary>
    /// オーブから出たらリセット
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
    /// リセット
    /// </summary>
    void reset()
    {
        GetTime = 0;
        _pointSlider.value = (float)GetTime / (float)MaxGetTime;
        _pointSlider.gameObject.SetActive(false);
    }
    /// <summary>
    /// bulletに当たった時の処理
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
    /// 3ポイント以上持っている時に知らせる
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
    /// 負けるシステム
    /// </summary>
    public void Death()
    {
        gameObject.GetComponent<Player>().enabled = false;//動いて欲しくない
        Destroy(gameObject, 1.7f);
        _over.gameObject.SetActive(true);
        _Death = true;
    }
}
