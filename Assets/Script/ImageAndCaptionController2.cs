using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageAndCaptionController2 : MonoBehaviour
{
    public UnityEngine.UI.Image displayImage; // �̹��� UI ���
    public UnityEngine.UI.Text captionText;   // �ڸ� UI ���

    public Sprite[] images;    // ��ȯ�� �̹��� �迭
    public string[] captions;  // ��ȯ�� �ڸ� �迭
    public AudioClip spacebarSound; // �����̽��ٸ� ���� �� ����� ȿ����

    private int currentIndex = 0; // ���� ǥ�õ� �̹����� �ڸ��� �ε���
    private bool isAutoTransition = false; // �ڵ� ��ȯ ���¸� ��Ÿ���� �÷���

    private AudioSource audioSource; // ȿ���� ����� ���� AudioSource ������Ʈ

    void Start()
    {
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
            if (currentIndex == 3)
            {
                StartCoroutine(AutoTransition(0.3f, 20)); // 0.3�� �������� �ڵ� ��ȯ
            }
            else if (currentIndex == 23)
            {
                StartCoroutine(AutoTransition(0.3f, 28)); // 0.3�� �������� �ڵ� ��ȯ
            }
            else if (currentIndex == 35)
            {
                StartCoroutine(AutoTransition(0.3f, 40)); // 0.3�� �������� �ڵ� ��ȯ
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
            // �ؽ�Ʈ�� ��� ����
            captionText.text = captions[currentIndex];
        }
    }

    IEnumerator AutoTransition(float interval, float end)
    {
        isAutoTransition = true; // �ڵ� ��ȯ ���·� ����

        for (int i = currentIndex; i <= end; i++)
        {
            UpdateDisplay(); // ���� ���÷��� ������Ʈ
            yield return new WaitForSeconds(interval); // ������ ���� ���� ���
            currentIndex++; // ���� �ε����� �̵�
        }

        isAutoTransition = false; // �ڵ� ��ȯ ����
        UpdateDisplay(); // ������ �̹����� ������Ʈ
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
