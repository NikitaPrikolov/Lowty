using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    //Переменная хранящая позицию игрока:
    public Transform player;

    //Массив платформ:
    public Chunk[] chunkPrefabs;

    //Переменная хранящая первую (начальную) платформу:
    public Chunk firstChunk;

    //Список, хранящий созданные платформы:
    private List<Chunk> spawnedChunks = new List<Chunk>();
    void Start()
    {
        //Добавить в список созданных платформ первую платформу:
        spawnedChunks.Add(firstChunk);
    }

    void Update()
    {
        //Выполнение метода SpawnChunk, если позиция игрока дальше позиции конца платформы на 25:
        if (player.position.x < spawnedChunks[spawnedChunks.Count - 1].End.position.x +25)
        {
            SpawnChunk();
        }
    }

    //Метод создания платформы:
    private void SpawnChunk()
    {
        //Выбор рандомной платформы из массива и установка её позиции:
        Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        //Добавление созданной платформы в список созданных:
        spawnedChunks.Add(newChunk);

        //Если количество созданных плаформ больше или равно 3, то удалять платформы позади:
        if (spawnedChunks.Count >=3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
}
