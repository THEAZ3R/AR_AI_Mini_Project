using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public UDPReceiver receiver;

    public GameObject catPrefab;
    public GameObject dogPrefab;

    GameObject currentAnimal;

    void Update()
    {
        string msg = receiver.GetLatestMessage();
        if (string.IsNullOrEmpty(msg)) return;

        AnimalData data = JsonUtility.FromJson<AnimalData>(msg);

        Vector3 screenPos = new Vector3(
            data.x * Screen.width,
            (1 - data.y) * Screen.height,
            2f
        );

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if (currentAnimal == null || currentAnimal.name != data.animal)
        {
            if (currentAnimal != null)
                Destroy(currentAnimal);

            GameObject prefab = data.animal == "cat" ? catPrefab : dogPrefab;

            currentAnimal = Instantiate(
                prefab,
                worldPos,
                Quaternion.identity  // Start with no rotation
            );

            currentAnimal.name = data.animal;
        }

        currentAnimal.transform.position = worldPos;

        // Always face the camera properly
        currentAnimal.transform.LookAt(Camera.main.transform);
        currentAnimal.transform.Rotate(0, 180, 0);  // Face towards user
    }
}