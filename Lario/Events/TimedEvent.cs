using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lario.Events
{
    public class TimedEvent
    {
        private bool _isStarted;
        private double _startTime;

        private double _timeOut;

        private Action _actionToExecute;

        public TimedEvent(double timeOut, Action actionToExecute)
        {
            _timeOut = timeOut;
            _actionToExecute = actionToExecute;
        }

        public void Start(double startTime)
        {
            _isStarted = true;
            _startTime = startTime;
        }

        public void Update(double elapsedTime)
        {
            if (_isStarted && (elapsedTime - _startTime) > _timeOut)
            {
                _actionToExecute?.Invoke();
                _startTime = elapsedTime;
                _isStarted = false;
            }
        }
    }
}
