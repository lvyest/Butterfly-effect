using UnityEngine; 
using UnityEngine.UI; 
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage; // ���̵� ȿ�� ���� Image ������Ʈ ����
    public float fadeSpeed = 1f; // ���̵� �ӵ� ����
                                 
                                 
    private void Start() 
    {
        StartCoroutine(FadeIn()); //FadeIn �ڷ�ƾ ����
    }

    public IEnumerator FadeIn() // ���̵��� �ڷ�ƾ �޼��� 
    {
        fadeImage.gameObject.SetActive(true); // ���̵� �̹��� ���� ������Ʈ Ȱ��ȭ
        fadeImage.color = Color.black; // ���̵� �̹����� ���� ������ �������ϰ�

        while (fadeImage.color.a > 0) // �̹����� ���� ���� 0���� Ŭ ������ �ݺ�
        {
            fadeImage.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime); // ���� �� ���ҽ��� ���� �����ϰ�
            yield return null; // ���� �����ӱ��� ���
        }

        fadeImage.gameObject.SetActive(false); // ���̵� �̹��� ���� ������Ʈ ��Ȱ��ȭ
    }

    public IEnumerator FadeOut(string sceneName) // ���̵� �ƿ�(���� ����������), ���ο� �� �ε��ϴ� ��ƾ �޼���
    {
        fadeImage.gameObject.SetActive(true); // ���̵� �̹��� ���� ������Ʈ Ȱ��ȭ
        fadeImage.color = new Color(0, 0, 0, 0); // ���̵� �̹����� ������ ������ ���������� ����.

        while (fadeImage.color.a < 1) // �̹����� ���� �� 1���� ���� ������ �ݺ�
        {
            fadeImage.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime); // ���� ���� �������� ���� �������ϰ� 
            yield return null; // ���� �����ӱ��� ���
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); // �־��� �� �̸��� ����� ���ο� ���� �ε�
    }
}
