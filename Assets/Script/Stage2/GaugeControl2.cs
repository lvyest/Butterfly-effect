using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using System.Collections; 

public class GaugeControl2 : MonoBehaviour
{
    public Slider gaugeSlider; // UI �����̴�
    public GameObject fanBlades; // ��ǳ�� �� ������Ʈ
    public float increaseRate = 10f; // ������ ���� �ӵ�
    public float decreaseAmount = 1f; // �����̽��� ���� �� ���ҷ�

    public Animator butterflyAnimator; // ������ �ִϸ�����
    public float initialFanBladeSpeed = 300f; // ��ǳ�� �� �ʱ� ȸ�� �ӵ�
    public float maxFanBladeSpeed = 600f; // ��ǳ�� �� �ִ� ȸ�� �ӵ�
    public float fanBladeSpeedIncreaseRate = 50f; // ��ǳ�� �� ȸ�� �ӵ� ������

    public GameObject[] obstacles; // ��ֹ� ������Ʈ��
    public float pushForce = 10f; // ��ֹ��� ������ ��

    public Slider spinningGauge; // ���ο� ������ �����̴�
    public float spinningGaugeIncreaseRate = 5f; // ���ο� ������ ���� �ӵ�

    public GameObject gameClearImage; // ���� Ŭ���� �̹��� ������Ʈ

    private float gaugeValue = 200f; // ������ �ʱⰪ
    private bool isSpinningGaugeActive = false; // spinningGauge Ȱ��ȭ ����
    private int currentObstacleIndex = 0; // ���� �о ��ֹ��� �ε���
    private bool isFanSpinning = false; // ��ǳ�� ȸ�� ����
    private float currentFanBladeSpeed; // ���� ��ǳ�� ȸ�� �ӵ�
    private bool isGameClear = false; // ���� Ŭ���� ���� �÷���

    void Start() { 
        gaugeSlider.value = gaugeValue; // �ʱ� ������ �� ����

        spinningGauge.gameObject.SetActive(false); // spinningGauge ��Ȱ��ȭ
        spinningGauge.maxValue = 20f; // spinningGauge �ִ밪 ����
        spinningGauge.value = 0f; // spinningGauge �ʱⰪ ����

        currentFanBladeSpeed = initialFanBladeSpeed; // ��ǳ�� ȸ�� �ӵ� �ʱ�ȭ

        gameClearImage.SetActive(false); // ���� Ŭ���� �̹��� ��Ȱ��ȭ
    }

    void Update() 
    {
        if (isGameClear) // ���� Ŭ���� ���� Ȯ��
        {
            if (Input.GetMouseButtonDown(0)) // ���콺 ��ư Ŭ�� Ȯ��
            {
                SceneManager.LoadScene("Stage3-1"); // "Stage3-1" �� �ε�
            }
            return; // ���� Ŭ���� ���¿����� �� �̻� ������Ʈ���� ����
        }

        fanBlades.transform.Rotate(Vector3.forward * currentFanBladeSpeed * Time.deltaTime); // ��ǳ�� �� ȸ��

        if (gaugeValue < 200f) // gaugeSlider�� 200���� ���� ���
        {
            gaugeValue += increaseRate * Time.deltaTime; // ������ ����
            gaugeSlider.value = gaugeValue; // �����̴� ������Ʈ
        }

        if (gaugeValue <= 20f) // gaugeSlider�� 20 ������ ���
        {
            if (!isSpinningGaugeActive) // spinningGauge�� Ȱ��ȭ���� ���� ���
            {
                ActivateSpinningGauge(); // spinningGauge Ȱ��ȭ
            }
            IncreaseSpinningGauge(); // spinningGauge ����
            RotateFanBlades(); // ��ǳ�� ȸ��
        }
        else // gaugeSlider�� 20 �̻��� ���
        {
            if (isSpinningGaugeActive) // spinningGauge�� Ȱ��ȭ�� ���
            {
                ResetSpinningGauge(); // spinningGauge �ʱ�ȭ �� ��Ȱ��ȭ
            }
            StopFanBlades(); // ��ǳ�� ����
        }

        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� �Է� Ȯ��
        {
            OnSpacebarPress(); // �����̽��� ���� ó��
        }

        if (spinningGauge.value >= 20f) // spinningGauge�� �ִ밪�� ������ ���
        {
            HandleSpinningGaugeFull(); // spinningGauge ���� á�� �� ó��
        }

        if (AllObstaclesAreGone()) // ��� ��ֹ��� ���ŵǾ����� Ȯ��
        {
            GameClear(); // ���� Ŭ���� ó��
        }
    }

    void OnSpacebarPress() // �����̽��� ���� ó�� �޼���
    {
        gaugeValue -= decreaseAmount; // ������ ����

        if (gaugeValue < 0f) // gaugeSlider ���� 0 ���Ϸ� �������� �ʵ��� ����
        {
            gaugeValue = 0f; // �ּҰ����� ����
        }

        gaugeSlider.value = gaugeValue; // �����̴� ������Ʈ

        butterflyAnimator.SetTrigger("flap"); // ���� ������ �ִϸ��̼� Ʈ����
    }

    void ActivateSpinningGauge() // spinningGauge Ȱ��ȭ �޼���
    {
        spinningGauge.gameObject.SetActive(true); // spinningGauge Ȱ��ȭ
        spinningGauge.value = 0f; // spinningGauge �ʱ�ȭ
        isSpinningGaugeActive = true; // spinningGauge Ȱ��ȭ ���� ����
        isFanSpinning = true; // ��ǳ�� ȸ�� ���� ����
        currentFanBladeSpeed = initialFanBladeSpeed; // ��ǳ�� ȸ�� �ӵ� �ʱ�ȭ
    }

    void IncreaseSpinningGauge() // spinningGauge ���� �޼���
    {
        spinningGauge.value += spinningGaugeIncreaseRate * Time.deltaTime; // spinningGauge ����
    }

    void ResetSpinningGauge() // spinningGauge �ʱ�ȭ �� ��Ȱ��ȭ �޼���
    {
        spinningGauge.value = 0f; // spinningGauge �ʱ�ȭ
        spinningGauge.gameObject.SetActive(false); // spinningGauge ��Ȱ��ȭ
        isSpinningGaugeActive = false; // spinningGauge Ȱ��ȭ ���� ����
    }

    void HandleSpinningGaugeFull() // spinningGauge�� ���� á�� �� ó�� �޼���
    {
        PushCurrentObstacle(); // ���� ��ֹ� �о��

        gaugeValue = 200f; // gaugeSlider �ʱ�ȭ
        gaugeSlider.value = gaugeValue; // �����̴� ������Ʈ

        ResetSpinningGauge(); // spinningGauge �ʱ�ȭ �� ��Ȱ��ȭ

        currentObstacleIndex++; // ���� ��ֹ��� �̵�
        if (currentObstacleIndex >= obstacles.Length) // ��� ��ֹ��� ó���� ���
        {
            currentObstacleIndex = 0; // �ε����� ó������ �ǵ���
        }
    }

    void RotateFanBlades() // ��ǳ�� ȸ�� �޼���
    {
        if (isFanSpinning) // ��ǳ�Ⱑ ȸ�� ������ Ȯ��
        {
            fanBlades.transform.Rotate(Vector3.forward * currentFanBladeSpeed * Time.deltaTime); // ��ǳ�� �� ȸ��

            currentFanBladeSpeed += fanBladeSpeedIncreaseRate * Time.deltaTime; // ȸ�� �ӵ� ����

            if (currentFanBladeSpeed > maxFanBladeSpeed) // �ִ� ȸ�� �ӵ� ����
            {
                currentFanBladeSpeed = maxFanBladeSpeed; // �ִ� �ӵ��� ����
            }
        }
    }

    void StopFanBlades() // ��ǳ�� ���� �޼���
    {
        if (isFanSpinning) // ��ǳ�Ⱑ ȸ�� ������ Ȯ��
        {
            isFanSpinning = false; // ��ǳ�� ȸ�� ���� ����
            currentFanBladeSpeed = initialFanBladeSpeed; // ��ǳ�� �ӵ� �ʱ�ȭ
        }
    }

    void PushCurrentObstacle() // ���� ��ֹ� �о�� �޼���
    {
        if (currentObstacleIndex < obstacles.Length) // ��ȿ�� ��ֹ� �ε������� Ȯ��
        {
            GameObject obstacle = obstacles[currentObstacleIndex]; // ���� ��ֹ� ����
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>(); // ��ֹ��� Rigidbody2D ����
            if (rb != null) // Rigidbody2D�� �ִ��� Ȯ��
            {
                Vector2 pushDirection = (obstacle.transform.position - fanBlades.transform.position).normalized; // �о ���� ���
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // ���� ���Ͽ� ��ֹ� �о��
            }
        }
    }

    bool AllObstaclesAreGone() // ��� ��ֹ��� ���ŵǾ����� Ȯ���ϴ� �޼���
    {
        foreach (GameObject obstacle in obstacles) // �� ��ֹ��� ���� �ݺ�
        {
            if (obstacle.activeInHierarchy && obstacle.transform.position.magnitude < 10f) // ��ֹ��� Ȱ�� �������� �� ����� �ָ� �ִ��� Ȯ��
            {
                return false; // ���� ���ŵ��� ���� ��ֹ��� ����
            }
        }
        return true; // ��� ��ֹ��� ���ŵ�
    }

    void GameClear() // ���� Ŭ���� ó�� �޼���
    {
        isGameClear = true; // ���� Ŭ���� ���� ����
        gameClearImage.SetActive(true); // ���� Ŭ���� �̹��� Ȱ��ȭ
        StopFanBlades(); // ��ǳ�� ȸ�� ����
    }
}
