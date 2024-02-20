using System.Collections.Generic;
using System.Linq;
using GameConfiguration;
using Interactable;
using UnityEngine;

public class LocationBuilder : MonoBehaviour
{
    [Header("Renderers")] 
    [SerializeField] private Transform[] _decorationPlaces;
    [SerializeField] private Transform _characterPlace, _itemPlace;
    [SerializeField] private SpriteRenderer _backgroundRenderer;

    [Header("Images")]
    [SerializeField] private Sprite[] _backgrounds;
    [SerializeField] private Interactive[] _characters, _items, _decorations;

    private readonly List<GameObject> _currentObjects = new List<GameObject>();

    public void Build(Location location)
    {
        SetImage(_backgroundRenderer, _backgrounds, location.background);
        Clear();
        SpawnObjects(location);
    }
    
    private static void SetImage(SpriteRenderer renderer, Sprite[] list, int number)
    {
        if (number < 0 || number >= list.Length)
        {
            renderer.gameObject.SetActive(false);
            return;
        }

        renderer.sprite = list[number];
        RandomFlip(renderer);
    }

    private void SpawnObjects(Location location)
    {
        SpawnObject(_characters, _characterPlace, location.character);
        SpawnObject(_items, _itemPlace, location.item);

        var decorTransforms = GetRandomElements(location.decorations.Length);

        for (int i = 0; i < decorTransforms.Length; i++)
        {
            SpawnObject(_decorations, decorTransforms[i], location.decorations[i]);
        }
    }

    private void SpawnObject(Interactive[] objects, Transform position, int index)
    {
        if (index < 0 || index >= objects.Length) return;

        var obj = Instantiate(objects[index], position);
        _currentObjects.Add(obj.gameObject);
    }

    private void Clear()
    {
        _currentObjects.ForEach(Destroy);
        _currentObjects.Clear();
    }

    private static void RandomFlip(SpriteRenderer texture2D)
    {
        if (Random.Range(0, 2) == 0) // Генерируем случайное число (0 или 1)
        {
            texture2D.flipX = !texture2D.flipX;
        }
    }

    private Transform[] GetRandomElements(int n) =>
        _decorationPlaces.OrderBy(_ => Random.value).Take(n).ToArray();
}