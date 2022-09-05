using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    //���������� �������� ������� ������:
    public Transform player;

    //������ ��������:
    public Chunk[] chunkPrefabs;

    //���������� �������� ������ (���������) ���������:
    public Chunk firstChunk;

    //������, �������� ��������� ���������:
    private List<Chunk> spawnedChunks = new List<Chunk>();
    void Start()
    {
        //�������� � ������ ��������� �������� ������ ���������:
        spawnedChunks.Add(firstChunk);
    }

    void Update()
    {
        //���������� ������ SpawnChunk, ���� ������� ������ ������ ������� ����� ��������� �� 25:
        if (player.position.x < spawnedChunks[spawnedChunks.Count - 1].End.position.x +25)
        {
            SpawnChunk();
        }
    }

    //����� �������� ���������:
    private void SpawnChunk()
    {
        //����� ��������� ��������� �� ������� � ��������� � �������:
        Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        //���������� ��������� ��������� � ������ ���������:
        spawnedChunks.Add(newChunk);

        //���� ���������� ��������� ������� ������ ��� ����� 3, �� ������� ��������� ������:
        if (spawnedChunks.Count >=3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
}
