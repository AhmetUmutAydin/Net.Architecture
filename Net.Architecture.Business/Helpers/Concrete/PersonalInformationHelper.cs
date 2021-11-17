using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Core.Constants;
using Net.Architecture.Core.Extensions;
using Net.Architecture.Core.Utilities.Result;
using Net.Architecture.DataAccess.UnitOfWork;
using Net.Architecture.Entities.Concrete.Common;
using Net.Architecture.Entities.Dtos;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class PersonalInformationHelper : BaseBusiness, IPersonalInformationHelper
    {
        private readonly IFileHelper _fileHelper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalInformationHelper(IFileHelper fileHelper, IUnitOfWork unitOfWork)
        {
            _fileHelper = fileHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IServiceResult<PersonalInformation>> AddPersonalInformation(PersonalInformationDto personalInformationDto)
        {
            var personalInformation = personalInformationDto.ToEntity<PersonalInformation>();
            personalInformation.Status = true;

            if (!string.IsNullOrEmpty(personalInformationDto.ImageUrl))
            {
                var fileIdResponse = await _fileHelper.GetFileIdByRootİmageUrl(personalInformationDto.ImageUrl);
                if (fileIdResponse.Result)
                    personalInformation.PictureId = fileIdResponse.Data;
                else
                    return new ServiceResult<PersonalInformation>(fileIdResponse.Message);
            }
            await _unitOfWork.Repository<PersonalInformationDal>().AddAsync(personalInformation);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceResult<PersonalInformation>(personalInformation);
        }

        public async Task<IServiceResult> UpdatePersonalInformation(PersonalInformationDto personalInformationDto)
        {
            if (!personalInformationDto.Id.HasValue)
                return new ServiceResult<AddressDto>(Messages.GlobalError);

            var personalInformation = await _unitOfWork.Repository<PersonalInformationDal>().GetAsync(x => x.Status && x.Id == personalInformationDto.Id);

            if (personalInformation is null)
                return new ServiceResult<AddressDto>(Messages.PersonalInformationNotFound);

            personalInformation.Name = personalInformationDto.Name;
            personalInformation.Surname = personalInformationDto.Surname;
            personalInformation.BirthPlace = personalInformationDto.BirthPlace;
            personalInformation.Birthdate = personalInformationDto.Birthdate;
            personalInformation.NationalIdentifier = personalInformationDto.NationalIdentifier;
            personalInformation.Gender = personalInformationDto.Gender;
            personalInformation.Notes = personalInformationDto.Notes;

            _unitOfWork.Repository<PersonalInformationDal>().Update(personalInformation);
            await _unitOfWork.SaveChangesAsync();
            return new ServiceResult();
        }

        public async Task<IServiceResult<IEnumerable<DropdownDto>>> GetPersonalInformationDropdown()
        {
            var result = await _unitOfWork.Repository<PersonalInformationDal>().GetEmployeeListWithPersonalInformation(JwtInstitutionId);
            var dtos = result.ToDtos<DropdownDto>();
            return new ServiceResult<IEnumerable<DropdownDto>>(dtos);
        }
    }
}
