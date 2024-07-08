using MediatR;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers
{
    public class UpdateCalendarEventHandler : IRequestHandler<UpdateCalendarEvent>
    {
        private readonly ICalendarEventRepository _repository;
        private readonly ICalendarEventReadService _readService;

        public UpdateCalendarEventHandler(ICalendarEventRepository repository, ICalendarEventReadService readService)
        {
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(UpdateCalendarEvent request, CancellationToken cancellationToken)
        {
            var (Id, Title, EventDescription, StartDate, EndDate) = request;

            if (!await _readService.ExistsByIdAsync(Id))
            {
                throw new CalendarEventDoesNotExistException(Id);
            }

            var calEv = await _repository.GetById(Id);


            // we update only changed properties rather than whole object 
            // beacuse changing the title to the same title would raise an event.
            if (calEv._Title != Title)
                calEv.SetTitle(Title);
            if (calEv._Description != EventDescription)
                calEv.SetDescription(EventDescription);
            if (calEv._StartDate != StartDate)
                calEv.UpdateStartDate(StartDate);
            if (calEv._EndDate != EndDate)
                calEv.UpdateEndDate(EndDate);

            await _repository.Update(calEv);
            await _repository.SaveAsync(cancellationToken);

        }


    }
}
