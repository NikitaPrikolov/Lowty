using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //����������, �������� �������� �������� �����:
    public float movementSpeed;

    //����������, �������� �������� �������� ����� � ������:
    public float rigtLeftSpeed;

    //���������� ����������, �������� ���������� ������������ ������:
    bool isHit;

    //����������, �������� ������� ������ ������������:
    public UnityEngine.Object hit;

    //����������, �������� ������ ������:
    public GameObject player;

    //����������, �������� ������ �������:
    public GameObject flash;

    //����������, �������� ������ �������:
    public GameObject darkness;

    //����������, �������� ����� ���������� UI ����������� �����:
    public Text scoreText;

    //����������, �������� ��������� UI ����������� �����:
    public GameObject score;

    //����������, ��� - �� �����:
    public float scoreAmount;

    //����������, �������� ��� - �� ����� ����������� �� ������� �������:
    public float pointIncreasedPerSecond;

    //����������, �������� ������ ����:
    public GameObject pauseMenu;

    //����������, �������� ���� ������������:
    public AudioSource crashSound;

    //����������, �������� ������� ����:
    public AudioSource backSound;

    //����������, �������� ������ ������:
    public AudioClip[] clips;

    //����������, �������� ���� ������:
    public AudioSource buttonSound;

    void Start()
    {

        //������������� ���������� ������������:
        isHit = false;
        //������ �������� ����������� �������:
        darkness.GetComponent<Animator>().Play("Darknes");
        //�������� ����:
        scoreAmount = 0f;

        backSound.clip = GetRandomClip();
        backSound.Play();

    }

    void Update()
    {
        //���� ���������� ������������ �������������, �� ����� �������� �� ��� �, �� ��������� movementSpeed * Time.deltaTime:
        if (isHit == false)
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);

            //��������� �� ��������� rigtLeftSpeed, �� ��� z, ��� ������� ������� A:
            if (Input.GetKey(KeyCode.A))
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -rigtLeftSpeed));

            //��������� �� ��������� rigtLeftSpeed, �� ��� z, ��� ������� ������� D:
            if (Input.GetKey(KeyCode.D))
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, rigtLeftSpeed));
        }

        //���� ���������� ������������ �������������,���������� ��������� ������ �������� ��� movementSpeed � rigtLeftSpeed, ����� �������� �������� �������� �������� �� ���� x � z:
        else
        {
            movementSpeed = 0;
            rigtLeftSpeed = 0;
        }

        //��������� ���������� �������� ����� �������� ����������� ������:
        scoreText.text = (int)scoreAmount + "";
        //������� �����:
        scoreAmount += pointIncreasedPerSecond * Time.deltaTime;

    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        //���� ��� ���������� == Wall:
        if (other.tag == "Wall")
        {
            if (scoreAmount > PlayerPrefs.GetFloat("highscore"))
            {
                PlayerPrefs.SetFloat("highscore", scoreAmount);
            }
            
            //�������� ������� hitRef ��� �������� ������� �������:
            GameObject hitRef = (GameObject)Instantiate(hit);
            //��������� ������� ������� ������ ������� ������ � ����� x � y ������������ �� 1:
            hitRef.transform.position = new Vector3(transform.position.x +1, transform.position.y +1, transform.position.z);
            //���������� ������������ �������������:
            isHit = true;
            //������ ����� ������������:
            crashSound.Play();
            //�������� ������� ����:
            backSound.pitch -= Time.deltaTime * 9;
            //���������� ������ � �����:
            player.SetActive(false);
            score.SetActive(false);
            //����� �������� �������:
            flash.GetComponent<Animator>().Play("Flash");
            //������ ������ Restart ����� 1,5 �������:
            Invoke("Restart", 1.5f);
        }
    }

    //����� ����������� �����:
    public void Restart()
    {
        buttonSound.Play();
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    //����� ����� ����:
    public void Pause()
    {
        buttonSound.Play();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    //����� ������������� ����, ����� �����:
    public void Resume()
    {
        buttonSound.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //����� �������� �� ����� �������� ����:
    public void Home(int sceneID)
    {
        buttonSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

}
