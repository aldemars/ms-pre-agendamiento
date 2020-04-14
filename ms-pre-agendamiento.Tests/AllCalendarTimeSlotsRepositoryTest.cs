using System.Linq;
using Xunit;

namespace ms_pre_agendamiento.Tests
{
    public class AllCalendarTimeSlotsRepositoryTests
    {
        private readonly AllCalendarTimeSlotsRepository _allCalendarTimeSlots;

        public AllCalendarTimeSlotsRepositoryTests()
        {
            _allCalendarTimeSlots = new AllCalendarTimeSlotsRepository();
        }

        [Fact]
        public void GetAllTimeSlotsMock_ShouldReturnListOfTimeSlots()
        {
            // Arrange
            const int expected = 4;

            // Act
            var actual = _allCalendarTimeSlots.GetAllTimeSlotsMock().Count();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}