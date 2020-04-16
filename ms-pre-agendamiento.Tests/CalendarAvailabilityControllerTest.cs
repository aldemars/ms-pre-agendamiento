using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ms_pre_agendamiento.Controllers;
using ms_pre_agendamiento.Models;
using Xunit;

namespace ms_pre_agendamiento.Tests
{
    public class CalendarAvailabilityControllerTest
    {
        [Fact]
        public void GetAllTimeSlotsMock_ShouldReturnListOfTimeSlots()
        {
            // Arrange
            const int expected = 4;

            var mockCalendarAvailabilityService = new Mock<ICalendarAvailabilityService>();
            mockCalendarAvailabilityService
                .Setup(x => x.GetAvailableBlocks())
                .Returns(
                    new List<TimeSlot>
                    {
                        new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 1, HourTo = 2},
                        new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 3, HourTo = 4},
                        new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 5, HourTo = 6},
                        new TimeSlot() {Date = DateTime.Now.ToShortDateString(), HourFrom = 7, HourTo = 8}
                    }
                );
            var calendarAvailabilityController =
                new CalendarAvailabilityController(mockCalendarAvailabilityService.Object);

            // Act
            var actual = calendarAvailabilityController.GetAvailableSlotsFromService().Count();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}