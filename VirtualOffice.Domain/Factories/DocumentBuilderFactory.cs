using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Builders;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Exceptions.Factories;

namespace VirtualOffice.Domain.Factories
{
    public class DocumentBuilderFactory
    {
        public static IDocumentBuilder CreateBuilder(DocumentTypeEnum documentTypeEnum)
        {
            switch(documentTypeEnum)
            {
                case DocumentTypeEnum.Private:
                    return new PrivateDocumentBuilder();
                case DocumentTypeEnum.Public:
                    return new PublicDocumentBuilder();
                default:
                    throw new IDocumentBuilderOfThatTypeDoesntExistException(documentTypeEnum);
            }
        }


    }
}
