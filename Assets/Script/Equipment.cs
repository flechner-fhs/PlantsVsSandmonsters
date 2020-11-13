using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Equipment")]
public class Equipment : ScriptableObject
{
    [Header("Basic Info")]
    [Tooltip("Name of the equipment")]
    public string Name = "New Equipment";
    [Tooltip("Sprite that is displayed in equipment selection")]
    public Sprite Sprite;
    [Tooltip("Offset of the Sprite to make it centered")]
    public Vector2 SpriteOffset;
    [Tooltip("How many equipment points this consumes")]
    public int Cost = 1;

    [TextArea]
    [Tooltip("Description that is displayed in equipment selection")]
    public string Description = "This is an equipment";

    [Header("Stat Changes")]
    [Tooltip("Changes the Water Supply Cap")]
    public float WaterSupplyMax = 0;

    [HideInInspector]
    public bool UnlocksNewPlant = false;
    [HideInInspector]
    [SerializeField]
    private GameObject plantPrefab = null;

    public GameObject PlantPrefab => UnlocksNewPlant ? plantPrefab : null;
}

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : Editor
{
    private SerializedProperty plantPrefabReference;

    private void OnEnable()
    {
        plantPrefabReference = serializedObject.FindProperty("plantPrefab");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var equipmentScript = target as Equipment;

        equipmentScript.UnlocksNewPlant = EditorGUILayout.Toggle("Unlocks New Plant", equipmentScript.UnlocksNewPlant);


        if (equipmentScript.UnlocksNewPlant)
            plantPrefabReference.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Plant Prefab", "Add the prefab of the plant"), plantPrefabReference.objectReferenceValue, typeof(GameObject), false);

        serializedObject.ApplyModifiedProperties();
    }
}