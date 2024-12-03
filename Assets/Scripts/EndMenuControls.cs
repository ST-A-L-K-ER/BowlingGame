using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����� ��� ���������� ������� ��������� ����
public class EndMenuControls : MonoBehaviour
{
    public TextMeshProUGUI winnerText; // ��������� ������� ��� ����������� ���������� ����

    // �����, ���������� ������ ����
    void Update()
    {
        DisplayWinner(); // ��������� ����� � ����������� ����
    }

    // ����� ��� ��������� ������� ������ "������ �����"
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game"); // ��������� ����� � �����
    }

    // ����� ��� ��������� ������� ������ "� ����"
    public void MenuPressed()
    {
        SceneManager.LoadScene("Menu"); // ��������� ����� �������� ����
    }

    // ����� ��� ��������� ������� ������ "�����"
    public void ExitPressed()
    {
        Application.Quit(); // ��������� ����������
    }

    // ����� ��� ����������� ���������� ��� ��������� � ������
    private void DisplayWinner()
    {
        if (EndScore.playerScore > EndScore.computerScore) // ���� ���� ������ ����
        {
            winnerText.text = $"����� ������� �� ������: {EndScore.playerScore}:{EndScore.computerScore}";
        }
        else if (EndScore.playerScore < EndScore.computerScore) // ���� ���� ���������� ����
        {
            winnerText.text = $"��������� ������� �� ������: {EndScore.computerScore}:{EndScore.playerScore}";
        }
        else // ���� ���� ������
        {
            winnerText.text = $"�����! ����: {EndScore.computerScore}:{EndScore.playerScore}";
        }
    }
}