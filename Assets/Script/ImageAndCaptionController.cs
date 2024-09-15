using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

public class ImageAndCaptionController : MonoBehaviour
{
    public UnityEngine.UI.Image displayImage; // �̹��� UI ���
    public UnityEngine.UI.Text captionText;   // �ڸ� UI ���
    public Button nextButton;  // ���� ��ư UI ���

    public Sprite[] images;    // ��ȯ�� �̹��� �迭
    public string[] captions;  // ��ȯ�� �ڸ� �迭
    public AudioClip spacebarSound; // �����̽��ٸ� ���� �� ����� ȿ����

    private int currentIndex = 0; // ���� ǥ�õ� �̹����� �ڸ��� �ε���
    private bool isAutoTransition = false; // �ڵ� ��ȯ ���¸� ��Ÿ���� �÷���
    private const int lastIndex = 24; // ������ �ε���
    private const int fadeTextUntilIndex = 5; // �ؽ�Ʈ ���̵� �� ȿ���� ������ �ִ� �ε���

    public float fadeDuration = 1f; // ���̵� �� ȿ���� ���� �ð�

    private AudioSource audioSource; // ȿ���� ����� ���� AudioSource ������Ʈ

    void Start()
    {
        nextButton.gameObject.SetActive(false); // ��ư�� ��Ȱ��ȭ
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ���� ������Ʈ�� �߰�
        UpdateDisplay(); // ó�� UI�� ������Ʈ
    }

    void Update()
    {
        // �ڵ� ��ȯ �߿��� �����̽��� �Է��� ����
        if (isAutoTransition) return;

        // �����̽��� �Է��� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ȿ���� ���
            PlaySound(spacebarSound);

            // ���� �ε����� ��ȯ
            currentIndex++;

            // �ڵ� ��ȯ ������ ���� �ڷ�ƾ ����
            if (currentIndex == 8)
            {
                StartCoroutine(AutoTransition(0.1f)); // 0.1�� �������� �ڵ� ��ȯ
            }
            else
            {
                UpdateDisplay(); // �̹����� �ڸ��� ������Ʈ
            }
        }
    }

    void UpdateDisplay()
    {
        // ���� �ε����� �´� �̹����� �ڸ��� ����
        if (currentIndex < images.Length)
        {
            displayImage.sprite = images[currentIndex]; // �̹��� ������Ʈ
        }

        if (currentIndex < captions.Length)
        {
            if (currentIndex <= fadeTextUntilIndex)
            {
                // �ε��� 5������ ���̵� �� ȿ�� ����
                captionText.text = captions[currentIndex];
                StartCoroutine(FadeInText()); // �ؽ�Ʈ ���̵� �� ȿ��
            }
            else
            {
                // �ε��� 5 ���Ĵ� ��� �ؽ�Ʈ ����
                captionText.text = captions[currentIndex];
                captionText.GetComponent<CanvasGroup>().alpha = 1; // ��� �ؽ�Ʈ ���̰� ����
            }
        }

        // ������ �ε����� �����ϸ� ��ư Ȱ��ȭ �� ���̵� ��
        if (currentIndex == lastIndex)
        {
            nextButton.gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
            StartCoroutine(FadeInButton()); // ��ư ���̵� �� ȿ��
        }
    }

    IEnumerator AutoTransition(float interval)
    {
        isAutoTransition = true; // �ڵ� ��ȯ ���·� ����

        for (int i = currentIndex; i <= 17; i++)
        {
            UpdateDisplay(); // ���� ���÷��� ������Ʈ
            yield return new WaitForSeconds(interval); // ������ ���� ���� ���
            currentIndex++; // ���� �ε����� �̵�
        }

        isAutoTransition = false; // �ڵ� ��ȯ ����
        UpdateDisplay(); // ������ �̹����� ������Ʈ
    }

    IEnumerator FadeInText()
    {
        // �ؽ�Ʈ�� CanvasGroup�� �������ų� �߰�
        CanvasGroup canvasGroup = captionText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = captionText.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // �ؽ�Ʈ�� �����ϰ� ����
        float elapsedTime = 0; // ��� �ð� �ʱ�ȭ

        // ������ ���� �ð� ���� ���� ���� �������� ���̵� �� ȿ�� ����
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // ���� �� ����
            yield return null; // ���� �����ӱ��� ���
        }
    }

    IEnumerator FadeInButton()
    {
        // ��ư�� CanvasGroup�� �������ų� �߰�
        CanvasGroup canvasGroup = nextButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = nextButton.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // ��ư�� �����ϰ� ����
        float elapsedTime = 0; // ��� �ð� �ʱ�ȭ

        // ������ ���� �ð� ���� ���� ���� �������� ���̵� �� ȿ�� ����
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // ���� �� ����
            yield return null; // ���� �����ӱ��� ���
        }
    }

    // ȿ������ ����ϴ� �޼���
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // ������ ȿ������ ���
        }
    }
}
