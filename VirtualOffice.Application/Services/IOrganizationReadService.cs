using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Repositories;

namespace VirtualOffice.Application.Services
{
    public interface IOrganizationReadService
    {


        public IOrganizationRepository _repository;
        public IOrganizationReadService _readService;

        public CreateNoteHandler(INoteRepository repository, INoteReadService noteReadService)
        {
            _repository = repository;
            _readService = noteReadService;
        }

        Task<bool> ExistsByIdAsync(Guid id);
    }
}
