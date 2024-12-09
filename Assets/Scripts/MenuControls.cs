using System.IO;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    // Метод для обработки нажатия кнопки "Играть"
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game"); // Загружаем сцену с игрой
    }

    // Метод для обработки нажатия кнопки "Выход"
    public void ExitPressed()
    {
        Application.Quit(); // Завершаем приложение
    }
}