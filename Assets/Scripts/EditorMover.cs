using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;
		
		//todo comment: Что произойдёт, если _delay > _duration?
		private float _delay = 0.5f;
		[Min(0.2f)]
		private float _duration = 5f;

        private void Start()
		{
			//todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
			//answer: потому что мы должны проинициализировать PositionSaver только один раз, GetComponent дорогая опция и каждый раз выполнять его в Update сильно нагрузит систему + это просто бесполезно
			if (_duration <= _delay) 
			{ 
				_duration = _delay * 5;
			}

			_save = GetComponent<PositionSaver>();
			_save.Records.Clear();
		}

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}

            //todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
            //answer: один таймер, другой интервал
            _currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
                    //todo comment: Для чего сохраняется значение игрового времени?
                    //answer: временная метка для воспроизведения и анализа
                    Time = Time.time,
				});
			}
		}
	}
}