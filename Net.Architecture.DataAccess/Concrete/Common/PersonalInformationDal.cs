﻿using Net.Architecture.DataAccess.Abstract.Common;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository;
using Net.Architecture.Entities.Concrete.Common;

namespace Net.Architecture.DataAccess.Concrete.Common
{
    public class PersonalInformationDal : BaseRepository<PersonalInformation, PostgreSqlContext>, IPersonalInformationDal
    {
        public PersonalInformationDal(PostgreSqlContext PostgreSqlContext) : base(PostgreSqlContext)
        {
        }

    }
}
