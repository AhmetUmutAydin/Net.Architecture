using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class ContactHelper : BaseBusiness, IContactHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IServiceResult<IEnumerable<CommunicationDto>>> SaveCommunications(IEnumerable<CommunicationDto> communicationDtos, IEnumerable<Communication> communications, long contactId)
        {
            var insertResult = await InsertContactCommunications(communicationDtos, contactId);
            if (!insertResult.Result)
                return new ServiceResult<IEnumerable<CommunicationDto>>(insertResult.Message);

            var updateResult = await UpdateContactCommunications(communicationDtos);
            if (!updateResult.Result)
                return new ServiceResult<IEnumerable<CommunicationDto>>(updateResult.Message);

            var deleteResult = await DeleteContactCommunications(communicationDtos, communications);
            if (!deleteResult.Result)
                return new ServiceResult<IEnumerable<CommunicationDto>>(deleteResult.Message);

            var result = updateResult.Data.Union(insertResult.Data);
            return new ServiceResult<IEnumerable<CommunicationDto>>(result);
        }

        public async Task<IServiceResult<IEnumerable<CommunicationDto>>> InsertContactCommunications(IEnumerable<CommunicationDto> communicationDtos, long contactId)
        {
            var insertedCommunications = communicationDtos.Where(i => i.Id == null).ToEntities<Communication>();
            var result = new List<Communication>();
            if (insertedCommunications.Any())
            {
                foreach (var entity in insertedCommunications)
                {
                    entity.Status = true;
                    entity.ContactId = contactId;
                    await _unitOfWork.Repository<CommunicationDal>().AddAsync(entity);
                    result.Add(entity);
                }

                await _unitOfWork.SaveChangesAsync();
            }
            return new ServiceResult<IEnumerable<CommunicationDto>>(result.ToDtos<CommunicationDto>());
        }

        public async Task<IServiceResult<Contact>> CreateContact()
        {
            var contact = new Contact()
            {
                Status = true,
            };
            await _unitOfWork.Repository<ContactDal>().AddAsync(contact);
            return new ServiceResult<Contact>(contact);
        }

        private async Task<IServiceResult> DeleteContactCommunications(IEnumerable<CommunicationDto> communicationDtos, IEnumerable<Communication> communications)
        {
            var deletedCommunications = communications.Where(i => i.Status && !communicationDtos.Any(c => c.Id == i.Id && c.Id != null));
            if (deletedCommunications.Any())
            {
                _unitOfWork.Repository<CommunicationDal>().DeleteRange(deletedCommunications);
                await _unitOfWork.SaveChangesAsync();
            }

            return new ServiceResult();
        }

        private async Task<IServiceResult<IEnumerable<CommunicationDto>>> UpdateContactCommunications(IEnumerable<CommunicationDto> communicationDtos)
        {
            var updatedCommunications = communicationDtos.Where(c => c.Id != null);
            var result = new List<Communication>();

            if (updatedCommunications.Any())
            {
                foreach (var dto in updatedCommunications)
                {
                    var entity = await _unitOfWork.Repository<CommunicationDal>().GetAsync(x => x.Id == dto.Id && x.Status);
                    if (entity.Value != dto.Value || entity.CommunicationType != dto.CommunicationType)
                    {
                        entity.Value = dto.Value;
                        entity.CommunicationType = dto.CommunicationType;
                        _unitOfWork.Repository<CommunicationDal>().Update(entity);
                    }
                    result.Add(entity);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            return new ServiceResult<IEnumerable<CommunicationDto>>(result.ToDtos<CommunicationDto>());
        }

    }
}
