using Xunit;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class AlarmTest
    {
        public class TestSensor : ISensor
        {
            public double testValue { get; set; } = 20;

            public double PopNextPressurePsiValue()
            {
                return testValue;
            }
        }

        private TestSensor _sensor;
        private Alarm _sut;
        public AlarmTest()
        {
            _sensor = new TestSensor();
            _sut = new Alarm(_sensor);
        }

        [Fact]
        public void ShouldStartInOffPosition()
        {
            _sensor.testValue = 20;
            Alarm alarm = new Alarm(_sensor);
            Assert.False(alarm.AlarmOn);
        }

        [Theory]
        [InlineData(17, false)]
        [InlineData(17.0001, false)]
        [InlineData(19, false)]
        [InlineData(21, false)]
        public void ShouldNotTriggerInRange(double pressure, bool shouldTrigger)
        {
            _sensor.testValue = pressure;
            Alarm alarm = new Alarm(_sensor);
            alarm.Check();
            Assert.Equal(alarm.AlarmOn, shouldTrigger);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(16.9999, true)]
        [InlineData(0, true)]
        [InlineData(-1, true)]
        public void ShouldTriggerIfValueLow(double pressure, bool shouldTrigger)
        {
            _sensor.testValue = pressure;
            Alarm alarm = new Alarm(_sensor);
            alarm.Check();
            Assert.Equal(alarm.AlarmOn, shouldTrigger);
        }

        [Theory]
        [InlineData(30, true)]
        [InlineData(21.1, true)]
        public void ShouldTriggerIfValueHigh(double pressure, bool shouldTrigger)
        {
            _sensor.testValue = pressure;
            Alarm alarm = new Alarm(_sensor);
            alarm.Check();
            Assert.Equal(alarm.AlarmOn, shouldTrigger);
        }

        [Fact]
        public void ShouldIncreaseAlarmCount()
        {
            _sensor.testValue = 10;
            Alarm alarm = new Alarm(_sensor);
            alarm.Check();
            Assert.Equal(1, alarm.AlarmCount);
            _sensor.testValue = 30;
            alarm.Check();
            Assert.Equal(2, alarm.AlarmCount);
            _sensor.testValue = 0;
            alarm.Check();
            Assert.Equal(3, alarm.AlarmCount);
        }
    }
}