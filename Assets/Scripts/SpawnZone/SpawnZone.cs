
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject objectToSpawn; // ������ �������, ������� ����� �����������
    public float spawnDelay = 1f; // �������� ����� ��������� ��������
    public int maxSpawnCount = 10; // ������������ ���������� ����������� ��������
    private int currentSpawnCount = 0; // ������� ���������� ��������� ��������
    private float timeSinceLastSpawn = 0f; // �����, ��������� � ���������� �������� �������
    public Collider spawnArea;
    void Update()
    {
        // ���������, �� ��������� �� ������������ ���������� ����������� ��������
        if (currentSpawnCount >= maxSpawnCount)
            return;

        // ����������� �����, ��������� � ���������� �������� �������
        timeSinceLastSpawn += Time.deltaTime;

        // ���� ������ ������ ��������, ������� ����� ������
        if (timeSinceLastSpawn >= spawnDelay)
        {
            Vector3 spawnPosition = spawnArea.bounds.center + Random.insideUnitSphere * spawnArea.bounds.extents.magnitude;
            spawnPosition.y += 10;
            GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            currentSpawnCount++;
            timeSinceLastSpawn = 0f;
        }
    }
}
