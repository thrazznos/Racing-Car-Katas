using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        public Alarm(ISensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor), "Sensor cannot be null");
        }

        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        ISensor _sensor;

        bool _alarmOn = false;
        private long _alarmCount = 0;


        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
            {
                _alarmOn = true;
                _alarmCount += 1;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }

        public long AlarmCount
        {
            get { return _alarmCount; }
        }
    }
}
