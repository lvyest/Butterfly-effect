using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using System.Collections; 

public class GaugeControl : MonoBehaviour 
{
    public Slider gaugeSlider; // UI �����̴�
    public GameObject[] flames; // �� ������Ʈ �迭
    public float increaseRate = 10f; // ������ ���� �ӵ�
    public float decreaseAmount = 1f; // �����̽��� ���� �� ���ҷ�
    public Animator butterflyAnimator; // ������ �ִϸ�����
    public float flameShakeAmount = 0.1f; // ���� �¿� ��û�Ÿ� ��
    public float flameShakeSpeed = 5f; // ���� ��û�Ÿ� �ӵ�
    public GameObject gameOverImage; // ���� ���� �̹��� ������Ʈ

    private float gaugeValue = 500f; // ������ �ʱⰪ
    private Vector3[] flameOriginalPositions; // ���� ���� ��ġ �迭
    private SpriteRenderer[] flameRenderers; // ���� ��������Ʈ ������ �迭
    public float fadeDuration = 2f; // ���̵� �ƿ� ���� �ð�
    private bool isGameOver = false; // ���� ���� ���� �÷���

    void Start() 
    {
        gaugeSlider.value = gaugeValue; // �ʱ� ������ �� ����

        flameOriginalPositions = new Vector3[flames.Length]; // �� ������Ʈ�� ���� ��ġ �迭 �ʱ�ȭ
        flameRenderers = new SpriteRenderer[flames.Length]; // �� ������Ʈ�� ��������Ʈ ������ �迭 �ʱ�ȭ

        for (int i = 0; i < flames.Length; i++) // �� ������Ʈ ������ ���� �ݺ�
        {
            flameOriginalPositions[i] = flames[i].transform.localPosition; // �� ���� ���� ��ġ ����
            flameRenderers[i] = flames[i].GetComponent<SpriteRenderer>(); // �� ���� ��������Ʈ ������ ����
        }

        gameOverImage.SetActive(false); // ���� ���� �̹��� ��Ȱ��ȭ
    }

    void Update() { 
        if (isGameOver) // ���� ���� �������� Ȯ��
        {
            if (Input.GetMouseButtonDown(0)) // ���콺 ��ư Ŭ�� Ȯ��
            {
                LoadNextStage(); // ���� �������� �ε�
            }
            return; // ���� ���� ���¿����� �� �̻� ������Ʈ���� ����
        }

        if (gaugeValue < 500f) // �������� �ִ밪���� ���� ���
        {
            gaugeValue += increaseRate * Time.deltaTime; // ������ ����
            gaugeSlider.value = gaugeValue; // �����̴� ������Ʈ
        }

        if (gaugeValue <= 5f && !isGameOver) // �������� 5 �����̰� ���� ���� ���°� �ƴ� ���
        {
            StartCoroutine(FadeOutFlames()); // �� ������Ʈ ���̵� �ƿ� �ڷ�ƾ ����
        }

        ShakeFlames(); // �� ��û�Ÿ� ȿ�� ����

        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� �Է� Ȯ��
        {
            OnSpacebarPress(); // �����̽��� ���� ó��
        }
    }

    void OnSpacebarPress() // �����̽��� ���� ó�� �޼���
    {
        gaugeValue -= decreaseAmount; // ������ ����

        if (gaugeValue < 0f) // ������ ���� 0 ���ϰ� ���� �ʵ��� ����
        {
            gaugeValue = 0f; // �ּҰ����� ����
        }

        gaugeSlider.value = gaugeValue; // �����̴� ������Ʈ

        butterflyAnimator.SetTrigger("flap"); // ���� ������ �ִϸ��̼� Ʈ����
    }

    void ShakeFlames() // �� ������Ʈ ��û�Ÿ� ó�� �޼���
    {
        for (int i = 0; i < flames.Length; i++) // �� �� ������Ʈ�� ���� �ݺ�
        {
            float shakeOffset = Mathf.Sin(Time.time * flameShakeSpeed) * flameShakeAmount; // �¿�� ��鸮�� �� ������ ���
            flames[i].transform.localPosition = new Vector3(flameOriginalPositions[i].x + shakeOffset, flameOriginalPositions[i].y, flameOriginalPositions[i].z); // ���� ���ο� ��ġ ����
        }
    }

    IEnumerator FadeOutFlames() // �� ������Ʈ�� ���̵� �ƿ��ϴ� �ڷ�ƾ �޼���
    {
        float elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
        Color[] originalColors = new Color[flameRenderers.Length]; // �� ���� ���� ���� �迭 �ʱ�ȭ

        for (int i = 0; i < flameRenderers.Length; i++) // �� �� ������Ʈ�� ���� �ݺ�
        {
            originalColors[i] = flameRenderers[i].color; // �� ���� ���� ���� ����
        }

        while (elapsedTime < fadeDuration) // ���̵� �ƿ� ���� �ð� ���� �ݺ�
        {
            elapsedTime += Time.deltaTime; // ��� �ð� ����
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // ���� �� ���

            for (int i = 0; i < flameRenderers.Length; i++) // �� �� ������Ʈ�� ���� �ݺ�
            {
                flameRenderers[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, alpha); // �� ���� ���� ����
            }

            yield return null; // ���� �����ӱ��� ���
        }

        foreach (GameObject flame in flames) // ��� �� ������Ʈ�� ���� �ݺ�
        {
            flame.SetActive(false); // �� ������Ʈ ��Ȱ��ȭ
        }

        GameOver(); // ���� ���� ó�� ȣ��
    }

    void GameOver() // ���� ���� ó�� �޼���
    {
        isGameOver = true; // ���� ���� ���� ����
        gameOverImage.SetActive(true); // ���� ���� �̹��� Ȱ��ȭ
    }

    void LoadNextStage() // ���� ���������� �� ��ȯ�ϴ� �޼���
    {
        SceneManager.LoadScene("Stage2"); // "Stage2" �� �ε�
    }
}
