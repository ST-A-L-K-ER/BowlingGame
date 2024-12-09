using System.IO;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    // ����� ��� ��������� ������� ������ "������"
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game"); // ��������� ����� � �����
    }

    // ����� ��� ��������� ������� ������ "�����"
    public void ExitPressed()
    {
        Application.Quit(); // ��������� ����������
    }
}