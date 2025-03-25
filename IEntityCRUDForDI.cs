using FieldTool.DAL.DTO;
using System;
using System.Collections.Generic;

namespace FieldTool.Entity
{
    public interface IEntityCRUDForDI
    {
        FieldTool.DAL.DTO.DIDTO ParseAsDIDTO(string contents);

        void Save(List<DIDTO> dis, string externalId, Uri uploadedUrl, string userName, bool isDeleted);
    }
}