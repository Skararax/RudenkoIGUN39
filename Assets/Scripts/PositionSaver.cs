using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using OpenCover.Framework.Model;
using JetBrains.Annotations;

namespace DefaultNamespace
{
    public class PositionSaver : MonoBehaviour
    {
        [Serializable]
        public struct Data
        {
            public Vector3 Position;
            public float Time;
        }

        [ReadOnly, Tooltip("Используйте контекстное меню → Create File"), SerializeField]
        private TextAsset _json;

        [field: SerializeField, HideInInspector]
        public List<Data> Records;

        private void Awake()
        {
            //todo comment: Что будет, если в теле этого условия не сделать выход из метода?
            //answer: NullReferenceException
            if (_json == null)
            {
                gameObject.SetActive(false);
                Debug.LogError("Please, create TextAsset and add in field _json");
                return;
            }

            JsonUtility.FromJsonOverwrite(_json.text, this);

            //todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
            //answer: NullReferenceException
            if (Records == null)
                Records = new List<Data>(10);
        }


        private void OnDrawGizmos()
        {
            //todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
            //answer: NullReferenceException, IndexOutOfRangeException
            if (Records == null || Records.Count == 0) return;
            var data = Records;
            var prev = data[0].Position;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(prev, 0.3f);

            //todo comment: Почему итерация начинается не с нулевого элемента?
            //answer: потому что первый элемент массива ([0]) уже отработал строками выше 
            for (int i = 1; i < data.Count; i++)
            {
                var curr = data[i].Position;
                Gizmos.DrawWireSphere(curr, 0.3f);
                Gizmos.DrawLine(prev, curr);
                prev = curr;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Create File")]
        private void CreateFile()
        {
            //todo comment: Что происходит в этой строке?
            //answer: эта строка готовит новый текстовый файл 
            var stream = System.IO.File.Create(Path.Combine(Application.dataPath, "Path.txt"));
            //todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав) 
            //answer: (моя догадка была неверна :с ) stream.Dispose эта строка освобождает файл который был открыт для записи
            stream.Dispose();
            UnityEditor.AssetDatabase.Refresh();
            //В Unity можно искать объекты по их типу, для этого используется префикс "t:"
            //После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
            var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
            foreach (var guid in guids)
            {
                //Этой командой можно получить путь к ассету через его гуид
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                //Этой командой можно загрузить сам ассет
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                //todo comment: Для чего нужны эти проверки?
                ///answer:Проверяем наличие ссылки на запись
                if (asset != null && asset.name == "Path")
                {
                    _json = asset;
                    UnityEditor.EditorUtility.SetDirty(this);
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.AssetDatabase.Refresh();
                    //todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
                    ///answer: нашли нужный файл - дальнейший поиск не требуется, выходим для оптимизации и предотвращения ошибок
                    return;
                }
            }
        }

        [Serializable]
        public class SaveWrapper
        {
            public List<Data> Records;
        }

        private void OnDestroy()
        {
            //todo logic...
            if (_json == null)
            {
                Debug.Log("There is no file to save");
                return;
            }

            if (Records == null || Records.Count == 0)
            {
                Debug.Log("There is no records to save");
                return;
            }

            var saveData = new SaveWrapper { Records = Records };
            string jsonText = JsonUtility.ToJson(saveData, true);
            string filePath = AssetDatabase.GetAssetPath(_json);

            System.IO.File.WriteAllText(filePath, jsonText);

            AssetDatabase.Refresh();
        }
#endif
    }
}