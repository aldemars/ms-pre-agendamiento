using System.Linq;
using NUnit.Framework;

namespace ms_pre_agendamiento.Tests
{
    public class AllCalendarTimeSlotsRepositoryTests
    {
        private AllCalendarTimeSlotsRepository _allCalendarTimeSlots;

        [SetUp]
        public void Setup()
        {
            _allCalendarTimeSlots = new AllCalendarTimeSlotsRepository();
        }

        [Test]
        public void GetAllTimeSlotsMock_ShouldReturnListOfTimeSlots()
        {
            // Arrange
            const int expected = 4;

            // Act
            var actual = _allCalendarTimeSlots.GetAllTimeSlotsMock().Count();

            // Assert
            Assert.AreEqual(actual, expected);
        }
    }
}