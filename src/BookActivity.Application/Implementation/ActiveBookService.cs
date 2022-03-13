using BookActivity.Application.Interfaces;
using BookActivity.Application.Models.DTO;
using BookActivity.Application.Models.DTO.Read;
using BookActivity.Application.Models.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Application.Implementation
{
    public class ActiveBookService : IActiveBookService
    {
        private readonly IMediatorHandler _mediatorHandler;

        private readonly IActiveBookRepository _activeBookRepository;

        public ActiveBookService(IMediatorHandler mediatorHandler, IActiveBookRepository activeBookRepository)
        {
            _mediatorHandler = mediatorHandler;
            _activeBookRepository = activeBookRepository;
        }

        public Task<ValidationResult> AddActiveBookAsync(ActiveBookDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ActiveBookDTO>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public Task<ActiveBookDTO> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> RemoveActiveBookAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> UpdateActiveBookAsync(ActiveBookDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
