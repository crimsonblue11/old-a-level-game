using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Text WinTxt;
    private Slider staminaBar;

    [SerializeField] private float Speed = 14f;

    private bool hasStamina = true;
    private bool isRunning = false;
    private bool isGameOver = false;

    private GameObject BlockList;

    void Start()
    {
        BlockList = GameObject.Find("Blocks");

        Transform canvas = GameObject.Find("Canvas").transform;
        WinTxt = canvas.Find("WinTxt").GetComponent<Text>();
        staminaBar = canvas.Find("StaminaBar").GetComponent<Slider>();

        controller = GetComponent<CharacterController>();

        WinTxt.enabled = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKey("space"))
            {
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            return;
        }

        if (BlockList.transform.childCount == 0)
        {
            isGameOver = true;
            WinTxt.enabled = true;
            return;
        }


        if (transform.position.z > 100)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 100f);
            return;
        }
        else if (transform.position.z < -100)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -100f);
            return;
        }
        else if (transform.position.x > 100)
        {
            transform.position = new Vector3(100f, transform.position.y, transform.position.z);
            return;
        }
        else if (transform.position.x < -100)
        {
            transform.position = new Vector3(-100f, transform.position.y, transform.position.z);
            return;
        }

        if (staminaBar.value <= 0)
        {
            hasStamina = false;
        }
        else if (!hasStamina && staminaBar.value == staminaBar.maxValue)
        {
            hasStamina = true;
        }


        if (Input.GetKeyDown("left shift") && hasStamina)
        {
            Speed += 25f;
            isRunning = true;
        }
        else if (Input.GetKeyUp("left shift") || !hasStamina)
        {
            Speed = 14f;
            isRunning = false;
        }

        if (isRunning)
        {
            staminaBar.value -= Time.deltaTime;
        }
        else
        {
            if (hasStamina)
            {
                staminaBar.value += Time.deltaTime * 2;
            }
            else
            {
                staminaBar.value += Time.deltaTime;
            }
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Speed * Time.deltaTime);
    }
}
