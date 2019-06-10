using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using TDDPalestraXUnitExample3.Model;
using TDDPalestraXUnitExample3.Model.Enum;

namespace XUnitPalestraTDDExample3.Builder
{
    public class UserModelBuilder : BuilderBase<UserModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<UserModel>.CreateNew()
                .With(x => x.BirthDate = DateTime.Now.AddYears(-16))
                .With(x => x.CellPhoneNumber = "(11) 99858-5826")
                .With(x => x.DocumentRG = "47.345.927-4")
                .With(x => x.DocumentCPF = "395.690.908-98")
                .With(x => x.Email = "mail@mail.com")
                .With(x => x.Gender = GenderEnum.Male)
                .With(x => x.Login = "login.eu")
                .With(x => x.Name = "Nome de Alguém")
                .With(x => x.Password = "SOMEVALIDHASHNUMBERYETTOBEDEFINED")
                .With(x => x.ReferenceId = "SOMEREFID")
                .With(x => x.TenantId = TestConstants.TenantId);
                
        }

        public ISingleObjectBuilder<UserModel> WithUniqueFields()
        {
            return _builderInstance
                .With(x => x.ReferenceId = Guid.NewGuid().ToString().Substring(0, 30))
                .With(x => x.Login = Guid.NewGuid().ToString().Substring(0, 30));
        }
    }
}
