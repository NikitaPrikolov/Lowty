using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Переменная, хранящая скорость движения вперёд:
    public float movementSpeed;

    //Переменная, хранящая скорость движения влево и вправо:
    public float rigtLeftSpeed;

    //Логическая переменная, хранящая показатели столкновения игрока:
    bool isHit;

    //Переменная, хранящая партикл эффект столкновения:
    public UnityEngine.Object hit;

    //Переменная, хранящая объект игрока:
    public GameObject player;

    //Переменная, хранящая спрайт вспышки:
    public GameObject flash;

    //Переменная, хранящая спрайт темноты:
    public GameObject darkness;

    //Переменная, хранящая текст компонента UI отображения очков:
    public Text scoreText;

    //Переменная, хранящая компонент UI отображения очков:
    public GameObject score;

    //Переменная, кол - во очков:
    public float scoreAmount;

    //Переменная, хранящая кол - во очков начисляемых за секунду времени:
    public float pointIncreasedPerSecond;

    //Переменная, хранящая панель меню:
    public GameObject pauseMenu;

    //Переменная, хранящая звук столкновения:
    public AudioSource crashSound;

    //Переменная, хранящая фоновый звук:
    public AudioSource backSound;

    //Переменная, хранящая массив звуков:
    public AudioClip[] clips;

    //Переменная, хранящая звук кнопки:
    public AudioSource buttonSound;

    void Start()
    {

        //Отрицательный показатель столкновения:
        isHit = false;
        //Запуск анимации рассеивания темноты:
        darkness.GetComponent<Animator>().Play("Darknes");
        //Обнулить очки:
        scoreAmount = 0f;

        backSound.clip = GetRandomClip();
        backSound.Play();

    }

    void Update()
    {
        //Если показатель столкновения отрицательный, то игрок движется по оси х, со скоростью movementSpeed * Time.deltaTime:
        if (isHit == false)
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);

            //Ускорение со скоростью rigtLeftSpeed, по оси z, при зажатой клавише A:
            if (Input.GetKey(KeyCode.A))
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -rigtLeftSpeed));

            //Ускорение со скоростью rigtLeftSpeed, по оси z, при зажатой клавише D:
            if (Input.GetKey(KeyCode.D))
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, rigtLeftSpeed));
        }

        //Если показатель столкновения положительный,происходит установка нового значения для movementSpeed и rigtLeftSpeed, новые значения обнуляют скорости движения по осям x и z:
        else
        {
            movementSpeed = 0;
            rigtLeftSpeed = 0;
        }

        //Присвоить переменную подсчета очков элементу отображения текста:
        scoreText.text = (int)scoreAmount + "";
        //Подсчет очков:
        scoreAmount += pointIncreasedPerSecond * Time.deltaTime;

    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        //Если тэг коллайдера == Wall:
        if (other.tag == "Wall")
        {
            if (scoreAmount > PlayerPrefs.GetFloat("highscore"))
            {
                PlayerPrefs.SetFloat("highscore", scoreAmount);
            }
            
            //Создание объекта hitRef для хранения эффекта вспышки:
            GameObject hitRef = (GameObject)Instantiate(hit);
            //Установка позиции вспышки равной позиции игрока с осями x и y увеличенными на 1:
            hitRef.transform.position = new Vector3(transform.position.x +1, transform.position.y +1, transform.position.z);
            //Показатель столкновения положительный:
            isHit = true;
            //Запуск звука столкновения:
            crashSound.Play();
            //Изменить фоновый звук:
            backSound.pitch -= Time.deltaTime * 9;
            //Отключение игрока и счета:
            player.SetActive(false);
            score.SetActive(false);
            //Запус анимации вспышки:
            flash.GetComponent<Animator>().Play("Flash");
            //Запуск метода Restart через 1,5 секунды:
            Invoke("Restart", 1.5f);
        }
    }

    //Метод перезапуска сцены:
    public void Restart()
    {
        buttonSound.Play();
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    //Метод паузы игры:
    public void Pause()
    {
        buttonSound.Play();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    //Метод возобновления игры, после паузы:
    public void Resume()
    {
        buttonSound.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //Метод перехода на сцену главного меню:
    public void Home(int sceneID)
    {
        buttonSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

}
