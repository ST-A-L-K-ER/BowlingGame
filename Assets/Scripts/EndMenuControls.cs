using System.IO;
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
        if (File.Exists(Application.dataPath + "/savegame.json"))
        {
            File.Delete(Application.dataPath + "/savegame.json");
            Saves data = new Saves
            {
                currentFrame = 1,
                currentRoll = 1,
                playerScore = 0,
                computerScore = 0,
                playerRolls = new int[21],
                computerRolls = new int[21],
                pinHasFallen = new bool[10],
                pinIsFallen = new bool[10],
                playerRollIndex = 0,
                computerRollIndex = 0
            };
        }

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