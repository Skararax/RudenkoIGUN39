using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

        private void Awake()
		{
            ////todo comment: зачем нужны эти проверки?
            ///answer: Чтобы избежать NullReferenceException 
            if (!TryGetComponent(out _save))
			{
				Debug.LogError("Records incorrect value", this);
                //todo comment: Для чего выключается этот компонент?
                ///answer: Чтобы у нас не вызывался Exception в каждом кадре
                enabled = false;
                return;
			}
            if (_save.Records == null)
            {
                Debug.LogError("Records is null (not initialized yet)", this);
                enabled = false;
                return;
            }

            if (_save.Records.Count == 0)
            {
                Debug.LogWarning("Records list is empty (0 records)", this);
                enabled = false;
            }
        }

		private void Update()
		{
			var curr = _save.Records[_index];
            //todo comment: Что проверяет это условие (с какой целью)? 
            ///answer: проверяет настало ли время для перехода к следующей точке в записи 
            if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
				//todo comment: Для чего нужна эта проверка?
				///answer: проверяет сколько еще осталось записей 
				if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
            //todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
            ///answer: Вычисляем прогресс движения МЕЖДУ точками 
            var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            ///answer: избегаем деления на 0 
            if (float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            ///answer: Плавно перемещает объект из предыдущей позиции (_prev.Position) в текущую (curr.Position) на основе прогресса (delta), где delta=0 в начале отрезка и delta=1 в конце
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}