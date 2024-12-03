using UnityEngine;

// Класс для управления состоянием кегли
public class Pin : MonoBehaviour
{
    public bool hasFallen = false; // Флаг, указывающий, что кегля уже упала и была учтена
    public bool IsFallen = false; // Флаг, указывающий, что кегля сбита игроком
    private Vector3 pinPosition; // Стартовая позиция кегли
    private Quaternion pinRotation; // Стартовый угол поворота кегли

    // Метод, вызываемый при старте
    void Start()
    {
        pinPosition = transform.position; // Сохраняем начальную позицию кегли
        pinRotation = transform.rotation; // Сохраняем начальный угол поворота кегли
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        var pinPhysics = GetComponent<Rigidbody>(); // Получаем компонент физики Rigidbody для кегли

        // Проверяем, изменилась ли позиция или угол поворота кегли
        if (!hasFallen && pinPhysics.rotation != pinRotation && pinPhysics.position != pinPosition)
        {
            hasFallen = true; // Отмечаем, что кегля упала
        }
    }

    // Метод для сброса состояния кегли в начальное положение
    public void ResetPinState()
    {
        hasFallen = false; // Сбрасываем флаг "кегля упала"
        IsFallen = false; // Сбрасываем флаг "кегля сбита игроком"

        var pinPhysics = GetComponent<Rigidbody>(); // Получаем компонент физики Rigidbody для кегли

        pinPhysics.velocity = Vector3.zero; // Обнуляем скорость движения
        pinPhysics.position = pinPosition; // Возвращаем кеглю в начальную позицию
        pinPhysics.rotation = pinRotation; // Возвращаем кеглю в начальный угол поворота
        pinPhysics.angularVelocity = Vector3.zero; // Обнуляем угловую скорость
    }
}