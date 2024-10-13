﻿using MassTransit;
using VirtualOffice.Application.IntegrationEvents.CalendarEventIntegrationEvents;
using VirtualOffice.Infrastructure.MongoDb.Services;

namespace VirtualOffice.Infrastructure.RabbitMQ.Consumers.CalendarEventConsumers
{
    public sealed class CalendarEventCreatedEventConsumer : IConsumer<CalendarEventCreatedIntegrationEvent>
    {
        private readonly CalendarEventsService _calendarEventsService;

        public CalendarEventCreatedEventConsumer(CalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        public async Task Consume(ConsumeContext<CalendarEventCreatedIntegrationEvent> context)
        {
            await _calendarEventsService.CreateAsync(context.Message);
        }
    }
}